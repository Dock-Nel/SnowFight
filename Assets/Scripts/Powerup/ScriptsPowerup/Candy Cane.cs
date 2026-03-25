using UnityEngine;

[CreateAssetMenu(fileName = "Candy Cane", menuName = "PowerUps/CandyCane")]
public class CandyCane : DataPowerUp
{
    public override void ApplyEffect(PlayerController player)
    {
        Debug.Log("Nothing");
    }
}
