using UnityEngine;

[CreateAssetMenu(fileName = "Snow Canon", menuName = "PowerUps/Tool/Snow Canon")]
public class SnowCanon : DataPowerUp
{
    public int ballsPerShot = 3;
    public int ammoCost = 2;
    public float delayBetweenBalls = 0.1f;

    public override void ApplyEffect(PlayerController player)
    {
        player.EquipTool(this);
    }
}