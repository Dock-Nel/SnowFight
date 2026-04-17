using UnityEngine;

[CreateAssetMenu(fileName = "InfiniteSnowballs", menuName = "PowerUps/Ephemeral/InfiniteSnowballs")]
public class InfiniteSnowball : DataPowerUp
{
    public float duration = 30f;

    public override void ApplyEffect(PlayerController player)
    {
       player.StartCoroutine(player.InfiniteSnowballsCoroutine(duration));
       player.EquipEphemeral(this);
    }
}