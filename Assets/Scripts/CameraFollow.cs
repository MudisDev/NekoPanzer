using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow sharedInstance;
    [SerializeField] GameObject follow;
    //[SerializeField] Transform minPosition, maxPosition;
    private Vector2 minCameraPosition, maxCameraPosition;

    Camera cam;
    float camHalfH, camHalfW;

    [SerializeField] float smoothTime;

    [SerializeField] Vector2 velocity;

    // [SerializeField] Vector2 minPosition, maxPosition;

    private void Awake()
    {
        if (sharedInstance == null && sharedInstance != this)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(gameObject); // Evita que haya múltiples instancias
            return;
        }

        this.cam = GetComponent<Camera>();
        this.camHalfH = this.cam.orthographicSize;
        this.camHalfW = this.camHalfH * cam.aspect;
    }

    // Use this for initialization
    void Start()
    {
        GameObject min = GameObject.Find("AxisMinCamera");
        GameObject max = GameObject.Find("AxisMaxCamera");
        if (!min || !max)
            Debug.LogWarning("Limites de mainCamera nulos");

        else
        {
            this.minCameraPosition = min.transform.position;
            this.maxCameraPosition = max.transform.position;

        }

        transform.position = new Vector3(
            Mathf.Clamp(follow.transform.position.x, minCameraPosition.x, maxCameraPosition.x),
            Mathf.Clamp(follow.transform.position.y, minCameraPosition.y, maxCameraPosition.y),
            transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!this.follow)
        {
            Debug.LogError("No se ha asignado un objeto para seguir en la cámara.");
            return;
        }

        float posX = Mathf.SmoothDamp(transform.position.x,
            follow.transform.position.x, ref velocity.x, smoothTime);
        float posY = Mathf.SmoothDamp(transform.position.y,
            follow.transform.position.y, ref velocity.y, smoothTime);

        Vector3 targetPosition = new Vector3(posX, posY, transform.position.z);
        targetPosition.x = Mathf.Clamp(targetPosition.x, minCameraPosition.x + this.camHalfW, maxCameraPosition.x - this.camHalfW);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minCameraPosition.y + this.camHalfH, maxCameraPosition.y - this.camHalfH);
        transform.position = targetPosition;


    }
}
