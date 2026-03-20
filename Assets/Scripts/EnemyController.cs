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
    [SerializeField] float patrolChangeTime = 2f;

    private bool isEnemyAlive;
    private Rigidbody2D enemyRigidBody;

    private Vector2 initialPosition;
    private Vector2 patrolTarget;
    private float patrolTimer;

    private Transform playerTarget;

    [SerializeField] SpriteRenderer spriteExplosion;

    void Awake()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        isEnemyAlive = true;

        if (enemyRigidBody == null)
            Debug.LogError("Error, no hay un rigidbody asignado en el enemigo Bv");

        if (this.spriteExplosion == null)
            Debug.LogError("Error, sprite de explisio no asignado Bv.");

        this.spriteExplosion.enabled = false;

        initialPosition = transform.position;

        SetNewPatrolPoint();
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
            return;
        }

        // Perseguir jugador
        enemyRigidBody.linearVelocity = direction * enemyspeed;
    }

    void Patrol()
    {
        patrolTimer += Time.deltaTime;

        // Cambiar punto de patrullaje cada cierto tiempo
        if (patrolTimer >= patrolChangeTime)
        {
            SetNewPatrolPoint();
            patrolTimer = 0f;
        }

        Vector2 direction = (patrolTarget - (Vector2)transform.position).normalized;
        enemyRigidBody.linearVelocity = direction * enemyspeed;

        // Si llega cerca del punto, generar otro
        if (Vector2.Distance(transform.position, patrolTarget) < 0.5f)
        {
            SetNewPatrolPoint();
        }
    }

    void SetNewPatrolPoint()
    {
        patrolTarget = initialPosition + Random.insideUnitCircle * patrolRadius;
    }

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
        this.spriteExplosion.enabled = true;
        yield return new WaitForSeconds(3);
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