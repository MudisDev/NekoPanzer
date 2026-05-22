using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int enemyLife = 3;
    [SerializeField] float enemyspeed = 2f;

    [Header("Detection")]
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float detectionRadius = 5f;

    [Header("Patrol")]
    [SerializeField] float patrolRadius = 3f;
    // [SerializeField] float patrolChangeTime = 2f;
    [SerializeField] Transform[] transforms;

    private bool isEnemyAlive;
    private Rigidbody2D enemyRigidBody;

    private Vector2 initialPosition;
    private Vector2 patrolTarget;
    private float patrolTimer;
    private Transform playerTarget;
    private SpriteRenderer tankBody;

    private bool canEnemyShoot;

    [SerializeField] SpriteRenderer spriteExplosion;
    [SerializeField] GameObject ammoPrefab;
    [SerializeField] GameObject target;
    [SerializeField] int enemyDamage;
    private BoxCollider2D boxCollider;

    private Transform initialPoint;
    private int currentPoint = -1;

    private float bodyAngleEnemy;

    void Awake()
    {
        this.enemyRigidBody = GetComponent<Rigidbody2D>();
        this.tankBody = GetComponent<SpriteRenderer>();
        this.boxCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        isEnemyAlive = true;

        if (enemyRigidBody == null)
            Debug.LogError("Error, no hay un rigidbody asignado en el enemigo Bv");

        if (this.spriteExplosion == null)
            Debug.LogError("Error, sprite de explisio no asignado Bv.");

        if (this.spriteExplosion == null)
            Debug.LogError("Error, sprite de tanque enemigo no asignado Bv.");

        this.spriteExplosion.enabled = false;

        this.initialPoint = this.transforms[0];


        initialPosition = transform.position;

        //SetNewPatrolPoint();

        this.canEnemyShoot = true;
    }

    void Update()
    {
        if (GameManager.sharedInstance.currentGameState != GameState.inGame) return;
        if (!isEnemyAlive) return;

        // --- MORIR ---
        if (enemyLife <= 0)
        {
            isEnemyAlive = false;
            StartCoroutine(ActiveExplosion());

            return;
        }

        // --- DETECCIÓN ---
        Collider2D player = Physics2D.OverlapCircle(
            transform.position,
            detectionRadius,
            playerLayer
        );

        if (player != null)
        {
            playerTarget = player.transform;
        }
        else
        {
            playerTarget = null;
        }

        // --- COMPORTAMIENTO ---
        if (playerTarget != null)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void FixedUpdate()
    {
        // Movimiento se aplica aquí si quieres más estabilidad física
    }

    // =========================
    // 🧠 COMPORTAMIENTOS
    // =========================

    void ChasePlayer()
    {
        // Dirección hacia el jugador
        Vector2 direction = (playerTarget.position - transform.position).normalized;

        // Distancia desde la zona inicial
        float distanceFromStart = Vector2.Distance(transform.position, initialPosition);

        // Si se aleja demasiado, regresa
        if (distanceFromStart > patrolRadius * 1.5f)
        {
            Vector2 back = (initialPosition - (Vector2)transform.position).normalized;
            enemyRigidBody.linearVelocity = back * enemyspeed;
            Girar(direction);

            return;
        }

        // Perseguir jugador
        enemyRigidBody.linearVelocity = direction * enemyspeed;
        Girar(direction);


        if (this.isEnemyAlive && this.canEnemyShoot)
        {
            this.canEnemyShoot = false;

            StartCoroutine(EnemyShoot());
        }



    }

    public IEnumerator EnemyShoot()
    {
        //while (true)
        //{
        yield return new WaitForSeconds(0.5f);
        //this.disparos++;
        //Debug.Log($"Disparo no. -> {disparos}");
        Debug.Log($"posicion del player -> {DirectionToPlayer()}");
        if (this.ammoPrefab != null)
        {
            //GameObject newAmmoPrefab = Instantiate(ammoPrefab, this.ammoOrigin.transform.position, Quaternion.identity);
            //GameObject newAmmoPrefab = Instantiate(ammoPrefab, this.transform.position, this.tankBody.transform.rotation);
            GameObject newAmmoPrefab = Instantiate(ammoPrefab, this.target.transform.position, this.tankBody.transform.rotation);
            //newAmmoPrefab.GetComponent<AmmoController>().SetDirection(DirectionToPlayer());
            //newAmmoPrefab.GetComponent<AmmoController>().SetDirection(this.directionShoot);
            newAmmoPrefab.GetComponent<AmmoController>().SetDirection(DirectionToPlayer());
            newAmmoPrefab.GetComponent<AmmoController>().SetEnum("turret");
            newAmmoPrefab.GetComponent<AmmoController>().SetDamage(this.enemyDamage);
        }

        else
            Debug.LogError("ammoPrefab no asinado");

        this.canEnemyShoot = true;
        //}
    }

    void Patrol()
    {
        if (transforms.Length == 0) return;

        if (currentPoint == -1)
            currentPoint = 0;

        Transform target = transforms[currentPoint];

        Vector2 direction = (target.position - transform.position).normalized;

        enemyRigidBody.linearVelocity = direction * enemyspeed;
        Girar(direction);

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance < 0.2f)
        {
            currentPoint++;

            if (currentPoint >= transforms.Length)
                currentPoint = 0;
        }
    }

    private void Girar(Vector2 enemyDirection)
    {
        //float deadZone = 0.2f;
        //if (playerMovement.magnitude > deadZone)
        //{
        //this.playerDirection = playerMovement.normalized;
        float targetAngle = Mathf.Atan2(enemyDirection.y, enemyDirection.x) * Mathf.Rad2Deg - 90f;

        // Aquí sí usamos bodyAngle como acumulador del ángulo actual
        this.bodyAngleEnemy = Mathf.LerpAngle(this.bodyAngleEnemy, targetAngle, Time.deltaTime * 15f); // 5f se siente fluido sin ser lento
        this.tankBody.transform.rotation = Quaternion.Euler(0, 0, bodyAngleEnemy);
        //}

        // Actualiza posición del targetAmmo
        //this.targetAmmo.transform.position = (Vector2)this.transform.position + this.turretDirection * this.targetDistance;

        // return playerMovement;
    }

    /* void SetNewPatrolPoint()
    {
        patrolTarget = initialPosition + Random.insideUnitCircle * patrolRadius;
    } */

    // =========================
    // 💥 VIDA
    // =========================

    public void SetEnemyLife(int damagePoints)
    {
        enemyLife -= damagePoints;
    }

    public IEnumerator DestroyEnemy()
    {
        enemyRigidBody.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    public IEnumerator ActiveExplosion()
    {
        enemyRigidBody.linearVelocity = Vector2.zero;
        enemyRigidBody.freezeRotation = true;
        this.boxCollider.excludeLayers = playerLayer;

        this.spriteExplosion.enabled = true;

        yield return new WaitForSeconds(3);
        GamePlayController.sharedInstance.SetTotalEnemies();
        Destroy(gameObject);
    }

    // =========================
    // 🎨 DEBUG
    // =========================

    void OnDrawGizmos()
    {
        // Zona de patrullaje
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(
            Application.isPlaying ? (Vector3)initialPosition : transform.position,
            patrolRadius
        );

        // Zona de detección
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    // =========================
    // 🎯 UTILIDAD
    // =========================

    public Vector3 DirectionToPlayer()
    {
        return (PlayerController.sharedInstance.GetPlayerPosition() - transform.position).normalized;
    }
}