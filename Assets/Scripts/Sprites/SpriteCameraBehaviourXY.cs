using UnityEngine;

public class SpriteCameraBehaviourXY : MonoBehaviour
{
    public Camera MainCamera;
    public Camera SideCamera;
    private Camera CodeCamera;

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
        transform.LookAt(CodeCamera.transform.position);
    }
}