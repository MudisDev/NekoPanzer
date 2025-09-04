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
    [SerializeField] float shootSpeed;
    [SerializeField] float shootCooldown;
    [SerializeField] int maxShoots;
    private int currentShoots;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private Vector2 playerDirection;
    private Vector2 turretDirection;
    private Vector2 playerVelocity;
    private Vector2 startPosition;

    [SerializeField] GameObject targetAmmo;
    [SerializeField] float targetDistance;

    const int MAXLIFE = 100;
    const int MINLIFE = 0;

    private bool canShoot;

    private void Awake()
    {
        this.rgbd = GetComponent<Rigidbody2D>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.boxCollider = GetComponent<BoxCollider2D>();

        this.startPosition = this.transform.position;



        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject);
            return;
        }

        sharedInstance = this;

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.playerVelocity = Vector2.zero;
        this.playerDirection = new Vector2(0, 1);
        this.canShoot = true;
        this.currentShoots = 0;

        this.transform.position = this.startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState != GameState.inGame)
            return;
        if (InputManager.sharedInstance.GetAttackButton() && this.canShoot && this.currentShoots < this.maxShoots)
        //if (Input.GetKeyDown(KeyCode.Space) && this.canShoot && this.currentShoots < this.maxShoots)
        {
            this.canShoot = false;
            StartCoroutine(Shoot());
        }

        if (this.playerLife <= 0)
        {
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
        yield return new WaitForSeconds(1);
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

        //this.targetAmmo.transform.position = new Vector3(gameObject.transform.position.x + this.targetDistance, gameObject.transform.position.y, gameObject.transform.position.z); 
    }

    Vector2 DirectionPlayer()
    {
        Vector2 playerMovement = InputManager.sharedInstance.GetMovement();
        Vector2 newTurretDirection = InputManager.sharedInstance.GetTurretMovement();

        // Solo actualiza si hay movimiento en la torreta
        if (newTurretDirection.magnitude > 0.1f)
        {
            this.turretDirection = newTurretDirection.normalized;
        }

        // Actualiza la posición del targetAmmo usando la última dirección válida
        this.targetAmmo.transform.position = (Vector2)this.transform.position + this.turretDirection * this.targetDistance;

        // Si el jugador está moviéndose, actualizamos la dirección de movimiento
        if (playerMovement.magnitude > 0.1f)
        {
            this.playerDirection = playerMovement.normalized;
        }

        return playerMovement;
    }


    public IEnumerator Shoot()
    {
        if (this.ammoPrefab != null)
        {
            GameObject newAmmoPrefab = Instantiate(ammoPrefab, transform.position, Quaternion.identity);
            newAmmoPrefab.GetComponent<AmmoController>().SetDirection(this.turretDirection);
            newAmmoPrefab.GetComponent<AmmoController>().SetEnum("player");
            newAmmoPrefab.GetComponent<AmmoController>().SetDamage(this.damage);
            newAmmoPrefab.GetComponent<AmmoController>().SetShootSpeed(this.shootSpeed);
            newAmmoPrefab.GetComponent<AmmoController>().SetPlayerController(this);
            this.currentShoots++;
        }
        else
            Debug.LogError("ammoPrefab no asinado");

        yield return new WaitForSeconds(this.shootCooldown);
        this.canShoot = true;
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Sand"))
        {
            this.speed /= 3;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Sand"))
            this.speed *= 3;
    }

    public void SetCurrentShoots()
    {
        this.currentShoots--;
        Debug.Log($"Disparos actuales -> {this.currentShoots}");
    }

    public Vector3 GetPlayerPosition()
    {
        return this.transform.position;
    }
}
