using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    SettingsValues Settings;
    float CurrentSensitivity;
    public List<float> snowList;
    public int posX;
    public int posZ;
    public Terrain Terrain;
    public Image Crosshair;
    public Camera playerCamera;
    public Slider HealthBar;
    public Image HealthBarInside;
    public Slider ReloadCooldown;
    public Slider ToolCooldown;
    public GameObject Shadow;
    public GameObject DamageFilter;

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
    public bool CanRun = true;

    [SerializeField]
    private float jumpSpeed = 8f;
    public float JumpSpeed
    {
        get { return jumpSpeed; }
        set { jumpSpeed = value; }
    }

    [SerializeField]
    private int amountToReload = 1;
    public int AmountToReload
    {
        get { return amountToReload; }
        set { amountToReload = value; }
    }
    public bool ReloadTwo;

    [SerializeField] float gravity = 20f;
    Vector3 moveDirection;
    private bool isRunning = false;
    float rotationX = 0;
    [SerializeField]
    public float rotationSpeed = 2.0f;
    [SerializeField]
    private float rotationXLimit = 45.0f;
    public float inputMultiplier = 1f;

    //Snowballs managment
    [SerializeField]
    private Transform shootingDistrict;
    [SerializeField]
    private Transform shootingDistrictSC;
    private Transform activeShootingDistrict;
    [SerializeField]
    private Rigidbody snowball;
    [SerializeField]
    private Rigidbody biggerSnowball;
    [SerializeField]
    private int snowballCount = 3;

    public int SnowballCount
    {
        get { return snowballCount; }
        set { snowballCount = value; }
    }

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
    public float baseReloadDelay = 0.5f;
    private bool isFiringTool = false;
    private bool isReloading = false;
    private bool isDamaged = false;
    private bool Cooldown;

    public bool hasInfiniteSnowballs = false;
    public bool InfiniteSnowballsRound = false;

    public float toolCooldownTimer = 0f;

    //TOOLS
    [SerializeField]
    private GameObject gogglesVisualEffect; 

    [SerializeField]
    private GameObject Turret;
    [Header("Turret Settings")]
    private System.Collections.Generic.List<GameObject> activeTurrets = new System.Collections.Generic.List<GameObject>();
    [SerializeField]
    private float spawnDistance = 2f;

    //TMP
    [SerializeField]
    public TMP_Text tmpNbSnowballs;

    //MODELS
    [SerializeField]
    private GameObject snowCanonModel;

    private AudioManager audioManager;

    void Start()
    {
        //Hide cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();

        Settings = FindAnyObjectByType<SettingsValues>();
        audioManager = Object.FindAnyObjectByType<AudioManager>();
        activeShootingDistrict = shootingDistrict;
        Cooldown = false;
    }

    void Update()
    {
        HealthBar.value = Health;
        HealthBar.maxValue = MaxHealth;
        if (Settings == null)
        {
            Debug.Log("No Settings Found !");
            Settings = FindAnyObjectByType<SettingsValues>();
        }
        else if (CurrentSensitivity != Settings.SensitivityValue)
        {
            rotationSpeed = Settings.SensitivityValue;
            CurrentSensitivity = Settings.SensitivityValue;
        }
        


        if (isInvincible)
        {
            HealthBarInside.color = new Color(255,214,00); //Yellow
        }
        else
        {
            HealthBarInside.color = new Color(00,241,255); //Blue
        }
        Ray downRay = new Ray(new Vector3(this.transform.position.x, this.transform.position.y - 1, this.transform.position.z), -Vector3.up);
        RaycastHit hitShadow;
        
        if (Physics.Raycast(downRay, out hitShadow))
        {
            Vector3 hitPosition = hitShadow.point;
            hitPosition.y += 0.05f;
            Shadow.transform.position = hitPosition;
        }


        //------------Movements------------

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float speedZ = Input.GetAxis("Vertical") * inputMultiplier;
        float speedX = Input.GetAxis("Horizontal") * inputMultiplier;
        float speedY = moveDirection.y;

        if (Input.GetKey(KeyCode.LeftShift) && CanRun)
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


        //If the player doesn't touch the ground
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
            tmpNbSnowballs.SetText("INFINITY");

        Vector3 fwd = playerCamera.transform.forward;
        RaycastHit hit;

        if (Physics.Raycast(shootingDistrict.position, fwd, out hit, 2.5f))
        {
            if (hit.collider.name == "Terrain")
            {
                Terrain = hit.collider.GetComponent<Terrain>();
                Vector3 terrainPosition = hit.point - Terrain.transform.position;
                Vector3 mapPosition = new Vector3(terrainPosition.x / Terrain.terrainData.size.x, 0, terrainPosition.z / Terrain.terrainData.size.z);
                float xCoord = mapPosition.x * Terrain.terrainData.alphamapWidth;
                float zCoord = mapPosition.z * Terrain.terrainData.alphamapHeight;
                posX = (int)xCoord;
                posZ = (int)zCoord;
                snowList = new List<float>();
                float[,,] splatMap = Terrain.terrainData.GetAlphamaps(posX, posZ, 1, 1);

                snowList.Add(splatMap[0, 0, 0]);
                snowList.Add(splatMap[0, 0, 1]);
                Debug.Log(snowList.IndexOf(snowList.Max()));

                if (snowList.IndexOf(snowList.Max()) == 0)
                {
                    Crosshair.color = Color.green;
                    if (Input.GetMouseButtonDown(1) && snowballCount < maxSnowball && !isReloading)
                    {
                        StartCoroutine(ReloadWait());
                    }
                }
                else
                {
                    Crosshair.color = Color.white;
                }
            }
            else if (hit.collider.CompareTag("Snow"))
            {
                Crosshair.color = Color.green;
                if (Input.GetMouseButtonDown(1) && snowballCount < maxSnowball && !isReloading)
                {
                    StartCoroutine(ReloadWait());
                }
            }
            else
            {
                Crosshair.color = Color.white;
            }
        }
        else
        {
            Crosshair.color = Color.white;
        }

        if (Physics.Raycast(shootingDistrict.position, fwd, out hit, 20f))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Crosshair.color = Color.red;
            }
        }
        else
        {
            Crosshair.color = Color.white;
        }

        /*Debug.DrawRay(
            activeShootingDistrict.position,
            fwd * 3,
            Color.red
        );*/

        if (Input.GetMouseButtonDown(0) && !isFiringTool)
        {
            if (currentTool is SnowCanon canon)
            {
                if (Time.timeScale != 0)
                {
                    if (hasInfiniteSnowballs || snowballCount >= 1)
                    {
                        audioManager.PlaySnowballShotRandomPitch();
                        FireCanonRoutine(canon);
                    }
                }
            }
            else if (hasInfiniteSnowballs || snowballCount >= 1)
            {
                if (Time.timeScale != 0)
                {
                    audioManager.PlaySnowballShotRandomPitch();
                    FireSingleSnowball();
                }
            }
        }

        /*if (Input.GetKeyDown(KeyCode.Alpha2) && (hasInfiniteSnowballs || snowballCount >= 3))
        {
            if (Time.timeScale != 0)
            {
                FireBigSnowball();
            }                
        }*/


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
                if (toolCooldownTimer <= 0)
                {
                    turretTool.PlaceTurret(this);
                }
            }
            else if (currentTool is SkiGoggles goggles)
            {
                if (toolCooldownTimer <= 0)
                {
                    goggles.Activate(this);
                    toolCooldownTimer = goggles.cooldown;
                }
            }
            else
            {
                currentTool.ApplyEffect(this);
            }
        }

        if (toolCooldownTimer > 0 && !Cooldown)
        {
            StartCoroutine(ItemCooldown());
        }
    }

    void FireSingleSnowball()
    {
        Rigidbody clone = Instantiate(snowball, activeShootingDistrict.position, activeShootingDistrict.rotation);
        Physics.IgnoreCollision(this.GetComponent<Collider>(), clone.GetComponent<Collider>(), true);
        Snowballs SnowballScript = clone.GetComponent<Snowballs>();
        SnowballScript.Source = "Player";
        clone.linearVelocity = playerCamera.transform.TransformDirection(Vector3.forward * shootVelocity);
        clone.gameObject.SetActive(true);
        if (!hasInfiniteSnowballs) snowballCount -= 1;
    }

    IEnumerator ItemCooldown()
    {
        Cooldown = true;
        ToolCooldown.maxValue = toolCooldownTimer;
        ToolCooldown.value = toolCooldownTimer;
        ToolCooldown.gameObject.SetActive(true);
        while (toolCooldownTimer > 0)
        {
            yield return null;
            toolCooldownTimer -= Time.deltaTime;
            ToolCooldown.value -= Time.deltaTime;
        }
        ToolCooldown.gameObject.SetActive(false);
        Cooldown = false;
    }

    void FireCanonRoutine(SnowCanon canon)
    {
        Rigidbody clone = Instantiate(biggerSnowball, activeShootingDistrict.position, activeShootingDistrict.rotation);
        Physics.IgnoreCollision(this.GetComponent<Collider>(), clone.GetComponent<Collider>(), true);
        BigSnowballs BigSnowballScript = clone.GetComponent<BigSnowballs>();
        BigSnowballScript.Source = "Player";
        clone.linearVelocity = playerCamera.transform.TransformDirection(Vector3.forward * (shootVelocity+2));
        clone.gameObject.SetActive(true);
        if (!hasInfiniteSnowballs) snowballCount -= 1;
    }

    IEnumerator ReloadWait()
    {
        if (Time.timeScale == 0) { yield break; }
        isReloading = true;
        
        audioManager.PlaySnowballReloadRandomPitch();

        float delay = baseReloadDelay;
        if (currentTool is SnowSkis skis)
        {
            delay = skis.reloadDelay;
        }

        ReloadCooldown.maxValue = delay;
        ReloadCooldown.value = delay;
        ReloadCooldown.gameObject.SetActive(true);

        float time = 0;

        while (time < delay)
        {
            yield return null;
            time += Time.deltaTime;
            ReloadCooldown.value -= Time.deltaTime;
        }

        ReloadCooldown.gameObject.SetActive(false);


        if (currentTool is SnowShovel shovel) amountToReload = shovel.reloadAmount;
        else if (ReloadTwo) amountToReload = 2;
        else amountToReload = 1;

        snowballCount = Mathf.Min(snowballCount + amountToReload, maxSnowball);
        isReloading = false;
    }

    public IEnumerator InfiniteSnowballsCoroutine(float duration)
    {
        hasInfiniteSnowballs = true;
        tmpNbSnowballs.SetText("INFINITY"); // � corriger c'est en dur pour le moment, �a me fait gagner du temps on va pas chipoter hein
        yield return new WaitForSeconds(duration);
        if (!InfiniteSnowballsRound)
        {
            hasInfiniteSnowballs = false;
            tmpNbSnowballs.SetText(snowballCount.ToString());
        }
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible || isDamaged) return;

        StartCoroutine(TakeDamageCoroutine(damage));
        Debug.Log(gameObject.name + " health is now at: " + health);
    }

    public IEnumerator TakeDamageCoroutine(float damage)
    {
        audioManager.PlayShotHigherPitch();
        isDamaged = true;
        Health -= damage;
        DamageFilter.SetActive(true);
        yield return new WaitForSeconds(1f);
        isDamaged = false;
        DamageFilter.SetActive(false);
    }

    public void EquipTool(DataPowerUp powerUp)
    {
        ClearAllTurrets();

        currentTool = powerUp;
       

        if (currentTool is SnowCanon)
        {
            activeShootingDistrict = shootingDistrictSC;
            snowCanonModel.SetActive(true);
        }
        else
        {
            activeShootingDistrict = shootingDistrict;
            snowCanonModel.SetActive(false);
        }

        OnInventoryChanged?.Invoke();
    }

    //public void EquipEphemeral(DataPowerUp powerUp)
    //{
    //    currentEphemeral = powerUp;
    //    OnInventoryChanged?.Invoke();
    //}

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

    public void SpawnTurret(SnowTurretTool turretTool)
    {
        if (activeTurrets.Count >= 3)
        {
            foreach (GameObject t in activeTurrets)
            {
                if (t != null) Destroy(t);
            }
            activeTurrets.Clear();
        }

        //Position devant joueur
        Vector3 spawnPos = transform.position + transform.forward * spawnDistance;
        spawnPos.y = transform.position.y; 

        GameObject newTurret = Instantiate(Turret, spawnPos, transform.rotation);
        newTurret.SetActive(true);
        activeTurrets.Add(newTurret);

        if (activeTurrets.Count == 3)
        {
            toolCooldownTimer = turretTool.cooldown;
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
        //else if (powerUp.category == DataPowerUp.PoolType.Ephemere)
        //{
        //    currentEphemeral = powerUp;
        //}
        OnInventoryChanged?.Invoke(); // Signal Update to UI
    }
}