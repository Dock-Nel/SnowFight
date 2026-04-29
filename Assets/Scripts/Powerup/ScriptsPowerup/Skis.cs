using UnityEngine;

[CreateAssetMenu(fileName = "Snow Skis", menuName = "PowerUps/Tool/Snow Skis")]
public class SnowSkis : DataPowerUp
{
    public float walkingSpeedMultiplier = 1.5f;
    public float runningSpeedMultiplier = 1.5f;
    public float jumpHeightMultiplier = 1.3f;
    public float reloadDelay = 0.10f;

    public override void ApplyEffect(PlayerController player)
    {
        player.EquipTool(this);
    }
}