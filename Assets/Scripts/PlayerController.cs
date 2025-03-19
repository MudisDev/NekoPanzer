using Mono.Cecil.Cil;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rgbd;
    [SerializeField] float speed;

    private Vector2 playerDirection;


    private void Awake()
    {
        this.rgbd = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.playerDirection = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

        this.playerDirection = DirectionPlayer();
        // Normaliza la dirección para evitar que diagonal sea más rápida
        this.playerDirection = playerDirection.normalized * speed;

        // Asigna directamente la velocidad en lugar de usar AddForce
        this.rgbd.linearVelocity = this.playerDirection;

    }

    Vector2 DirectionPlayer()
    {
        Vector2 movement = Vector2.zero;

        if (Input.GetKey(KeyCode.A))
            movement = new Vector2(-1, 0);
        else if (Input.GetKey(KeyCode.D))
            movement = new Vector2(1, 0);
        else if (Input.GetKey(KeyCode.W))
            movement = new Vector2(0, 1);
        else if (Input.GetKey(KeyCode.S))
            movement = new Vector2(0, -1);
        else
            movement = Vector2.zero;

        return movement;
    }

}
