using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[CreateAssetMenu(fileName = "InvertedControlsRound", menuName = "DifficultRound/InvertedControlsRound")]
public class InvertedControlsRound : DataDifficultRound
{
    public override void ApplyEffect(PlayerController player, GameManager manager)
    {
        player.inputMultiplier = -1f;
    }

    public override void RevertEffect(PlayerController player, GameManager manager)
    {
        player.inputMultiplier = 1f;
    }
}