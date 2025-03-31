using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{

    [SerializeField] Slider HealthBar;

    private Image FillHealthBar;
    const int MAXLIFE = 100;
    const int MINLIFE = 0;
    [SerializeField] TextMeshProUGUI nameScene;

    private void Awake()
    {
        this.FillHealthBar = this.HealthBar.fillRect.GetComponent<Image>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!this.HealthBar)
            Debug.LogWarning("Error, HealtBar no asignado");
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState != GameState.inGame)
            return;
        UpdateHealthBar();
        DisableFillHealtBar();
        this.nameScene.text = UpdateNameScene();
    }

    public void UpdateHealthBar()
    {
        this.HealthBar.value = PlayerController.sharedInstance.GetLife();
    }

    public void DisableFillHealtBar()
    {
        if (this.HealthBar.value == 0)
            this.FillHealthBar.enabled = false;
    }

     string UpdateNameScene()
    {
        string nameScene = "";
        //Debug.Log($"updatenaemscene -> {ChangeScene.sharedInstance.GetCurrentScene()}");
        switch (ChangeScene.sharedInstance.GetCurrentScene())
        {
            case "Level1":
                nameScene = "Nivel 1";
                return nameScene;
            case "Level2":
                nameScene = "Nivel 2";
                return nameScene;
            case "Level3":
                nameScene = "Nivel 3";
                return nameScene;
            case "Level4":
                nameScene = "Nivel 4";
                return nameScene;
            case "FinalLevel":
                nameScene = "Nivel Final";
                return nameScene;
        }
        return "null";
    }
}
