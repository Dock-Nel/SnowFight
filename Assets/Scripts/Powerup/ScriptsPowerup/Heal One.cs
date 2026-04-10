using UnityEngine;

[CreateAssetMenu(fileName = "Heal One", menuName = "PowerUps/HealOne")]
public class HealOne : DataPowerUp
{
    public override void ApplyEffect(PlayerController player)
    {
            player.Health += 10;
    }
}
