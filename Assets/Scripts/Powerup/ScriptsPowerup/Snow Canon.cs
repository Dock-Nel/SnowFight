using UnityEngine;

[CreateAssetMenu(fileName = "Snow Canon", menuName = "PowerUps/Tool/Snow Canon")]
public class SnowCanon : DataPowerUp
{
    public int ammoCost = 1;

    public override void ApplyEffect(PlayerController player)
    {
        player.EquipTool(this);
    }
}