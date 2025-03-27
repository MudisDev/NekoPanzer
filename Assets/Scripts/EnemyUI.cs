using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] Slider sliderEnemyLife;
    [SerializeField] Image backgroundImageSlider;
    [SerializeField] Image fillImageSlider;


    void Awake()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetSliderEnemyLife(int pointsLife)
    {
        this.sliderEnemyLife.value = pointsLife;
        //this.sliderEnemyLife.value = math.clamp(this.sliderEnemyLife.value, 0, 100);
    }
    public void SetSliderEnemyLifePosition(Vector3 position)
    {
        this.transform.position = position;
    }

    public void EnableDisableSliderImage(bool fullLife)
    {
        if (fullLife)
        {
            this.backgroundImageSlider.enabled = false;
            this.fillImageSlider.enabled = false;
        }
        else
        {
            this.backgroundImageSlider.enabled = true;
            this.fillImageSlider.enabled = true;
        }
    }
}
