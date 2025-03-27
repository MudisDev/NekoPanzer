using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class DestroyableObject : MonoBehaviour
{
    [SerializeField] int objectLife;
    [SerializeField] GameObject SliderLife;
    [SerializeField] float offsetYSliderLife;
    GameObject sliderLife = null;

    const int MINLIFE = 0;
    const int MAXLIFE = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InstantiateLifeBar();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState != GameState.inGame)
            return;
        if (this.objectLife <= 0)
        {
            Destroy(this.sliderLife);
            Destroy(gameObject);
        }

        UpdateSliderLife();
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

    public void InstantiateLifeBar()
    {
        if (!this.SliderLife)
        {
            Debug.LogError("SliderLife no asignado");
            return;
        }
        this.sliderLife = Instantiate(this.SliderLife, this.transform.position, Quaternion.identity);
    }

    public bool FullLife()
    {
        if (this.objectLife == MAXLIFE)
            return true;
        return false;
    }

    public void UpdateSliderLife()
    {
        if (!this.sliderLife)
            return;
        Vector3 fixPosition = new Vector3(this.transform.position.x, this.transform.position.y + this.offsetYSliderLife, this.transform.position.z);

        this.sliderLife.GetComponent<EnemyUI>().EnableDisableSliderImage(FullLife());
        this.sliderLife.GetComponent<EnemyUI>().SetSliderEnemyLifePosition(fixPosition);
        this.sliderLife.GetComponent<EnemyUI>().SetSliderEnemyLife(this.objectLife);
    }
}
