using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

//Hace referencia a los estados posibels del juego
public enum GameState
{
    menu, inGame, gameOver, pause, win,
};

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;  //Variable que hace referencia al mismo game manager
    public GameState currentGameState = GameState.menu; //Variable para saber estado del juego, al inicio esta en menu principal

    [SerializeField] bool gameIsPaused = false;
    [SerializeField] Canvas canvasMenu, canvasGameOver, canvasInGame, canvasPause, canvasWin /* canvasTouchUI */;
    private void Awake()
    {
        sharedInstance = this;
    }

    public void Start()
    {
        if (DataStorage.sharedInstance.GetEnableMainMenu())
            BackToMenu();
        else
            StartGame();

    }

    private void Update()
    {
        if (InputManager.sharedInstance.GetPauseButton() && this.currentGameState == GameState.inGame)
        //if (Input.GetKeyDown(KeyCode.P))
            TogglePause();
    }
    public void StartGame()
    {
        this.SetGameState(GameState.inGame);

    }


    //Gestiona si el juego esta pausado o no
    public void TogglePause()
    {
        gameIsPaused = !gameIsPaused;

        if (gameIsPaused)
            Pause();
        else
            Resume();
    }

    public void Pause()
    {
        gameIsPaused = true;
        Time.timeScale = 0f;
        this.SetGameState(GameState.pause);
    }

    public void Resume()
    {
        gameIsPaused = false;
        Time.timeScale = 1f;
        this.SetGameState(GameState.inGame);
    }

    //<<< Termina lo que gestiona la pausa>>>

    public void GameOver()
    {
        DataStorage.sharedInstance.ResetData();
        this.SetGameState(GameState.gameOver);
    }

    public void Win()
    {
        DataStorage.sharedInstance.ResetData();
        this.SetGameState(GameState.win);
    }

    public void BackToMenu()
    {
        if (this.gameIsPaused)
        {
            gameIsPaused = false;
            Time.timeScale = 1f;
        }

        if (this.currentGameState == GameState.gameOver ||
           this.currentGameState == GameState.win ||
           this.currentGameState == GameState.pause)
        {
            DataStorage.sharedInstance.ResetData();
            ChangeScene.sharedInstance.RefreshScene();
        }

        this.SetGameState(GameState.menu);
    }

    //Desactiva todos los canvas a la vez
    void SetCanvasEnable()
    {
        canvasMenu.enabled = false;
        canvasGameOver.enabled = false;
        canvasInGame.enabled = false;
        canvasPause.enabled = false;
        canvasWin.enabled = false;
        //this.canvasTouchUI.enabled = false;
    }

    //Funcion que cambia el estado del juego
    //Activa el canvas correspondiente
    void SetGameState(GameState newGameState)
    {
       
        // sera necesario que solo sea en el lvl1
        if (newGameState == GameState.menu)
        {
            //Preparar codigo para volver al menu
            SetCanvasEnable();
            canvasMenu.enabled = true;


        }
        else if (newGameState == GameState.gameOver)
        {
            //Preparar codigo para pantalla de gameover
            SetCanvasEnable();
            canvasGameOver.enabled = true;

        }
        else if (newGameState == GameState.inGame)
        {

            DataStorage.sharedInstance.SetEnableMainMenu(false); //es para que no aparezca  el main menu al cambiar de escena.
            //Preparar codigo para estar en juego
            SetCanvasEnable();
            canvasInGame.enabled = true;
            //this.canvasTouchUI.enabled = true;

        }
        else if (newGameState == GameState.pause)
        {
            //Preparar codigo para estar en juego
            SetCanvasEnable();
            canvasPause.enabled = true;

        }
        else if (newGameState == GameState.win)
        {
            //Preparar codigo para estar en juego
            SetCanvasEnable();
            canvasWin.enabled = true;
        }
        this.currentGameState = newGameState;

        //AudioManager.sharedInstance.SetTrackMusic(this.currentGameState);

         MenuController.sharedInstance.DetectCurrentCanva();
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
             Application.Quit();
#endif
    }
}
