using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSpeed", menuName = "PowerUps/PlayerSpeed")]
public class PlayerSpeed : DataPowerUp
{
    [Header("Progression")]
    public int[] bonusSteps = {2, 2, 2};

    [System.NonSerialized]
    public int currentLevel = 0;
    public override void ApplyEffect(PlayerController player)
    {
        if (currentLevel < bonusSteps.Length)
        {
            int bonus = bonusSteps[currentLevel];
            player.RunningSpeed += bonus;
            player.WalkingSpeed += bonus;
            currentLevel++;
        }
    }
    public override bool IsMaxedOut()
    {
        return currentLevel >= bonusSteps.Length;
    }
}
