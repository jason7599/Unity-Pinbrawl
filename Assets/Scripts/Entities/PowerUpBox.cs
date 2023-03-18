using UnityEngine;

public class PowerUpBox : Entity
{
    public override void OnHit(int damage) => OnPowerUpBoxGet();
    protected override void OnPlayerZoneEnter() => OnPowerUpBoxGet();

    [SerializeField] private Ball[] _ballPrefabs;

    private void OnPowerUpBoxGet()
    {
        // TEMP
        Ball randomBallPrefab = _ballPrefabs[Random.Range(0, _ballPrefabs.Length)];
        print($"congrats! you got {randomBallPrefab.name}");
        GameManager.player.AddBall(Instantiate(randomBallPrefab));
        DestroyThis();
    }
}
