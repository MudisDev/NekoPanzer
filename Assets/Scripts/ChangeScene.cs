using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes
{
    level1, level2, level3, level4, level5, finalLevel
}
public class ChangeScene : MonoBehaviour
{
    public static ChangeScene sharedInstance;

    private SpriteRenderer spr;

    [SerializeField] Scenes tarjetScene;
    //[SerializeField] Vector2 fixExitPosition;

    private bool knockingDoor;
    //private float exitPosition;

    private void Awake()
    {
         sharedInstance = this;
         this.spr = GetComponent<SpriteRenderer>();
        //this.exitPosition = this.gameObject.transform.position.x + this.fixExitPosition.x; 
    }

    private void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
            //if (this.knockingDoor && InputManager.sharedInstance.GetActionButton() && PlayerController.sharedInstance.GetIsTouchingTheGround())
            //if (this.knockingDoor && Input.GetKeyDown(KeyCode.E))
            if (this.knockingDoor && InputManager.sharedInstance.GetActionButton())
                ChangeSceneNow();


        /* Scene currentScene = SceneManager.GetActiveScene();
        Debug.Log($"curente scene -> {currentScene.name}"); */
        //Debug.Log($"Get currente stcen -> {GetCurrentScene()}");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerTag"))
        {
            this.knockingDoor = true;
            this.spr.color = Color.green;
            //TouchUIController.sharedInstance.SetActionButton(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerTag"))
        {
            this.knockingDoor = false;
            this.spr.color = Color.white;
            //TouchUIController.sharedInstance.SetActionButton(false);
        }
    }

    //se va a ejecutar desde el manager, solo si es game over, volvera a cargar lvl1
    public void RefreshScene()
    {
        SceneManager.LoadScene("Level1");
    }

    public string GetCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.name;
    }

    public int GetNumberCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        switch (currentScene.name)
        {
            case "Level1":
                return 0;
            case "Level2":
                return 1;
            case "Level3":
                return 2;
            case "Level4":
                return 3;
            case "FinalLevel":
                return 4;
        }
        return -1;
    }

    void ChangeSceneNow()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        //SavePlayerInfo();

        if (this.tarjetScene == Scenes.level1)
            SceneManager.LoadScene("Level1");
        else if (this.tarjetScene == Scenes.level2)
            SceneManager.LoadScene("Level2");
        else if (this.tarjetScene == Scenes.level3)
            SceneManager.LoadScene("Level3");
        else if (this.tarjetScene == Scenes.level4)
            SceneManager.LoadScene("Level4");
        else if (this.tarjetScene == Scenes.level5)
            SceneManager.LoadScene("Level5");
        else if (this.tarjetScene == Scenes.finalLevel)
            SceneManager.LoadScene("FinalLevel");
    }

    /* private void SavePlayerInfo()
    {
        DataStorage.sharedInstance.SavePlayerPointsLife(LevelSystem.sharedInstance.GetLife());
        DataStorage.sharedInstance.SetMaxPlayerLife(LevelSystem.sharedInstance.GetMaxPlayerLife());

        bool directionPlayer = PlayerAnimationController.sharedInstance.GetMirrorAnimation();
        DataStorage.sharedInstance.SetDirectionPlayer(directionPlayer);
        Vector2 playerPosition = new Vector2(this.exitPosition, PlayerController.sharedInstance.GetCurrentPlayerPosition());

        DataStorage.sharedInstance.SetPlayerPosition(playerPosition, GetNumberCurrentScene());
        DataStorage.sharedInstance.SetPlayerTotalExp(LevelSystem.sharedInstance.GetPlayerTotalExp());
        DataStorage.sharedInstance.SetPlayerLevel(LevelSystem.sharedInstance.GetPlayerLevel());
        DataStorage.sharedInstance.SetPlayerRemainingExp(LevelSystem.sharedInstance.GetPlayerRemainingExp());
        DataStorage.sharedInstance.SetNextLevel(LevelSystem.sharedInstance.GetNextLevel());
        DataStorage.sharedInstance.SetPlayerDamage(LevelSystem.sharedInstance.GetCurrentPlayerDamage());

        DataStorage.sharedInstance.SetPlayerMoney(LevelSystem.sharedInstance.GetPlayerMoney());
    } */
}