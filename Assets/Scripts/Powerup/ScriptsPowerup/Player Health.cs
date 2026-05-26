using UnityEngine;

[CreateAssetMenu(fileName = "PlayerHealth", menuName = "PowerUps/PlayerHealth")]
public class PlayerHealth : DataPowerUp
{
    [Header("Progression")]
    public int[] bonusSteps = {10, 10, 20};

    [System.NonSerialized]
    public int currentLevel = 0;
    public override void ApplyEffect(PlayerController player)
    {
        if (currentLevel < bonusSteps.Length)
        {
            int bonus = bonusSteps[currentLevel];
            player.MaxHealth += bonus;
            player.Health = player.MaxHealth;
            currentLevel++;
        }
    }
    public override bool IsMaxedOut()
    {
        return currentLevel >= bonusSteps.Length;
    }
    public override int GetCurrentLevel()
    {
        return currentLevel;
    }
}
