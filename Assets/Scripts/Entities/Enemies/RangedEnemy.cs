using UnityEngine;
using System.Collections;

public class RangedEnemy : Enemy
{
    [SerializeField] private GameObject _projectilePrefab;

    private void Start()
    {
        preAdvancePriority = PreAdvancePriority.First;
    }

    public override IEnumerator PreAdvanceRoutine()
    {
        Attack();
        yield return new WaitForSeconds(0.1f);
    }

    protected override void Attack()
    {
        GameObject projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
        StartCoroutine(projectile.transform.SmoothMoveTo(GameManager.player.transform.position, 0.1f));
        Destroy(projectile, 0.2f); // TEMP
    }
}
