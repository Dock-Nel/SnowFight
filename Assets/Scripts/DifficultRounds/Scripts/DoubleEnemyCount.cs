using UnityEngine;

[CreateAssetMenu(fileName = "DoubleEnemyCount", menuName = "DifficultRound/DoubleEnemyCount")]
public class DoubleEnemyCount : DataDifficultRound
{
    public override void ApplyEffect(PlayerController player, GameManager manager)
    {
        foreach (GameObject bot in manager.Bots)
        {
            manager.SpawnSingleBot();
        }
    }
    public override void RevertEffect(PlayerController player, GameManager manager)
    {
        
    }
}