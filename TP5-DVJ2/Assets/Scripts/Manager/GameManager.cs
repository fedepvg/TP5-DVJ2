using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    int enemiesDawn;
    int score;
    bool inGame;
    float GOTimer;
    const float TimeInGO=5;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        enemiesDawn = 0;
        inGame = true;
        GOTimer = 0;
    }

    private void Update()
    {
        if(!InGame)
        {
            GameOver();
        }
    }

    public static GameManager Instance
    {
        get { return instance; }
    }

    public void GameOver()
    {
        inGame = false;
        GOTimer += Time.deltaTime;
        if(GOTimer>TimeInGO)
        {
            inGame = true;
            SceneManager.LoadScene("EndGameScene");
            GOTimer = 0;
        }
    }

    public void AddEnemyDawn()
    {
        enemiesDawn++;
        score += 100;
    }

    public int EnemiesDawn
    {
        get { return enemiesDawn; }
    }

    public int Score
    {
        get { return score; }
    }

    public bool InGame
    {
        get { return inGame; }
    }
}
