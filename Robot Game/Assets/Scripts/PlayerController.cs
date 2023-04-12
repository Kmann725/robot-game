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

    public GameObject playerCamera;
    public GrenadeMaker gm;

    public int normalGrenadeCount = 0;
    public int empGrenadeCount = 0;
    public int gravityWellGrenadeCount = 0;
    public bool carryingNormal = false;
    public bool carryingEMP = false;
    public bool carryingGravity = false;

    float inputX;
    float inputZ;

    public static PlayerController Instance;

    List<IPlayerObserver> observers = new List<IPlayerObserver>();

    private PlayerData playerDataForObservers = new PlayerData();

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        //transform.position += movement * speed * Time.deltaTime;

        MouseLook();

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.AddForce(Vector3.up * jump);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && normalGrenadeCount > 0 && !carryingNormal)
        {
            gm.GenerateGrenade(1);
            carryingNormal = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && empGrenadeCount > 0 && !carryingEMP)
        {
            gm.GenerateGrenade(2);
            carryingEMP = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && gravityWellGrenadeCount > 0 && !carryingGravity)
        {
            gm.GenerateGrenade(3);
            carryingGravity = true;
        }

        if (Input.GetKeyDown(KeyCode.Y))
            TakeDamage(10);
    }

    private void FixedUpdate()
    {
        Vector3 movement = (transform.right * inputX + transform.forward * inputZ) * speed;

        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }

    private void MouseLook()
    {
        float y = Input.GetAxis("Mouse X") * mouseSensitivity;
        xRot += Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRot = Mathf.Clamp(xRot, -90, 90);

        playerCamera.transform.eulerAngles = new Vector3(-xRot, transform.eulerAngles.y, 0);
        transform.Rotate(0, y, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            canJump = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            canJump = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("normal grenade pickup"))
        {
            normalGrenadeCount++;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("emp grenade pickup"))
        {
            empGrenadeCount++;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("gravity well grenade pickup"))
        {
            gravityWellGrenadeCount++;
            Destroy(other.gameObject);
        }
    }

    public override void TakeDamage(int damage)
    {
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
    }
}
