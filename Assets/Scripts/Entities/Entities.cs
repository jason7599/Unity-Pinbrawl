using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entities : MonoBehaviour
{
    private List<Entity> _entities = new List<Entity>();
    public int activeCount { get { return _entities.Count; } }

    private EntityAdvanceOrder _advanceOrder = new EntityAdvanceOrder();

    public T Spawn<T>(T entityPrefab, Tile tile) where T : Entity
    {
        T entity = Instantiate(entityPrefab, tile.transform.position, Quaternion.identity, transform);
        entity.SetTile(tile);
        _entities.Add(entity);
        return entity;
    }

    public IEnumerator AdvanceRoutine()
    {
        _entities.RemoveAll(entity => entity == null || entity.currentTile == null);

        if (activeCount == 0) yield break;

        List<Entity> preAdvanceFirst = new List<Entity>();
        List<Entity> preAdvanceSecond = new List<Entity>();

        foreach (Entity entity in _entities)
        {
            if (entity.preAdvancePriority == PreAdvancePriority.First) preAdvanceFirst.Add(entity);
            else if (entity.preAdvancePriority == PreAdvancePriority.Second) preAdvanceSecond.Add(entity);
        }

        foreach (Entity entity in preAdvanceFirst)
        {
            yield return StartCoroutine(entity.PreAdvanceRoutine());
        }
        foreach (Entity entity in preAdvanceSecond)
        {
            yield return StartCoroutine(entity.PreAdvanceRoutine());
        }

        _entities.RemoveAll(entity => entity == null || entity.currentTile == null); 
        _entities.Sort(_advanceOrder); // sort by tile 

        foreach (Entity entity in _entities)
        {
            entity.Advance();
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
