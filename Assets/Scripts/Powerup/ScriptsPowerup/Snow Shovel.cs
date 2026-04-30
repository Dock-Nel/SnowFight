using UnityEngine;

[CreateAssetMenu(fileName = "Snow Shovel", menuName = "PowerUps/Tool/Snow Shovel")]
public class SnowShovel : DataPowerUp
{
    public int reloadAmount = 4; 

    public override void ApplyEffect(PlayerController player)
    {
        player.EquipTool(this);
    }
}