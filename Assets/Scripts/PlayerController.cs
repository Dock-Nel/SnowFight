using UnityEngine;
using TMPro;
using System.Collections;
using UnityEditor.Experimental.GraphView;

public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;

    CharacterController characterController;

    public float health = 30;
    [SerializeField]
    private float walkingSpeed = 7.5f;
    [SerializeField]
    private float runningSpeed = 15f;
    [SerializeField]
    private float jumpSpeed = 8f;
    float gravity = 20f;
    Vector3 moveDirection;
    private bool isRunning = false;
    float rotationX = 0;
    [SerializeField]
    private float rotationSpeed = 2.0f;
    [SerializeField]
    private float rotationXLimit = 45.0f;

    //Snowballs managment
    [SerializeField]
    private Transform shootingDisctrict;
    [SerializeField]
    private Rigidbody snowball;
    [SerializeField]
    private int snowballCount = 3;
    [SerializeField]
    private int maxSnowball = 3;
    private bool isReloading = false;

    //TMP

    [SerializeField]
    private TMP_Text tmpNbSnowballs;

    void Start()
    {
        //Hide cursor
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        //------------Movements------------

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float speedZ = Input.GetAxis("Vertical");
        float speedX = Input.GetAxis("Horizontal");
        float speedY = moveDirection.y;


        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        if (isRunning)
        {
            speedX = speedX * runningSpeed;
            speedZ = speedZ * runningSpeed;
        }
        else
        {
            speedX = speedX * walkingSpeed;
            speedZ = speedZ * walkingSpeed;
        }

        moveDirection = forward * speedZ + right * speedX;

        if (Input.GetButton("Jump") && characterController.isGrounded)
        {

            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = speedY;
        }


        //If the player don't touch the ground
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        //Camera Rotation
        rotationX += -Input.GetAxis("Mouse Y") * rotationSpeed;
        rotationXLimit = 80f;
        rotationX = Mathf.Clamp(rotationX, -rotationXLimit, rotationXLimit);

        //Up/down rotation camera
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        //right/left mouse
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * rotationSpeed, 0);


        //------------Shooting------------
        IEnumerator Wait()
        {
            isReloading = true;
            yield return new WaitForSeconds(1f);
            snowballCount += 1;
            isReloading = false;
        }

        tmpNbSnowballs.SetText(snowballCount.ToString());

        Vector3 fwd = playerCamera.transform.forward;
        RaycastHit hit;

        Debug.DrawRay(
            shootingDisctrict.position,
            fwd * 3,
            Color.red
        );

        if (Physics.Raycast(shootingDisctrict.position, fwd, out hit, 2))
        {
            if (hit.collider.CompareTag("Snow"))
            {
                Debug.Log("R to reload");
                if (Input.GetKeyDown(KeyCode.R) && snowballCount < maxSnowball && isReloading == false)
                {
                    StartCoroutine(Wait());
                    
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && snowballCount is >= 1 and <= 3)
        {
            snowballCount -= 1;
            Rigidbody clone;
            clone = Instantiate(snowball, shootingDisctrict.position, shootingDisctrict.rotation);
            clone.linearVelocity = playerCamera.transform.TransformDirection(Vector3.forward * 10);
        }

        //else if (snowballCount is <= 1)
        //{
        //    Debug.Log("No More Snowballs");
        //}


        //------------Player actions------------

    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + " health is now at: " + health);

        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}