using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    public static MenuController sharedInstance;

    [SerializeField] GameObject firstButtonCanvasMenu;
    [SerializeField] GameObject firstButtonCanvasPause;
    [SerializeField] GameObject firstButtonCanvasGameOver;
    //[SerializeField] GameObject firstButtonCanvasWin;
    //[SerializeField] EventSystem eventmenu;
    private EventSystem eventmenu;


    private void Awake()
    {
        if (sharedInstance == null)
            sharedInstance = this;
        else
            Destroy(gameObject);

        this.eventmenu = GetComponent<EventSystem>();

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (this.eventmenu == null)
            Debug.LogError("Error, no hay un event system asignado");

        else
            DetectCurrentCanva();
    }

    // Update is called once per frame
    void Update()
    {
        //DetectCurrentCanva();
    }

    public void DetectCurrentCanva()
    {
        GameState currentGameState = GameManager.sharedInstance.currentGameState;

        //if (GameManager.sharedInstance.currentGameState != GameState.inGame)



        switch (currentGameState)
        {
            case GameState.menu:
                this.eventmenu.SetSelectedGameObject(this.firstButtonCanvasMenu);
                break;
            case GameState.pause:
                this.eventmenu.SetSelectedGameObject(this.firstButtonCanvasPause);
                break;
            case GameState.gameOver:
                this.eventmenu.SetSelectedGameObject(this.firstButtonCanvasGameOver);
                break;
            default:
                this.eventmenu.SetSelectedGameObject(null);
                break;
        }

    }
}
