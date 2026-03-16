using System.Collections;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    int disparos = 0;
    bool canShoot;
    [SerializeField] GameObject ammoPrefab;
    [SerializeField] Vector2 directionShoot;
    [SerializeField] int damage;
    [SerializeField] float cooldownTime;
    private Rigidbody2D rgbd;
    private SpriteRenderer spr;
    private bool paralyzedTurret;

    [SerializeField] GameObject ammoOrigin;
    [SerializeField] float turretSize;
    [SerializeField] float visionRange;

    private Vector2 turretDirection;
    [SerializeField] GameObject turretGun;

    [SerializeField] LayerMask capajugador;

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

        this.ammoOrigin.transform.position = new Vector2(this.rgbd.transform.position.x, this.rgbd.transform.position.y - this.turretSize);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState != GameState.inGame)
            return;


        if (PlayerDetected())
        {
            if (this.canShoot && !this.paralyzedTurret)
            {
                this.canShoot = false;
                StartCoroutine(Shoot());
            }




           
                this.turretDirection = DirectionToPlayer().normalized;
                float targetAngle = Mathf.Atan2(this.turretDirection.y, this.turretDirection.x) * Mathf.Rad2Deg;

                float smoothAngle = Mathf.LerpAngle(
                    this.turretGun.transform.eulerAngles.z,
                    targetAngle - 90f,
                    Time.deltaTime * 15f
                );

                this.turretGun.transform.rotation = Quaternion.Euler(0, 0, smoothAngle);
            



        }

        this.ammoOrigin.transform.position = (Vector2)this.transform.position + (Vector2)DirectionToPlayer() * this.turretSize;

    }

    public IEnumerator Shoot()
    {
        //while (true)
        //{
        yield return new WaitForSeconds(this.cooldownTime);
        this.disparos++;
        //Debug.Log($"Disparo no. -> {disparos}");
        Debug.Log($"posicion del player -> {DirectionToPlayer()}");
        if (this.ammoPrefab != null)
        {
            GameObject newAmmoPrefab = Instantiate(ammoPrefab, this.ammoOrigin.transform.position, Quaternion.identity);
            //newAmmoPrefab.GetComponent<AmmoController>().SetDirection(DirectionToPlayer());
            //newAmmoPrefab.GetComponent<AmmoController>().SetDirection(this.directionShoot);
            newAmmoPrefab.GetComponent<AmmoController>().SetDirection(DirectionToPlayer());
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
    public bool PlayerDetected()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, DirectionToPlayer(), this.visionRange, capajugador);



        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
            {

                //Debug.DrawRay(this.transform.position, DirectionToPlayer() * this.visionRange, Color.red);

                return false;
            }
            else
            {
                Debug.Log(" Jugador detectado");
                //Debug.DrawRay(this.transform.position, DirectionToPlayer() * this.visionRange, Color.green);
                Debug.Log($"capa colicionada -> {hit.collider.name}");

                return true;
            }
        }
        return false;
        // Debug.DrawRay(this.transform.position, DirectionToPlayer() * this.visionRange, Color.red);


    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (PlayerController.sharedInstance == null) return;
        /* if (isTouching)
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.red; */


        //Gizmos.DrawRay(this.transform.position, DirectionToPlayer() * 2);

        //Gizmos.DrawRay(this.ammoOrigin.transform.position, DirectionToPlayer() * this.visionRange);
    }

#endif
}
