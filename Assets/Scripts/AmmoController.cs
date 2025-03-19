using UnityEngine;

public class AmmoController : MonoBehaviour
{
    Rigidbody2D rgbd;
    [SerializeField] float shootSpeed = 5;

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

    }

    public void SetDirection(Vector2 direction)
    {
        this.rgbd.AddForce(direction * this.shootSpeed, ForceMode2D.Force);
    }
}
