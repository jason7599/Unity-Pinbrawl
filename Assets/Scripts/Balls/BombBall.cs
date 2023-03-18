using UnityEngine;

public class BombBall : Ball
{
    [SerializeField] private ParticleSystem _explosionEffect;

    protected override void OnEntityHit(Entity entity)
    {
        int r = entity.currentTile.rowIndex;
        int c = entity.currentTile.columnIndex;

        for (int dR = -1; dR <= 1; dR++)
        {
            for (int dC = -1; dC <= 1; dC++)
            {
                GameManager.tiles[r + dR, c + dC]?.entity?.OnHit(_damage);
            }
        }
        
        transform.PlayEffect(_explosionEffect);
        ReturnToPlayer();
    }
}
