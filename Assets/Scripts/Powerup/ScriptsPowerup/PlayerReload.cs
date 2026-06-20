using UnityEngine;

[CreateAssetMenu(fileName = "PlayerReload", menuName = "PowerUps/PlayerReload")]

public class PlayerReload : DataPowerUp
{
    [System.NonSerialized]
    public bool currentLevel = false;
    public override void ApplyEffect(PlayerController player)
    {
        player.AmountToReload += 1;
        currentLevel = true;  
    }
    public override bool IsMaxedOut()
    {
        return currentLevel == true;
    }
}
