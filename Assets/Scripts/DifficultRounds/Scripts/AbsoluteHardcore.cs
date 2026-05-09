using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "AbsoluteHardcore", menuName = "DifficultRound/AbsoluteHardcore")]
public class AbsoluteHardcore : DataDifficultRound
{
    float currentHealth;
    float currentMaxHealth;

    int currentSnowball;
    int currentMaxSnowball;

    public override void ApplyEffect(PlayerController player, GameManager manager)
    {
        currentHealth = player.Health;
        currentMaxHealth = player.MaxHealth;
        player.Health = 10;
        player.MaxHealth = 10;

        currentSnowball = player.SnowballCount;
        currentMaxSnowball = player.MaxSnowball;
        player.SnowballCount = 1;
        player.MaxSnowball = 1;

        manager.SpawnBots();

        foreach (GameObject bot in manager.Bots)
        {
            BotController controller = bot.GetComponent<BotController>();
            if (controller != null)
            {
                controller.health *= 2;
            }
        }
    }
    public override void RevertEffect(PlayerController player, GameManager manager)
    {
        player.Health = currentHealth;
        player.MaxHealth = currentMaxHealth;

        player.SnowballCount = currentSnowball;
        player.MaxSnowball = currentMaxSnowball;
    }
}

