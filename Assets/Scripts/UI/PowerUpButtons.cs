using TMPro;
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
    [SerializeField]
    private TextMeshProUGUI LevelText;

    private AudioSource _GrabBonus1;
    private AudioSource _GrabBonus2;

    private void Start()
    {
        _GrabBonus1 = GameObject.Find("GrabBonus_1").GetComponent<AudioSource>();
        _GrabBonus2 = GameObject.Find("GrabBonus_2").GetComponent<AudioSource>();
    }

    public void Setup(DataPowerUp data)
    {
        currentData = data;
        Title.text = data.powerUpName;
        Description.text = data.powerUpDescription;
        //iconImage.sprite = data.icon;

        if (LevelText != null)
        {
            if (data.category == DataPowerUp.PoolType.Passif)
            {
                LevelText.gameObject.SetActive(true);
                LevelText.text = "Level " + data.GetCurrentLevel() + " / 3";
                LevelText.color = Color.cyan;
            }
            else
            {
                LevelText.gameObject.SetActive(false);
            }
        }
    }

    public void OnClick()
    {
        PlayerController player = Object.FindAnyObjectByType<PlayerController>();
        PowerUpManager manager = Object.FindAnyObjectByType<PowerUpManager>();

        if (player != null && currentData != null)
        {
            //override
            currentData.ApplyEffect(player);
            //Debug.Log("Effet appliqué : " + currentData.powerUpName);
            
            if (manager != null)
            {
                manager.NoMorePowerUps(currentData);
            }
        }
    }

    public void LeftChoice()
    {
        _GrabBonus1.Play();
    }

    public void RightChoice()
    {
        _GrabBonus2.Play();
    }
}