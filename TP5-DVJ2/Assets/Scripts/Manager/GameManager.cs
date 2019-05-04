using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    int enemiesDawn;
    int score;
    bool inGame;

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
    }

    public static GameManager Instance
    {
        get { return instance; }
    }

    public void GameOver()
    {
        inGame = false;
        //SceneManager.LoadScene("EndGameScene");
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
