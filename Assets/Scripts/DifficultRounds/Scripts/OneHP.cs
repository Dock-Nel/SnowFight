using UnityEngine;

[CreateAssetMenu(fileName = "OneHP", menuName = "DifficultRound/OneHP")]
public class OneHP : DataDifficultRound
{
    float currentHealth;
    float currentMaxHealth;
    public override void ApplyEffect(PlayerController player, GameManager manager)
    {
        currentHealth = player.Health;
        currentMaxHealth = player.MaxHealth;
        player.Health = 10;
        player.MaxHealth = 10;
    }
    public override void RevertEffect(PlayerController player, GameManager manager)
    {
        player.Health = currentHealth; 
        player.MaxHealth = currentMaxHealth;
    }
}
