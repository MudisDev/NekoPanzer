using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayController : MonoBehaviour
{

    public static GamePlayController sharedInstance;
    private int totalEnemies = -1;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject);
            return;
        }

        sharedInstance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.totalEnemies = GameObject.FindGameObjectsWithTag("EnemyTag").Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState != GameState.inGame) return;
        //Debug.Log($"Nuero de enemigos en escena -> {this.totalEnemies}");

        if (this.totalEnemies <= 0)
        {
            if (SceneManager.GetActiveScene().name != "Level2")
                StartCoroutine(ChangeScene());
            else
                StartCoroutine(Win());

        }

    }

    public void SetTotalEnemies()
    {
        this.totalEnemies--;
    }

    public IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Level2");
    }

    public IEnumerator Win()
    {
        yield return new WaitForSeconds(3);
        GameManager.sharedInstance.Win();
    }
}
