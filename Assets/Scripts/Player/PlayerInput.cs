using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Player _player;
    private PlayerAim _aim;

    private void Start()
    {
        _player = GetComponent<Player>();
        _aim = GetComponent<PlayerAim>();
        _aim.enabled = false;

        // enable self on player turn start
        GameManager.Instance.OnPlayerTurnStart += () => enabled = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _aim.enabled = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            _aim.enabled = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            enabled = false;
            _aim.enabled = false;
            _player.Shoot();
        }
    }

}
