using System.Collections;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    int disparos = 0;
    bool canShoot;
    [SerializeField] GameObject ammoPrefab;
    [SerializeField] Vector2 directionShoot;
    [SerializeField] int damage;
    private Rigidbody2D rgbd;
    private SpriteRenderer spr;
    private bool paralyzedTurret;

    void Awake()
    {
        this.rgbd = GetComponent<Rigidbody2D>();
        this.spr = GetComponent<SpriteRenderer>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.canShoot = true;
        this.paralyzedTurret = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState != GameState.inGame)
            return;
        if (this.canShoot && !this.paralyzedTurret)
        {
            this.canShoot = false;
            StartCoroutine(Shoot());
        }

    }

    public IEnumerator Shoot()
    {
        //while (true)
        //{
        yield return new WaitForSeconds(1);
        this.disparos++;
        //Debug.Log($"Disparo no. -> {disparos}");

        if (this.ammoPrefab != null)
        {
            GameObject newAmmoPrefab = Instantiate(ammoPrefab, transform.position, Quaternion.identity);
            //newAmmoPrefab.GetComponent<AmmoController>().SetDirection(DirectionToPlayer());
            newAmmoPrefab.GetComponent<AmmoController>().SetDirection(this.directionShoot);
            newAmmoPrefab.GetComponent<AmmoController>().SetEnum("turret");
            newAmmoPrefab.GetComponent<AmmoController>().SetDamage(this.damage);
        }

        else
            Debug.LogError("ammoPrefab no asinado");

        this.canShoot = true;
        //}
    }

    public void SetParalyzedTurret()
    {
        this.paralyzedTurret = true;
        StartCoroutine(FreezeTurret());


    }

    public IEnumerator FreezeTurret()
    {
        this.spr.color = Color.blue;

        yield return new WaitForSeconds(3);
        this.paralyzedTurret = false;
        this.spr.color = Color.white;
    }

    public Vector3 DirectionToPlayer()
    {
        Vector3 directionToPlayer = (PlayerController.sharedInstance.GetPlayerPosition() - this.transform.position).normalized;
        return directionToPlayer;
    }

}
