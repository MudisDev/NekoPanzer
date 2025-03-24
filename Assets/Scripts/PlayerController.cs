using Mono.Cecil.Cil;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController sharedInstance;
    private Rigidbody2D rgbd;
    [SerializeField] float speed;
    [SerializeField] GameObject ammoPrefab;

    [SerializeField] int playerLife;

    private Vector2 playerDirection;
    private Vector2 playerVelocity;


    private void Awake()
    {
        this.rgbd = GetComponent<Rigidbody2D>();

        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject);
            return;
        }

        sharedInstance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.playerVelocity = Vector2.zero;
        this.playerDirection = new Vector2(0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        if (this.playerLife <= 0)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {

        this.playerVelocity = DirectionPlayer();
        // Normaliza la dirección para evitar que diagonal sea más rápida
        this.playerVelocity = playerVelocity.normalized * speed;

        // Asigna directamente la velocidad en lugar de usar AddForce
        this.rgbd.linearVelocity = this.playerVelocity;

    }

    Vector2 DirectionPlayer()
    {
        Vector2 movement = Vector2.zero;

        if (Input.GetKey(KeyCode.A))
        {
            movement = new Vector2(-1, 0);
            if (this.playerDirection != new Vector2(-1, 0))
            {
                this.playerDirection = new Vector2(-1, 0);
                Debug.Log("Izquierda");
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movement = new Vector2(1, 0);
            if (this.playerDirection != new Vector2(1, 0))
            {
                this.playerDirection = new Vector2(1, 0);
                Debug.Log("Derecha");
            }
        }
        else if (Input.GetKey(KeyCode.W))
        {
            movement = new Vector2(0, 1);
            if (this.playerDirection != new Vector2(0, 1))
            {
                this.playerDirection = new Vector2(0, 1);
                Debug.Log("Arriba");
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movement = new Vector2(0, -1);
            if (this.playerDirection != new Vector2(0, -1))
            {
                this.playerDirection = new Vector2(0, -1);
                Debug.Log("Abajo");
            }
        }
        else
            movement = Vector2.zero;

        return movement;
    }

    void Shoot()
    {
        if (this.ammoPrefab != null)
        {
            GameObject newAmmoPrefab = Instantiate(ammoPrefab, transform.position, Quaternion.identity);
            newAmmoPrefab.GetComponent<AmmoController>().SetDirection(this.playerDirection);
        }

        else
            Debug.LogError("ammoPrefab no asinado");
    }

    public void SetLife(int pointsLife)
    {
        this.playerLife -= pointsLife;
        Debug.Log($"playerlife -> {this.playerLife}");
    }


}
