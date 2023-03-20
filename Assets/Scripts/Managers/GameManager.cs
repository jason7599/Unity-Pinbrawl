using TMPro;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    public Action OnPlayerTurnStart;

    [SerializeField] private Player _player;
    [SerializeField] private Tiles _tiles;
    [SerializeField] private Entities _entities;

    public static Player player { get { return Instance._player; } }
    public static Tiles tiles { get { return Instance._tiles; } }
    public static Entities entities { get { return Instance._entities; } }

    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private BonusBall _bonusBallPrefab;
    [SerializeField] private PowerUpBox _powerUpBoxPrefab;

    [SerializeField] private TMP_Text _levelsText;

    [SerializeField] private int _enemySpawnCount = 3;

    private Camera _cam; // TEMP
    private int _level = 0;
    
    private void Start()
    {
        _cam = Camera.main;
        _player.OnPlayerTurnFinished += AdvanceLevel;

        AdvanceLevel();
    }

    private void AdvanceLevel() => StartCoroutine(AdvanceLevelRoutine());

    private IEnumerator AdvanceLevelRoutine()
    {
        _level++;

        yield return StartCoroutine(_entities.AdvanceRoutine());

        _levelsText.text = $"{_level}";
        
        yield return new WaitForSeconds(0.5f);

        SpawnEntities();

        yield return new WaitForSeconds(0.25f);

        OnPlayerTurnStart?.Invoke();
    }

    private void SpawnEntities()
    {
        List<Tile> spawnableTiles = Utils.Shuffle(_tiles.GetUnoccupiedTilesInSpawnRow());

        int spawnedCount = 0;

        foreach (Tile tile in spawnableTiles) // try to spawn at least _enemySpawnCount enemies
        {
            _entities.Spawn(_enemyPrefab, tile).SetMaxHealth(3 * _level);
            if (++spawnedCount >= _enemySpawnCount)
                break;
        }

        int levelMod = _level % 3;
        if (levelMod == 0) return; // no extra spawn

        if (spawnedCount < spawnableTiles.Count) // room for more
        {
            _entities.Spawn<Entity>(levelMod == 1 ? _bonusBallPrefab : _powerUpBoxPrefab, spawnableTiles[spawnedCount]);
        }
    }

    void Update()
    {
        Tile tile = TileLookedAt();

        if (tile != null)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (tile.isOccupied == false)
                {
                    // tile.SpawnEntity(_enemyPrefab);
                    Enemy enemy = _entities.Spawn(_enemyPrefab, tile);
                }
                else
                {
                    print("This tile is occupied!");
                }
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                tile.entity?.OnHit(100_000);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            // SpawnRandom(3);
        }
    }

    Tile TileLookedAt()
    {
        Ray cursorRay = _cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(cursorRay, out RaycastHit hit, 10f, (1 << 10)))
            return hit.transform.GetComponent<Tile>();
        return null;
    }

}
