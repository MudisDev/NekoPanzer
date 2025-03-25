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
    [SerializeField] AmmoControllerEnum typeSubject;

    string boderLayer = "Border";

    private int damage;
    private Vector2 shootDirection;

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
    }

    public void SetDirection(Vector2 direction)
    {
        this.shootDirection = direction;
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            this.rgbd.linearVelocity = this.shootDirection * this.shootSpeed;
        }
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
            Destroy(gameObject);
        }


        if (collision.gameObject.CompareTag("PlayerTag") && this.typeSubject == AmmoControllerEnum.turret)
        {
            PlayerController.sharedInstance.SetLife(this.damage);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("DestructibleObjectTag") && this.typeSubject == AmmoControllerEnum.player)
        {
            collision.gameObject.GetComponent<DestroyableObject>().SetObjectLife(this.damage);
            Destroy(gameObject);
        }
    }

    public void SetEnum(string opcion)
    {
        if (opcion == AmmoControllerEnum.turret.ToString())
        {
            this.typeSubject = AmmoControllerEnum.turret;
        }
        else if (opcion == AmmoControllerEnum.player.ToString())
        {
            this.typeSubject = AmmoControllerEnum.player;
        }
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
