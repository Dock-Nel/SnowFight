using UnityEngine;

[CreateAssetMenu(fileName = "ShotVelocity", menuName = "PowerUps/ShotVelocity")]
public class ShotVelocity : DataPowerUp
{
    [Header("Progression")]
    public int[] bonusSteps = { 3, 3, 4 };

    [System.NonSerialized] //With this, the currentLevel isn't save between games
    public int currentLevel = 0;

    public override void ApplyEffect(PlayerController player)
    {
        if (currentLevel < bonusSteps.Length)
        {
            int bonus = bonusSteps[currentLevel];
            player.ShootVelocity += bonus;
            currentLevel++;
        }
    }
    public override bool IsMaxedOut()
    {
        return currentLevel >= bonusSteps.Length;
    }
}
