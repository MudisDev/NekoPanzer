using System.Collections;


//using System.Numerics;
//using Mono.Cecil.Cil;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController sharedInstance;
    private Rigidbody2D rgbd;
    [SerializeField] float speed;
    [SerializeField] GameObject ammoPrefab;
    [SerializeField] GameObject ammoOrigin;

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
    [SerializeField] GameObject tankTurret;
    [SerializeField] GameObject tankBody;
    [SerializeField] float targetDistance;
    private Animator animatorTrack;


    const int MAXLIFE = 100;
    const int MINLIFE = 0;

    private bool canShoot;

    private void Awake()
    {
        this.rgbd = GetComponent<Rigidbody2D>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.boxCollider = GetComponent<BoxCollider2D>();
        this.animatorTrack = GetComponent<Animator>();

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

        this.targetAmmo.transform.position = (Vector2)this.tankTurret.transform.position + new Vector2(0, 1) * this.targetDistance;
        this.turretDirection = new Vector2(0, 1);

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState != GameState.inGame)
            return;
        //if (InputManager.sharedInstance.GetAttackButton() && this.canShoot && this.currentShoots < this.maxShoots && EstaApuntado())
        if (this.canShoot && this.currentShoots < this.maxShoots && EstaApuntado())

        {

            if (InputManager.sharedInstance.GetTurretJoystickShoot())
            {

                Vector2 turretInput = InputManager.sharedInstance.GetTurretMovement();

                if (turretInput.magnitude > 0.1f)
                {
                    this.canShoot = false;
                    StartCoroutine(Shoot());
                }
            }
            if (InputManager.sharedInstance.GetAttackButton())
            {
                this.canShoot = false;
                StartCoroutine(Shoot());
            }

        }

        if (this.playerLife <= 0)
        {
            DisablePlayer();
            StartCoroutine(GameOver());
        }

        //this.targetAmmo.transform.position = (Vector2)this.transform.position + this.turretDirection * this.targetDistance;


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

        //Debug.Log("Esta apuntando => " + EstaApuntado());
        //        Debug.Log($"turret direction -> {this.turretDirection}");

        this.animatorTrack.speed = (this.playerVelocity.magnitude > 0.1f) ? 1 : 0;
    }

    bool EstaApuntado()
    {
        if (this.transform.position == this.targetAmmo.transform.position)
        {
            return false;
        }
        return true;
    }

    private float bodyAngle; // fuera de la función, como variable de clase

    Vector2 DirectionPlayer()
    {
        Vector2 playerMovement = InputManager.sharedInstance.GetMovement();
        Vector2 newTurretDirection = InputManager.sharedInstance.GetTurretMovement();

        // --- Rotación de la torreta (esto está bien como lo tienes) ---
        if (newTurretDirection.magnitude > 0.2f)
        {
            this.turretDirection = newTurretDirection.normalized;
            float targetAngle = Mathf.Atan2(this.turretDirection.y, this.turretDirection.x) * Mathf.Rad2Deg;

            float smoothAngle = Mathf.LerpAngle(
                this.tankTurret.transform.eulerAngles.z,
                targetAngle - 90f,
                Time.deltaTime * 15f
            );

            this.tankTurret.transform.rotation = Quaternion.Euler(0, 0, smoothAngle);
        }

        // --- Movimiento y rotación del cuerpo ---
        float deadZone = 0.2f;
        if (playerMovement.magnitude > deadZone)
        {
            this.playerDirection = playerMovement.normalized;
            float targetAngle = Mathf.Atan2(this.playerDirection.y, this.playerDirection.x) * Mathf.Rad2Deg - 90f;

            // Aquí sí usamos bodyAngle como acumulador del ángulo actual
            bodyAngle = Mathf.LerpAngle(bodyAngle, targetAngle, Time.deltaTime * 15f); // 5f se siente fluido sin ser lento
            this.tankBody.transform.rotation = Quaternion.Euler(0, 0, bodyAngle);
        }

        // Actualiza posición del targetAmmo
        //this.targetAmmo.transform.position = (Vector2)this.transform.position + this.turretDirection * this.targetDistance;
        this.targetAmmo.transform.position = (Vector2)this.tankTurret.transform.position + this.turretDirection * this.targetDistance;

        return playerMovement;
    }


    public IEnumerator Shoot()
    {
        if (this.ammoPrefab != null)
        {
            AudioManager.sharedInstance.PlayShoot();
            Vector2 newDirection = this.ammoOrigin.transform.position - this.tankTurret.transform.position;
            //Vector3 fixAmmoPosition = new Vector3(transform.position.x, transform.position.y - 0.44f, transform.position.z);
            //GameObject newAmmoPrefab = Instantiate(ammoPrefab, this.targetAmmo.transform.position, tankTurret.transform.rotation);
            GameObject newAmmoPrefab = Instantiate(ammoPrefab, this.ammoOrigin.transform.position, tankTurret.transform.rotation);
            //newAmmoPrefab.GetComponent<AmmoController>().SetDirection(this.turretDirection);
            newAmmoPrefab.GetComponent<AmmoController>().SetDirection(newDirection);
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
        //Debug.Log($"Disparos actuales -> {this.currentShoots}");
    }

    public Vector3 GetPlayerPosition()
    {
        return this.transform.position;
    }
}
