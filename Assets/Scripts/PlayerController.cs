using System.Collections;
using Mono.Cecil.Cil;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController sharedInstance;
    private Rigidbody2D rgbd;
    [SerializeField] float speed;
    [SerializeField] GameObject ammoPrefab;

    [SerializeField] int playerLife;
    [SerializeField] int damage;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private Vector2 playerDirection;
    private Vector2 playerVelocity;

    const int MAXLIFE = 100;
    const int MINLIFE = 0;

    private void Awake()
    {
        this.rgbd = GetComponent<Rigidbody2D>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.boxCollider = GetComponent<BoxCollider2D>();

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
        if (GameManager.sharedInstance.currentGameState != GameState.inGame)
            return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        if (this.playerLife <= 0)
        {
            //Destroy(gameObject);
            //gameObject.SetActive(false);
            DisablePlayer();
            StartCoroutine(GameOver());
        }
    }

    public void DisablePlayer()
    {
        this.spriteRenderer.enabled = false;
        this.boxCollider.enabled = false;
    }

    public IEnumerator GameOver()
    {
        //Debug.Log("Se entra a la corrutina");
        yield return new WaitForSeconds(1);
        //Debug.Log("Se inicia la corrutina");

        GameManager.sharedInstance.GameOver();
    }
    void FixedUpdate()
    {
        if (GameManager.sharedInstance.currentGameState != GameState.inGame)
            return;

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
                //Debug.Log("Izquierda");
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movement = new Vector2(1, 0);
            if (this.playerDirection != new Vector2(1, 0))
            {
                this.playerDirection = new Vector2(1, 0);
                //Debug.Log("Derecha");
            }
        }
        else if (Input.GetKey(KeyCode.W))
        {
            movement = new Vector2(0, 1);
            if (this.playerDirection != new Vector2(0, 1))
            {
                this.playerDirection = new Vector2(0, 1);
                //Debug.Log("Arriba");
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movement = new Vector2(0, -1);
            if (this.playerDirection != new Vector2(0, -1))
            {
                this.playerDirection = new Vector2(0, -1);
                //Debug.Log("Abajo");
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
            newAmmoPrefab.GetComponent<AmmoController>().SetEnum("player");
            newAmmoPrefab.GetComponent<AmmoController>().SetDamage(this.damage);
        }
        else
            Debug.LogError("ammoPrefab no asinado");
    }

    public void SetLife(int pointsLife)
    {
        this.playerLife -= pointsLife;
        this.playerLife = math.clamp(this.playerLife, MINLIFE, MAXLIFE);
        //Debug.Log($"playerlife -> {this.playerLife}");
    }

    public int GetLife()
    {
        return this.playerLife;
    }

}
