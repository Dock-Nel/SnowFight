using UnityEngine;

[CreateAssetMenu(fileName = "Heal Two", menuName = "PowerUps/HealTwo")]
public class HealTwo : DataPowerUp
{
    public override void ApplyEffect(PlayerController player)
    {
        if (player.Health + 20 > player.MaxHealth)
        {
            player.Health += 20;

        }
        else if (player.Health + 10 > player.MaxHealth)
        {
            player.Health += 10;
        }
    }
}
