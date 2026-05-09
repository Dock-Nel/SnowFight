using UnityEngine;
using System.Collections;

public class SnowTurretEntity : MonoBehaviour
{
    public Rigidbody snowballPrefab;
    public Transform shootPoint;
    public float shootForce = 10f;
    public float upwardForce = 5f; 

    void Start()
    {
        StartCoroutine(ShootRoutine());
    }

    IEnumerator ShootRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            Shoot();
        }
    }

    void Shoot()
    {
        Rigidbody sb = Instantiate(snowballPrefab, shootPoint.position, shootPoint.rotation);
        sb.gameObject.SetActive(true);
        Vector3 force = (transform.forward * shootForce) + (Vector3.up * upwardForce);
        sb.AddForce(force, ForceMode.Impulse);
    }
}