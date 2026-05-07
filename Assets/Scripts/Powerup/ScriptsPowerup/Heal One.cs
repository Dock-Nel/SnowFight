using UnityEngine;

[CreateAssetMenu(fileName = "Heal One", menuName = "PowerUps/HealOne")]
public class HealOne : DataPowerUp
{
    public override void ApplyEffect(PlayerController player)
    {
        if (player.Health + 10 <= player.MaxHealth)
        {
            player.Health += 10;
        }
    }
}
