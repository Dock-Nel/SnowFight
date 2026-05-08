using UnityEngine;

[CreateAssetMenu(fileName = "OneHP", menuName = "Scriptable Objects/OneHP")]
public class OneHP : DataDifficultRound
{
    float currentHealth;
    float currentMaxHealth;
    public override void ApplyEffect(PlayerController player)
    {
        currentHealth = player.Health;
        currentMaxHealth = player.MaxHealth;
        player.Health = 10;
        player.MaxHealth = 10;
    }
    public override void RevertEffect(PlayerController player)
    {
        player.Health = currentHealth; 
        player.MaxHealth = currentMaxHealth;
    }
}
