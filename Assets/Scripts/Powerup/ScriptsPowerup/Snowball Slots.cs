using UnityEngine;

[CreateAssetMenu(fileName = "NewSnowballSlot", menuName = "PowerUps/SnowballSlot")]
public class SnowballSlotPowerUp : DataPowerUp
{
    [Header("Progression")]
    public int[] bonusSteps = { 2, 3, 4 };

    [System.NonSerialized] 
    public int currentLevel = 0;

    public override void ApplyEffect(PlayerController player)
    {
        
        if (currentLevel < bonusSteps.Length)
        {
            int bonus = bonusSteps[currentLevel];
            player.MaxSnowball += bonus; // Get Set
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