using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class PowerUpButton : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    //public TextMeshProUGUI descText;
    //public Image iconImage;

    [SerializeField]
    private TextMeshProUGUI Title;
    [SerializeField]
    private TextMeshProUGUI Description;
    private DataPowerUp currentData;

    public void Setup(DataPowerUp data)
    {
        currentData = data;
        Title.text = data.powerUpName;
        Description.text = data.powerUpDescription;
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