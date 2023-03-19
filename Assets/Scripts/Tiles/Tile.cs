using UnityEngine;
using UnityEditor;

public class Tile : MonoBehaviour
{   
    public int rowIndex { get; private set; }
    public int columnIndex { get; private set; }

    public Entity entity { get; private set; }
    public bool isOccupied { get { return entity != null; } }
 
    public void Configure(int row, int column, Material material)
    {
        rowIndex = row; columnIndex = column;
        GetComponentInChildren<Renderer>().material = material;
    }

    public void SetEntity(Entity entity) // called by entity.SetTile(tile)
    {
        this.entity = entity;
    }

    public override string ToString()
    {
        return $"Tile {rowIndex}, {columnIndex}";
    }

    // private void OnDrawGizmos()
    // {
    //     Handles.Label(transform.position, $"{rowIndex}, {columnIndex}");
    // }
}
