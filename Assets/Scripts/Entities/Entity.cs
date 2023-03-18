using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public Tile currentTile { get; private set; }

    public void Advance()
    {
        Tile nextTile = GameManager.tiles.TileBelow(currentTile); // TODO: abstract 

        if (nextTile == null)
        {
            OnPlayerZoneEnter();
        }
        else if (!nextTile.isOccupied)
        {
            // TEMP. Add animations or smth later
            // transform.position = nextTile.transform.position;
            StartCoroutine(transform.SmoothMoveTo(nextTile.transform.position, 0.15f));
            SetTile(nextTile);
        }
    }

    public virtual void SetTile(Tile tile)
    {
        currentTile?.SetEntity(null);
        tile?.SetEntity(this);
        currentTile = tile;
    }

    protected void DestroyThis(float destroyTime = 0f) // death, playerzone enter
    {
        SetTile(null);
        Destroy(gameObject, destroyTime);
    }

    protected abstract void OnPlayerZoneEnter();
    public abstract void OnHit(int damage = 1);
}