using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : Damageable, IPlayerSubject
{
    public float speed = 5f;
    public float jump = 5f;
    public float mouseSensitivity = 5f;
    private bool canJump = true;
    private float xRot;

    private Rigidbody rb;
    private AudioSource src;

    public GameObject playerCamera;
    public GameObject pistol;
    public GameObject bullets;
    public GrenadeMaker gm;
    public GameObject bulletSpawn;
    Camera cam;

    //Weapon variables
    public ParticleSystem muzzleFlash;
    public int gunDamage = 10;
    public float range = 100f;
    public float hitForce = 10f;

    public int normalGrenadeCount = 0;
    public int empGrenadeCount = 0;
    public int gravityWellGrenadeCount = 0;
    public bool carryingNormal = false;
    public bool carryingEMP = false;
    public bool carryingGravity = false;

    public Transform groundCheck;
    public LayerMask groundLayer;

    public AudioClip shoot;
    public AudioClip hit;

    public static PlayerController Instance;

    public bool resumed;

    List<IPlayerObserver> observers = new List<IPlayerObserver>();

    private PlayerData playerDataForObservers = new PlayerData();

    public TutorialScript tutorialscript;

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        src = GetComponent<AudioSource>();
        cam = GetComponentInChildren<Camera>();
        pistol = cam.transform.Find("Pistol").gameObject;

        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            tutorialscript = GameObject.FindGameObjectWithTag("TutorialManager").GetComponent<TutorialScript>();
        }
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        MouseLook();

        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (!carryingGravity && !carryingNormal && !carryingEMP)
        {
            pistol.SetActive(true);
            if (Input.GetMouseButtonDown(0) && resumed == true)
            {
                Shoot();
            }
        }
        else
        {
            pistol.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Y))
            TakeDamage(10);

        if (Time.timeScale == 0)
        {
            resumed = false;
        }
        else if (Time.timeScale == 1)
        {
            resumed = true;
        }
    }

    public void HoldGrenade(int type)
    {
        if(type == 1)
        {
            if(carryingNormal)
            {
                gm.GenerateGrenade(0);
            }
            else if(normalGrenadeCount > 0)
            {
                gm.GenerateGrenade(1);
                carryingNormal = true;
            }
        }
        else if(type == 2)
        {
            if (carryingEMP)
            {
                gm.GenerateGrenade(0);
            }
            else if (empGrenadeCount > 0)
            {
                gm.GenerateGrenade(2);
                carryingEMP = true;
            }
        }
        else if(type == 3)
        {
            if (carryingGravity)
            {
                gm.GenerateGrenade(0);
            }
            else if (gravityWellGrenadeCount > 0)
            {
                gm.GenerateGrenade(3);
                carryingGravity = true;
            }
        }
    }

    public void MovePlayer(float inputX, float inputZ)
    {
        Vector3 movement = (transform.right * inputX + transform.forward * inputZ) * speed;

        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }

    public void JumpPlayer()
    {
        if(IsGrounded())
        {
            rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
        }
    }

    private void MouseLook()
    {
        float y = Input.GetAxis("Mouse X") * mouseSensitivity;
        xRot += Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRot = Mathf.Clamp(xRot, -90, 90);

        playerCamera.transform.eulerAngles = new Vector3(-xRot, transform.eulerAngles.y, 0);
        transform.Rotate(0, y, 0);
    }

    void Shoot()
    {
        src.PlayOneShot(shoot);

        Quaternion bulletRot = bulletSpawn.transform.rotation;
        bulletRot.eulerAngles = new Vector3(bulletRot.eulerAngles.x - 0.5f, bulletRot.eulerAngles.y - 1.5f, bulletRot.eulerAngles.z);

        Instantiate(bullets, bulletSpawn.transform.position, bulletRot);

        /*
        RaycastHit hitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, range))
        {
            //Debug.Log(hitInfo.transform.gameObject.name);


            //Get the Target script off the hit object
            Enemy target =
            hitInfo.transform.gameObject.GetComponent<Enemy>();
            //If a target script was found, make the target take damage
            if (target != null)
            {
                //Instantiate(bullet, transformPos.transform);
                //target.SpawnBullet(hitInfo.point, cam.transform.rotation);
                target.TakeDamage(gunDamage);
            }

            //If the shot hits a Rigidbody, apply a force
            if (hitInfo.rigidbody != null)
            {
                hitInfo.rigidbody.AddForce(cam.transform.TransformDirection(Vector3.forward) * hitForce, ForceMode.Impulse);
            }
        } */

        //At the beginning of the Shoot() method, play the particle effect
        muzzleFlash.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("normal grenade pickup") || other.gameObject.CompareTag("emp grenade pickup") || other.gameObject.CompareTag("gravity well grenade pickup"))
            GameController.Instance.GrabGrenade(other.gameObject);
        else if (other.gameObject.CompareTag("GrenadeTrigger"))
        {
            tutorialscript.GrenadeTutorial();
            Destroy(other);
        }
        else if (other.gameObject.CompareTag("ButtonTrigger"))
        {
            tutorialscript.ButtonTutorial();
            Destroy(other);
        }
    }

    public void GrabGrenade(string tag)
    {
        if (tag.Equals("normal grenade pickup"))
            normalGrenadeCount++;
        else if (tag.Equals("emp grenade pickup"))
            empGrenadeCount++;
        else if (tag.Equals("gravity well grenade pickup"))
            gravityWellGrenadeCount++;
        NotifyPlayerObservers();
    }

    public override void TakeDamage(int damage)
    {
        src.PlayOneShot(hit);
        base.TakeDamage(damage);
        NotifyPlayerObservers();
    }

    public void RegisterPlayerObserver(IPlayerObserver observer)
    {
        observers.Add(observer);
        NotifyPlayerObservers();
    }

    public void RemovePlayerObserver(IPlayerObserver observer)
    {
        if (observers.Contains(observer))
            observers.Remove(observer);
    }

    public void NotifyPlayerObservers()
    {
        UpdatePlayerDataForObservers();
        foreach (IPlayerObserver observer in observers)
            observer.UpdateData(playerDataForObservers);
    }

    public void UpdatePlayerDataForObservers()
    {
        playerDataForObservers.PlayerHealth = currentHealth;
        playerDataForObservers.NormalGrenadeCount = normalGrenadeCount;
        playerDataForObservers.EMPGrenadeCount = empGrenadeCount;
        playerDataForObservers.GravityWellGrenadeCount = gravityWellGrenadeCount;
    }

    protected override void Destruction()
    {
        // HANDLE PLAYER DEATH HERE
    }

    private bool IsGrounded()
    {
        if (Physics.CheckSphere(groundCheck.transform.position, .1f, groundLayer))
        {
            // If the ray hits an object in the ground layer, the player is considered grounded
            return true;
        }
        return false;
    }
}
