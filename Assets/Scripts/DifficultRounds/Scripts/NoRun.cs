using UnityEngine;

[CreateAssetMenu(fileName = "NoRun", menuName = "DifficultRound/NoRun")]
public class NoRun : DataDifficultRound
{
    public override void ApplyEffect(PlayerController player, GameManager manager)
    {
        player.CanRun = false;
    }
    public override void RevertEffect(PlayerController player, GameManager manager)
    {
        player.CanRun = true;
    }
}

