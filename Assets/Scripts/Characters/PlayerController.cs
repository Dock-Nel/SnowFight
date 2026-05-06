using System.Collections;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;
    public Slider HealthBar;
    public Slider ReloadCooldown;

    CharacterController characterController;

    [SerializeField]
    private float health = 30;
    public float Health
    {
        get { return health; }
        set { health = value; }
    }
    [SerializeField]
    private float maxHealth = 30;
    public float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
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

    [SerializeField] float gravity = 20f;
    Vector3 moveDirection;
    private bool isRunning = false;
    float rotationX = 0;
    [SerializeField]
    public float rotationSpeed = 2.0f;
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
    [SerializeField]
    private float baseReloadDelay = 0.5f;
    private bool isFiringTool = false;
    private bool isReloading = false;

    public bool hasInfiniteSnowballs = false;

    private float toolCooldownTimer = 0f;

    //TOOLS
    [SerializeField]
    private GameObject gogglesVisualEffect; // Glisse ton "GogglesOverlay" ici
    [Header("Turret Settings")]
    private System.Collections.Generic.List<GameObject> activeTurrets = new System.Collections.Generic.List<GameObject>();
    [SerializeField]
    private float spawnDistance = 2f;


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
        HealthBar.value = Health;
        HealthBar.maxValue = MaxHealth;
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

        float speedMult = 1f;
        float jumpMult = 1f;

        if (currentTool is SnowSkis activeSkis)
        {
            speedMult = activeSkis.walkingSpeedMultiplier;
            jumpMult = activeSkis.jumpHeightMultiplier;
        }

        if (isRunning)
        {
            speedX = speedX * runningSpeed * speedMult;
            speedZ = speedZ * runningSpeed * speedMult;
        }
        else
        {
            speedX = speedX * walkingSpeed * speedMult;
            speedZ = speedZ * walkingSpeed * speedMult;
        }

        if (Input.GetButton("Jump") && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed * jumpMult;
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

        if (!hasInfiniteSnowballs)
            tmpNbSnowballs.SetText(snowballCount.ToString() + " / " + maxSnowball.ToString());
        else
            tmpNbSnowballs.SetText("INFINY");

        Vector3 fwd = playerCamera.transform.forward;
        RaycastHit hit;

        if (Physics.Raycast(shootingDisctrict.position, fwd, out hit, 2))
        {
            if (hit.collider.CompareTag("Snow"))
            {
                if (Input.GetMouseButtonDown(1) && snowballCount < maxSnowball && !isReloading)
                {
                    StartCoroutine(ReloadWait());
                }
            }
        }

        Debug.DrawRay(
            shootingDisctrict.position,
            fwd * 3,
            Color.red
        );

        if (Input.GetMouseButtonDown(0) && !isFiringTool)
        {
            if (currentTool is SnowCanon canon)
            {
                if (Time.timeScale != 0)
                {
                    if (hasInfiniteSnowballs || snowballCount >= canon.ammoCost)
                    {
                        StartCoroutine(FireCanonRoutine(canon));
                    }
                }  
            }
            else if (hasInfiniteSnowballs || snowballCount >= 1)
            {
                if (Time.timeScale != 0)
                {
                    FireSingleSnowball();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && (hasInfiniteSnowballs || snowballCount >= 3))
        {
            if (Time.timeScale != 0)
            {
                FireBigSnowball();
            }                
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
            if (currentTool is SnowTurretTool turretTool)
            {
                turretTool.PlaceTurret(this);
            }
            else if (currentTool is SkiGoggles goggles)
            {
                if (toolCooldownTimer <= 0)
                {
                    goggles.Activate(this);
                    toolCooldownTimer = goggles.cooldown;
                    Debug.Log("Lunettes de ski activées ! Invincible pour 5s.");
                }
                else
                {
                    Debug.Log("Lunettes en recharge... " + Mathf.Ceil(toolCooldownTimer) + "s restantes.");
                }
            }
            else
            {
                currentTool.ApplyEffect(this);
            }
        }

        if (toolCooldownTimer > 0)
        {
            toolCooldownTimer -= Time.deltaTime;
        }
    }

    void FireSingleSnowball()
    {
        Rigidbody clone = Instantiate(snowball, shootingDisctrict.position, shootingDisctrict.rotation);
        clone.gameObject.SetActive(true);
        Snowballs SnowballScript = clone.GetComponent<Snowballs>();
        SnowballScript.Source = "Player";
        clone.linearVelocity = playerCamera.transform.TransformDirection(Vector3.forward * shootVelocity);
        if (!hasInfiniteSnowballs) snowballCount -= 1;
    }

    void FireBigSnowball()
    {
        Rigidbody clone = Instantiate(biggerSnowball, shootingDisctrict.position, shootingDisctrict.rotation);
        clone.gameObject.SetActive(true);
        clone.linearVelocity = playerCamera.transform.TransformDirection(Vector3.forward * 13);
        if (!hasInfiniteSnowballs) snowballCount -= 3;
    }

    IEnumerator FireCanonRoutine(SnowCanon canon)
    {
        isFiringTool = true;

        if (!hasInfiniteSnowballs) snowballCount -= canon.ammoCost;

        for (int i = 0; i < canon.ballsPerShot; i++)
        {
            Rigidbody clone = Instantiate(snowball, shootingDisctrict.position, shootingDisctrict.rotation);
            clone.gameObject.SetActive(true);
            clone.linearVelocity = playerCamera.transform.TransformDirection(Vector3.forward * shootVelocity);
            yield return new WaitForSeconds(canon.delayBetweenBalls);
        }

        isFiringTool = false;
    }

    IEnumerator ReloadWait()
    {
        isReloading = true;

        float delay = baseReloadDelay;
        if (currentTool is SnowSkis skis)
        {
            delay = skis.reloadDelay;
        }

        ReloadCooldown.maxValue = delay;
        ReloadCooldown.value = delay;
        ReloadCooldown.gameObject.SetActive(true);

        while (ReloadCooldown.value > 0)
        {
            ReloadCooldown.value -= delay/100;
            yield return new WaitForSeconds(delay/100);
        }

        ReloadCooldown.gameObject.SetActive(false);

        int amountToReload = 1;
        if (currentTool is SnowShovel shovel) amountToReload = shovel.reloadAmount;

        snowballCount = Mathf.Min(snowballCount + amountToReload, maxSnowball);
        isReloading = false;
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
        SceneManager.LoadScene("Main Menu");
    }
    public void EquipTool(DataPowerUp powerUp)
    {
        ClearAllTurrets();

        currentTool = powerUp;
        OnInventoryChanged?.Invoke();
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

        if (gogglesVisualEffect != null && currentTool is SkiGoggles)
        {
            gogglesVisualEffect.SetActive(true);
        }

        yield return new WaitForSeconds(duration);

        if (gogglesVisualEffect != null)
        {
            gogglesVisualEffect.SetActive(false);
        }

        isInvincible = false;
    }

    public void SpawnTurret(GameObject prefab)
    {
        //Position devant joueur
        Vector3 spawnPos = transform.position + transform.forward * spawnDistance;
        spawnPos.y = transform.position.y; 

        GameObject newTurret = Instantiate(prefab, spawnPos, transform.rotation);
        activeTurrets.Add(newTurret);

        if (activeTurrets.Count > 3)
        {
            Destroy(activeTurrets[0]);
            activeTurrets.RemoveAt(0);
        }
    }

    private void ClearAllTurrets()
    {
        foreach (GameObject t in activeTurrets)
        {
            if (t != null) Destroy(t);
        }
        activeTurrets.Clear();
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