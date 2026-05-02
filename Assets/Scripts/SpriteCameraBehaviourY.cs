using UnityEngine;

public class SpriteCameraBehaviourY : MonoBehaviour
{
    public Camera Camera;

    [SerializeField] float TargetRotation;
    void Update()
    {
        TargetRotation = Mathf.Atan2(Camera.transform.position.x - transform.position.x, Camera.transform.position.z - transform.position.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, TargetRotation, 0f);
    }
}

