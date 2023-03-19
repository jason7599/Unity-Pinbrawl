using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entities : MonoBehaviour
{
    private List<Entity> _entities = new List<Entity>();
    public int activeCount { get { return _entities.Count; } }

    private EntityAdvanceOrder _comp = new EntityAdvanceOrder();

    public T Spawn<T>(T entityPrefab, Tile tile) where T : Entity
    {
        T entity = Instantiate(entityPrefab, tile.transform.position, Quaternion.identity, transform);
        entity.SetTile(tile);
        _entities.Add(entity);
        return entity;
    }

    public IEnumerator AdvanceRoutine()
    {
        if (activeCount == 0) yield break;

        _entities.RemoveAll(entity => entity == null || entity.currentTile == null); // filter out nulls
        _entities.Sort(_comp); // sort by tile 
        
        foreach (Entity entity in _entities)
        {
            entity.Advance();
            yield return new WaitForSeconds(0.025f);
        }
    }

    private class EntityAdvanceOrder : IComparer<Entity>
    {
        public int Compare(Entity x, Entity y)
        {
            int xR = x.currentTile.rowIndex;
            int yR = y.currentTile.rowIndex;

            if (xR != yR) return xR.CompareTo(yR);

            int xC = x.currentTile.columnIndex;
            int yC = y.currentTile.columnIndex;

            return xC.CompareTo(yC);
        }
    }
}
