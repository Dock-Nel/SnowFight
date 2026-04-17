using UnityEngine;
using TMPro;
using System.Collections;
using UnityEditor.Experimental.GraphView;

public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;

    CharacterController characterController;

    [SerializeField]
    private float health = 30;
    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    public bool isInvincible = false;

    [SerializeField] private DataPowerUp currentTool;
    public DataPowerUp GetCurrentTool() => currentTool; //Similar as Get Set
    [SerializeField] private DataPowerUp currentEphemeral;
    public DataPowerUp GetCurrentEphemeral() => currentEphemeral;


    //New system, like ringing a bell - Not sure about using it right but i try.
    public System.Action OnInventoryChanged;

    [SerializeField]
    private float walkingSpeed = 7.5f;
    public float WalkingSpeed
    {
        get { return walkingSpeed; }
        set { walkingSpeed = value; }
    }

    [SerializeField]
    private float runningSpeed = 10f;
    public float RunningSpeed
    {
        get { return runningSpeed; }
        set { runningSpeed = value; }
    }

    [SerializeField]
    private float jumpSpeed = 8f;
    public float JumpSpeed
    {
        get { return jumpSpeed; }
        set { jumpSpeed = value; }
    }

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
    private Rigidbody biggerSnowball;
    [SerializeField]
    private int snowballCount = 3;
    [SerializeField]
    private int maxSnowball = 3;
    public int MaxSnowball
    {
        get { return maxSnowball; }
        set { maxSnowball = value; }
    }
    [SerializeField]
    private int shootVelocity = 10;
    public int ShootVelocity
    {
        get { return shootVelocity; }
        set { shootVelocity = value; }
    }
    private bool isReloading = false;

    public bool hasInfiniteSnowballs = false;

    //TMP

    [SerializeField]
    private TMP_Text tmpNbSnowballs;

    void Start()
    {
        //Hide cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
            //need to change to 1 or lower, but 0 for test
            yield return new WaitForSeconds(0f);
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
                if (Input.GetMouseButtonDown(1) && snowballCount < maxSnowball && isReloading == false)
                {
                    StartCoroutine(Wait());
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && snowballCount >= 1 && snowballCount <= maxSnowball)
        {
            Rigidbody clone;
            clone = Instantiate(snowball, shootingDisctrict.position, shootingDisctrict.rotation);
            clone.linearVelocity = playerCamera.transform.TransformDirection(Vector3.forward * shootVelocity);
            if(hasInfiniteSnowballs == false)
            {
                snowballCount -= 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && (hasInfiniteSnowballs || snowballCount >= 3))
        {
            Rigidbody clone;
            clone = Instantiate(biggerSnowball, shootingDisctrict.position, shootingDisctrict.rotation);
            clone.linearVelocity = playerCamera.transform.TransformDirection(Vector3.forward * 13);

            if (!hasInfiniteSnowballs)
            {
                snowballCount -= 3;
            }
        }

        if (Input.GetKeyDown(KeyCode.A) && currentEphemeral != null)
        {
            currentEphemeral.ApplyEffect(this);
            currentEphemeral = null; // Empty slot
        }

        //------------- Items ---------------

        if (Input.GetKeyDown(KeyCode.A) && currentEphemeral != null)
        {
            currentEphemeral.ApplyEffect(this);
            currentEphemeral = null;
            OnInventoryChanged?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.E) && currentTool != null)
        {
            currentTool.ApplyEffect(this);
        }

    }

    public IEnumerator InfiniteSnowballsCoroutine(float duration)
    {
        hasInfiniteSnowballs = true;
        tmpNbSnowballs.SetText("INFINY"); // à corriger c'est en dur pour le moment, ça me fait gagner du temps on va pas chipoter hein
        yield return new WaitForSeconds(duration);
        hasInfiniteSnowballs = false;
        tmpNbSnowballs.SetText(snowballCount.ToString());
    }

    public void TakeDamage(float damage)
    {

        if (isInvincible) return;

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
    public void EquipEphemeral(DataPowerUp powerUp)
    {
        currentEphemeral = powerUp;
        Debug.Log("Nouvel objet éphémère : " + powerUp.powerUpName);
        OnInventoryChanged?.Invoke();
    }
    public IEnumerator InvincibilityCoroutine(float duration)
    {
        isInvincible = true;
        Debug.Log("Début Invincibilité");

        yield return new WaitForSeconds(duration);

        isInvincible = false;
        Debug.Log("Fin Invincibilité");
    }

    //------------ Slots ------------

    public void EquipPowerUp(DataPowerUp powerUp)
    {
        if (powerUp.category == DataPowerUp.PoolType.Tool)
        {
            currentTool = powerUp;
        }
        else if (powerUp.category == DataPowerUp.PoolType.Ephemere)
        {
            currentEphemeral = powerUp;
        }
        OnInventoryChanged?.Invoke(); // Signal Update to UI
    }
}