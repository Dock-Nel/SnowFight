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
        SnapToGround();
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
        Snowballs snowballScript = sb.GetComponent<Snowballs>();
        snowballScript.Source = "Player";
        Vector3 force = (transform.forward * shootForce) + (Vector3.up * upwardForce);
        sb.AddForce(force, ForceMode.Impulse);
    }

    void SnapToGround()
    {
        Collider myCollider = GetComponent<Collider>();
        myCollider.enabled = false;

        Vector3 rayStart = transform.position + Vector3.up * 2f;
        Ray ray = new Ray(rayStart, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 15f))
        {
            float offsetVersLeHaut = 0.67f;
            transform.position = hit.point + Vector3.up * offsetVersLeHaut;
        }
        myCollider.enabled = true;
    }
}