using UnityEngine;

public class SpriteCameraBehaviour : MonoBehaviour
{
    public Camera Camera;

    [SerializeField] float TargetRotation;
    void Update()
    {
        TargetRotation = Mathf.Atan2(transform.position.x - Camera.transform.position.x, transform.position.z - Camera.transform.position.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, TargetRotation, 0f);
    }
}
