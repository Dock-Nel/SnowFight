using UnityEngine;

public class SpriteCameraBehaviourY : MonoBehaviour
{
    public Camera MainCamera;
    public Camera SideCamera;
    private Camera CodeCamera;

    [SerializeField] float TargetRotation;

    void Start()
    {
        if (MainCamera == null)
        {
            GameObject mainCamObj = GameObject.Find("Main Camera");
            if (mainCamObj != null) MainCamera = mainCamObj.GetComponent<Camera>();
        }

        if (SideCamera == null)
        {
            GameObject sideCamObj = GameObject.Find("Side Camera");
            if (sideCamObj != null) SideCamera = sideCamObj.GetComponent<Camera>();
        }
    }

    void Update()
    {
        if (MainCamera == null || SideCamera == null) return;

        if (MainCamera.isActiveAndEnabled == true)
        {
            CodeCamera = MainCamera;
        }
        else
        {
            CodeCamera = SideCamera;
        }
        TargetRotation = Mathf.Atan2(CodeCamera.transform.position.x - transform.position.x, CodeCamera.transform.position.z - transform.position.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, TargetRotation, 0f);
    }
}

