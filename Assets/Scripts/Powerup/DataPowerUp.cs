using UnityEngine;

[CreateAssetMenu(fileName = "DataPowerUp", menuName = "Scriptable Objects/DataPowerUp")]
public class DataPowerUp : ScriptableObject
{
    public string powerUpName;
    //[TextArea] public string description;
    //public Sprite icon;
    public enum PoolType { Passif, Tool, Ephemere }
    public PoolType category;

    [Range(0, 100)]
    public float percentage;

    public virtual void ApplyEffect(PlayerController player)
    {

    }

    public virtual bool IsMaxedOut()
    {
        return false;
    }
}
