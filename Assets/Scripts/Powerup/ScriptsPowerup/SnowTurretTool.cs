using UnityEngine;

[CreateAssetMenu(fileName = "Snow Turret Tool", menuName = "PowerUps/Tool/Turret")]
public class SnowTurretTool : DataPowerUp
{
    public GameObject turretPrefab;

    public override void ApplyEffect(PlayerController player)
    {
        player.EquipTool(this);
    }

    public void PlaceTurret(PlayerController player)
    {
        player.SpawnTurret(turretPrefab);
    }
}