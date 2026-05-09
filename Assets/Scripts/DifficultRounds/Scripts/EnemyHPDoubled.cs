using UnityEngine;

[CreateAssetMenu(fileName = "EnemyHPDoubled", menuName = "DifficultRound/EnemyHPDoubled")]
public class EnemyHPDoubled : DataDifficultRound
{
    public override void ApplyEffect(PlayerController player, GameManager manager)
    {
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
        
    }
}

