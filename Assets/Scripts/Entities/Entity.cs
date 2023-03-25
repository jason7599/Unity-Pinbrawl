using UnityEngine;
using System.Collections;

public enum PreAdvancePriority { None, First, Second }

public abstract class Entity : MonoBehaviour
{
    public Tile currentTile { get; private set; }
    
    public PreAdvancePriority preAdvancePriority { get; protected set; } = PreAdvancePriority.None;
    public virtual IEnumerator PreAdvanceRoutine() { yield break; }

    public void Advance()
    {
        Tile nextTile = GameManager.tiles.TileBelow(currentTile); // TODO: abstract 

        if (nextTile == null)
        {
            OnPlayerZoneEnter(); // COROUTINE
        }
        else if (!nextTile.isOccupied)
        {
            // TEMP. Add animations or smth later
            SetTile(nextTile);
            StartCoroutine(transform.SmoothMoveTo(nextTile.transform.position, 0.15f));
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
