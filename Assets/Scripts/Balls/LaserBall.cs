using UnityEngine;

public class LaserBall : Ball
{
    protected override void OnEntityHit(Entity entity)
    {
        int row = entity.currentTile.rowIndex;
        int cols = GameManager.tiles.columnCount;

        for (int c = 0; c < cols; c++)
        {
            GameManager.tiles[row, c].entity?.OnHit(_damage);
        }
    }
}
