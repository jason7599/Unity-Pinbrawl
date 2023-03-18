using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Entity
{
    private Animator _anim;
    private Collider _collider;

    private int _dieTriggerHash = Animator.StringToHash("die");
    private int _readyBoolHash = Animator.StringToHash("ready");
    private int _attackTriggerHash = Animator.StringToHash("attack");
    private int _hurtTriggerHash = Animator.StringToHash("hurt");

    [SerializeField] private int _maxHealth = 100;
    private int _health;

    [SerializeField] private ParticleSystem _bloodEffect;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private TMP_Text _healthText;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _collider = GetComponent<Collider>();
    }

    public void SetMaxHealth(int maxHealth)
    {
        _healthBar.value = _healthBar.maxValue = _health = _maxHealth = maxHealth;
        _healthText.text = $"{_health}";
    }

    public override void OnHit(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Die();
            return;
        }
        _healthBar.value = _health;
        _healthText.text = $"{_health}";
        _anim.SetTrigger(_hurtTriggerHash);
    }

    private void Die()
    {
        _anim.SetTrigger(_dieTriggerHash);
        _collider.enabled = false;

        transform.PlayEffect(_bloodEffect);
        DestroyThis(0.5f);
    }

    public override void SetTile(Tile tile)
    {
        base.SetTile(tile);

        if (tile != null && tile.rowIndex == 0)
        {
            _anim.SetBool(_readyBoolHash, true);
        }
    }

    protected override void OnPlayerZoneEnter()
    {
        CloseAttack();
    }

    private void CloseAttack()
    {
        transform.LookAt(GameManager.player.transform);
        _anim.SetTrigger(_attackTriggerHash);

        DestroyThis(0.5f);
    }
}
