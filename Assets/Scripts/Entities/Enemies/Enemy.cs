using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : Entity
{
    protected Animator _anim;
    protected Collider _collider;

    protected int _dieTriggerHash = Animator.StringToHash("die");
    protected int _readyBoolHash = Animator.StringToHash("ready");
    protected int _attackTriggerHash = Animator.StringToHash("attack");
    protected int _hurtTriggerHash = Animator.StringToHash("hurt");

    [SerializeField] protected int _maxHealth = 100;
    protected int _health;

    [SerializeField] protected ParticleSystem _bloodEffect;
    [SerializeField] protected Slider _healthBar;
    [SerializeField] protected TMP_Text _healthText;

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

    public override void SetTile(Tile tile)
    {
        base.SetTile(tile);

        if (tile != null && tile.rowIndex == 0)
        {
            _anim.SetBool(_readyBoolHash, true);
        }
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

    protected override void OnPlayerZoneEnter()
    {
        Attack();
        
        DestroyThis(0.5f);
    }

    protected abstract void Attack();

}
