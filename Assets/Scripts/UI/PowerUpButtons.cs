using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpButton : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    //public TextMeshProUGUI descText;
    //public Image iconImage;

    private DataPowerUp currentData;

    public void Setup(DataPowerUp data)
    {
        currentData = data;
        nameText.text = data.powerUpName;
        //descText.text = data.description;
        //iconImage.sprite = data.icon;
    }

    public void OnClick()
    {
        PlayerController player = Object.FindFirstObjectByType<PlayerController>();
        PowerUpManager manager = Object.FindFirstObjectByType<PowerUpManager>();

        if (player != null && currentData != null)
        {
            //override
            currentData.ApplyEffect(player);
            Debug.Log("Effet appliqué : " + currentData.powerUpName);
            
            if (manager != null)
            {
                manager.NoMorePowerUps(currentData);
            }
        }
    }
}