using UnityEngine;

[CreateAssetMenu(fileName = "DataDifficultRound", menuName = "Scriptable Objects/DataDifficultRound")]
public class DataDifficultRound : ScriptableObject
{
    public string DifficultRoundName;
    [TextArea] public string DifficultRoundDescription;

    public int ID;

    [Range(0, 100)]
    public float percentage;

    public virtual void ApplyEffect(PlayerController player, GameManager manager)
    {

    }

    public virtual void RevertEffect(PlayerController player, GameManager manager)
    {

    }
}