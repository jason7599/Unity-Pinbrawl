public class BonusBall : Entity
{
    public override void OnHit(int damage) => OnBonusBallGet();
    protected override void OnPlayerZoneEnter() => OnBonusBallGet();

    private void OnBonusBallGet()
    {
        GameManager.player.AddBall();
        DestroyThis();
    }
}
