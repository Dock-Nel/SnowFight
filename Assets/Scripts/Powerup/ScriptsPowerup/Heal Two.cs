using UnityEngine;

[CreateAssetMenu(fileName = "Heal Two", menuName = "PowerUps/HealTwo")]
public class HealTwo : DataPowerUp
{
    public override void ApplyEffect(PlayerController player)
    {
            player.Health += 20;
    }
}
