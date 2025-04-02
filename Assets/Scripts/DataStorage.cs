using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStorage : MonoBehaviour
{

    public static DataStorage sharedInstance;


    bool enableMainMenu = true;
    /* float playerPointsLife = 100;
    int maxPlayerLife = 100;

    int playerDamage = 10;

    bool relic1 = true;
    bool relic2 = true;
    bool relic3 = true;
    bool finalRelic = true;
    bool example = true;

    bool directionPlayer = false;

    int playerLevel = 1;
    int playerTotalExp = 0;

    int playerRemainingExp = 0;
    int nextLevel = 10;

    int playerMoney; */

    //private Vector2 playerPosition = Vector2.zero;

   /*  private Vector2[] playerPositions = new Vector2[5]
{
    new Vector2(0, 0),
    new Vector2(0, 0),
    new Vector2(0, 0),
    new Vector2(0, 0),
    new Vector2(0, 0)
}; */


    private void Awake()
    {
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject);
            return;
        }

        sharedInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    /* public void SavePlayerPointsLife(float life)
    {
        this.playerPointsLife = life;
    } */

    public void ResetData()
    {
        //this.playerPointsLife = 100;

        SetEnableMainMenu(true);
        /* SavePlayerPointsLife(this.playerPointsLife);

        this.relic1 = true;
        this.relic2 = true;
        this.relic3 = true;
        this.finalRelic = true;
        this.playerLevel = 1;
        this.playerTotalExp = 0;
        this.playerRemainingExp = 0;
        this.nextLevel = 10;
        this.maxPlayerLife = 100;
        this.playerDamage = 10;

        this.playerMoney = 0;

        for (int i = 0; i < playerPositions.Length; i++)
            playerPositions[i] = Vector2.zero; */
    }

    public bool GetEnableMainMenu()
    {
        return this.enableMainMenu;
    }

    public void SetEnableMainMenu(bool mainMenu)
    {
        this.enableMainMenu = mainMenu;
    }


    /* public float LoadPlayerPointsLife()
    {
        return this.playerPointsLife;
    }

    public void SetKeyObjects(int relic, bool status)
    {
        switch (relic)
        {
            case 1:
                this.relic1 = status;
                break;
            case 2:
                this.relic2 = status;
                break;
            case 3:
                this.relic3 = status;
                break;
            case 4:
                this.example = status;
                break;
            case 0:
                this.finalRelic = status;
                break;
        }

    }

    public bool GetKeyObjects(int relic)
    {
        switch (relic)
        {
            case 1:
                return this.relic1;

            case 2:
                return this.relic2;

            case 3:
                return this.relic3;
            case 4:
                return this.example;

            case 0:
                return this.finalRelic;

        }
        return false;
    }

    public void SetPlayerPosition(Vector2 position, int scene)
    {
        this.playerPositions[scene] = position;

    }

    public Vector2 GetPlayerPosition(int scene)
    {
        return this.playerPositions[scene];
    }

    public bool GetDirectionPlayer()
    {
        return this.directionPlayer;
    }

    public void SetDirectionPlayer(bool direction)
    {
        this.directionPlayer = direction;
    }

    public int GetPlayerLevel()
    {
        return this.playerLevel;
    }

    public void SetPlayerLevel(int level)
    {
        this.playerLevel = level;
    }

    public int GetPlayerTotalExp()
    {
        return this.playerTotalExp;
    }

    public void SetPlayerTotalExp(int exp)
    {
        this.playerTotalExp = exp;
    }

    public int GetPlayerRemainingExp(){
        return this.playerRemainingExp;
    }

    public void SetPlayerRemainingExp(int exp){
        this.playerRemainingExp = exp;
    }

    public int GetNextLevel(){
        return this.nextLevel;
    }

    public void SetNextLevel(int points){
        this.nextLevel = points;
    }

    public void SetMaxPlayerLife(int maxLife)
    {
        this.maxPlayerLife = maxLife;
    }

    public int GetMaxPlayerLife()
    {
        return this.maxPlayerLife;
    }

    public int GetPlayerDamage()
    {
        return this.playerDamage;
    }

    public void SetPlayerDamage(int damage)
    {
        this.playerDamage = damage;
    }

    public void SetPlayerMoney(int money)
    {
        this.playerMoney = money;
    }

    public int GetPlayerMoney()
    {
        return this.playerMoney;
    } */


}
