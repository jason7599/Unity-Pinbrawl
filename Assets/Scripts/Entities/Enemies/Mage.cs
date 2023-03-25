using UnityEngine;
using System.Collections;

public class Mage : MeleeEnemy
{
    [SerializeField] private Enemy _spawnEnemy;

    private void Start()
    {
        preAdvancePriority = PreAdvancePriority.Second;
    }

    public override IEnumerator PreAdvanceRoutine()
    {
        Tile randomTile = GameManager.tiles.GetRandomUnoccupiedTile();
        if (randomTile != null)
        {
            GameManager.entities.Spawn(_spawnEnemy, randomTile);
        }

        yield return new WaitForSeconds(0.5f);
    }
}
