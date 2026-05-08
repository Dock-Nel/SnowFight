using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Image toolIcon;

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

    private void Start()
    {
        toolIcon.enabled = false;
    }

    public void UpdateInventoryDisplay()
    {
        UpdateSlot(toolIcon, player.GetCurrentTool());
    }

    private void UpdateSlot(Image iconDisplay, DataPowerUp data)
    {
        if (data != null && data.icon != null)
        {
            toolIcon.enabled = true;
            iconDisplay.sprite = data.icon;
            iconDisplay.enabled = true; 
        }
        else
        {
            iconDisplay.enabled = false;
        }
    }
}