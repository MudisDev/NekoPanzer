using Unity.Mathematics;
using UnityEngine;

enum AmmoControllerEnum
{
    player, turret
}

public class AmmoController : MonoBehaviour
{
    Rigidbody2D rgbd;
    [SerializeField] float shootSpeed = 1;
    //private float shootSpeed = 1;
    [SerializeField] AmmoControllerEnum fireBy;

    string boderLayer = "Border";

    private int damage;
    private Vector2 shootDirection;
    private PlayerController playerController;

    void Awake()
    {
        this.rgbd = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"TypeSbject -> {this.typeSubject}");
        //Debug.Log($"Rotacion Ammo -> {gameObject.transform.rotation}");
    }

    public void SetDirection(Vector2 direction)
    {
        this.shootDirection = direction;
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            this.rgbd.linearVelocity = this.shootDirection * this.shootSpeed;
        }
    }

    public void SetRotationZ(float axisZ)
    {

    }

    private void FixedUpdate()
    {
        // Si el juego no está en estado inGame, detener el movimiento
        if (GameManager.sharedInstance.currentGameState != GameState.inGame)
            this.rgbd.linearVelocity = Vector2.zero;
        else
            this.rgbd.linearVelocity = this.shootDirection * this.shootSpeed;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(this.boderLayer))
        {
            //Debug.Log($"chocado Bv");
            DestroyAmmo();
        }

        if (collision.gameObject.CompareTag("PlayerTag") && this.fireBy == AmmoControllerEnum.turret)
        {
            PlayerController.sharedInstance.SetLife(this.damage);
            DestroyAmmo();
        }

        if (collision.gameObject.CompareTag("DestructibleObjectTag") && this.fireBy == AmmoControllerEnum.player)
        {
            collision.gameObject.GetComponent<DestroyableObject>().SetObjectLife(this.damage);
            DestroyAmmo();
        }
        if (collision.gameObject.CompareTag("TurretTag") && this.fireBy == AmmoControllerEnum.player)
        {
            collision.gameObject.GetComponent<TurretController>().SetParalyzedTurret();
            DestroyAmmo();
        }
        if (collision.gameObject.CompareTag("EnemyTag") && this.fireBy == AmmoControllerEnum.player)
        {
            collision.gameObject.GetComponent<EnemyController>().SetEnemyLife(this.damage);
            DestroyAmmo();
        }
    }

    public void DestroyAmmo()
    {
        if (this.fireBy == AmmoControllerEnum.player)
        {
            this.playerController.SetCurrentShoots();
        }
        Destroy(gameObject);
    }

    public void SetEnum(string opcion)
    {
        if (opcion == AmmoControllerEnum.turret.ToString())
        {
            this.fireBy = AmmoControllerEnum.turret;
        }
        else if (opcion == AmmoControllerEnum.player.ToString())
        {
            this.fireBy = AmmoControllerEnum.player;
        }
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    public void SetShootSpeed(float speed)
    {
        this.shootSpeed = speed;
        this.shootSpeed = math.clamp(this.shootSpeed, 0, 10);
    }

    public void SetPlayerController(PlayerController playerController)
    {
        this.playerController = playerController;
    }
}
