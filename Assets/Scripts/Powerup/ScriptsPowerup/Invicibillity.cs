using UnityEngine;

[CreateAssetMenu(fileName = "Invincibility", menuName = "PowerUps/Ephemeral/Invincibility")]
public class EphemeralInvincibility : DataPowerUp
{
    public float duration = 20f;

    public override void ApplyEffect(PlayerController player)
    {
        player.StartCoroutine(player.InvincibilityCoroutine(duration));
    }
}