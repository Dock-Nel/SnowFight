    using UnityEngine;

    [CreateAssetMenu(fileName = "Ski Goggles", menuName = "PowerUps/Tool/Ski Goggles")]
    public class SkiGoggles : DataPowerUp
    {
        public float invincibilityDuration = 5f;
        public float cooldown = 40f;

        public override void ApplyEffect(PlayerController player)
        {
            player.EquipTool(this);
        }

        public void Activate(PlayerController player)
        {
            player.StartCoroutine(player.InvincibilityCoroutine(invincibilityDuration));
        }
    }