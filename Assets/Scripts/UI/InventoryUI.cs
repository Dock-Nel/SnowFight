using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    [SerializeField] private Image toolIcon;
    [SerializeField] private Image ephemeralIcon;

    [SerializeField] private Sprite emptySlotSprite;

    void OnEnable()
    {
        if (player != null)
            player.OnInventoryChanged += UpdateInventoryDisplay;
    }

    void OnDisable()
    {
        if (player != null)
            player.OnInventoryChanged -= UpdateInventoryDisplay;
    }

    void Start()
    {
        UpdateInventoryDisplay();
    }

    public void UpdateInventoryDisplay()
    {
        UpdateSlot(toolIcon, player.GetCurrentTool());
        UpdateSlot(ephemeralIcon, player.GetCurrentEphemeral());
    }

    private void UpdateSlot(Image iconDisplay, DataPowerUp data)
    {
        if (data != null && data.icon != null)
        {
            iconDisplay.sprite = data.icon;
            iconDisplay.enabled = true; 
        }
        else
        {
            if (emptySlotSprite != null)
            {
                iconDisplay.sprite = emptySlotSprite;
                iconDisplay.enabled = true;
            }
            else
            {
                iconDisplay.enabled = false;
            }
        }
    }
}