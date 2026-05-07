using UnityEngine;

public class SpriteCameraBehaviourY : MonoBehaviour
{
    public Camera MainCamera;
    public Camera SideCamera;
    private Camera CodeCamera;

    [SerializeField] float TargetRotation;
    void Update()
    {
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

