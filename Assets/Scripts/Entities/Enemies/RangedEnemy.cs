using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] private GameObject _projectilePrefab;

    public override void Advance() // attack before moving 
    {
        Attack();

        base.Advance(); 
    }

    protected override void Attack()
    {
        GameObject projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
        StartCoroutine(projectile.transform.SmoothMoveTo(GameManager.player.transform.position, 0.1f));
        Destroy(projectile, 0.2f); // TEMP
    }
}
