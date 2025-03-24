using UnityEngine;

enum AmmoControllerEnum
{
    player, turret
}

public class AmmoController : MonoBehaviour
{
    Rigidbody2D rgbd;
    [SerializeField] float shootSpeed = 5;
    [SerializeField] AmmoControllerEnum typeSubject;

    string boderLayer = "Border";

    private int damage;

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
        this.rgbd.AddForce(direction * this.shootSpeed, ForceMode2D.Force);
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
    }

    public void SetEnum(string opcion)
    {
        if (opcion == AmmoControllerEnum.turret.ToString())
        {
            this.typeSubject = AmmoControllerEnum.turret;
        }
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
