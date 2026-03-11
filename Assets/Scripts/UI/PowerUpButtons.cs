using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpButton : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descText;
    public Image iconImage;

    private DataPowerUp currentData;

    public void Setup(DataPowerUp data)
    {
        currentData = data;
        nameText.text = data.powerUpName;
        descText.text = data.description;
        iconImage.sprite = data.icon;
    }

    public void OnClick()
    {
        Debug.Log("Joueur a choisi : " + currentData.powerUpName);
    }
}