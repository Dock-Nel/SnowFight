using UnityEngine;

public class SpriteCameraBehaviourXY : MonoBehaviour
{
    public Camera Camera;

    void Update()
    {
        transform.LookAt(Camera.transform);
    }
}