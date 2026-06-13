using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FoggyRound", menuName = "DifficultRound/FoggyRound")]
public class FoggyRound : DataDifficultRound
{
    private List<Light> disabledLights = new List<Light>();
    public override void ApplyEffect(PlayerController player, GameManager manager)
    {
        disabledLights.Clear();
        Light[] allLights = GameObject.FindObjectsByType<Light>(FindObjectsInactive.Exclude);
        Transform fogTransform = player.transform.Find("FogBubble");
        fogTransform.gameObject.SetActive(true);
        foreach (Light light in allLights)
        {
            if (light.enabled)
            {
                light.enabled = false;
                disabledLights.Add(light);
            }
        }
    }

    public override void RevertEffect(PlayerController player, GameManager manager)
    {
        Transform fogTransform = player.transform.Find("FogBubble");
        fogTransform.gameObject.SetActive(false);
        foreach (Light light in disabledLights)
        {
            if (light != null) 
            {
                light.enabled = true;
            }
        }

        disabledLights.Clear();
    }
}
