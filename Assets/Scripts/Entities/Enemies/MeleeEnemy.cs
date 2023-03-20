using UnityEngine;

public class MeleeEnemy : Enemy
{
    protected override void Attack()
    {
        _anim.SetTrigger(_attackTriggerHash);
        StartCoroutine(transform.SmoothMoveTo(GameManager.player.transform.position + Vector3.forward * 2, 0.1f));
    }
}
