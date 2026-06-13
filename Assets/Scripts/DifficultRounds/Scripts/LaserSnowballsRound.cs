using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "LaserSnowballsRound", menuName = "DifficultRound/LaserSnowballsRound")]
public class LaserSnowballsRound : DataDifficultRound
{
    private int originalShootVelocity;

    public override void ApplyEffect(PlayerController player, GameManager manager)
    {
      
        originalShootVelocity = player.ShootVelocity;
        player.ShootVelocity = 70;
        player.hasInfiniteSnowballs = true;

        for (int i = 0; i < 3; i++)
        {
            manager.SpawnBots();
        }
    }

    public override void RevertEffect(PlayerController player, GameManager manager)
    {
        player.ShootVelocity = originalShootVelocity;
        player.hasInfiniteSnowballs = false;
    }
}