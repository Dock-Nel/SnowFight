using UnityEngine;

[CreateAssetMenu(fileName = "OneSnowball", menuName = "DifficultRound/OneSnowball")]
public class OneSnowball : DataDifficultRound
{
    int currentSnowball;
    int currentMaxSnowball;
    float CurrentbaseReloadDelay;
    public override void ApplyEffect(PlayerController player, GameManager manager)
    {
        currentSnowball = player.SnowballCount;
        currentMaxSnowball = player.MaxSnowball;
        CurrentbaseReloadDelay = player.baseReloadDelay;
        player.baseReloadDelay = 0.15f;
        player.SnowballCount = 1;
        player.MaxSnowball = 1;
    }
    public override void RevertEffect(PlayerController player, GameManager manager)
    {
        player.SnowballCount = currentSnowball;
        player.MaxSnowball = currentMaxSnowball;
        player.baseReloadDelay = CurrentbaseReloadDelay;
    }
}

