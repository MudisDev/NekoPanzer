using Unity.Mathematics;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    [SerializeField] int objectLife;

    const int MINLIFE = 0;
    const int MAXLIFE = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState != GameState.inGame)
            return;
        if (this.objectLife <= 0)
            Destroy(gameObject);
    }

    public void SetObjectLife(int pointsLife)
    {
        this.objectLife -= pointsLife;
        this.objectLife = math.clamp(objectLife, MINLIFE, MAXLIFE);
        Debug.Log($"Objectlife -> {this.objectLife}");
    }

    public int GetLife()
    {
        return this.objectLife;
    }
}
