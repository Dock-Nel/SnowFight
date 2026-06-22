using UnityEngine;

[CreateAssetMenu(fileName = "PlayerReload", menuName = "PowerUps/PlayerReload")]

public class PlayerReload : DataPowerUp
{
    [System.NonSerialized]
    public bool currentLevel = false;
    public override void ApplyEffect(PlayerController player)
    {
        player.ReloadTwo = true;
        currentLevel = true;  
    }
    public override bool IsMaxedOut()
    {
        return currentLevel == true;
    }
}
