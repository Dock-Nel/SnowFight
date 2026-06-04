using TMPro;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    public TextMeshProUGUI FPSCount;
    void Start()
    {
        InvokeRepeating("GetFPS", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            if (FPSCount.isActiveAndEnabled) { FPSCount.gameObject.SetActive(false); }
            else { FPSCount.gameObject.SetActive(true); }
        }
    }

    void GetFPS()
    {
        if (FPSCount.isActiveAndEnabled)
        {
            FPSCount.text = ((int)(1.0f / Time.smoothDeltaTime)).ToString();
        }
    }
}
