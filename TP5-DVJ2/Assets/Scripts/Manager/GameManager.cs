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
    public bool SceneLoading;
    [SerializeField]
    GameObject EnemyPrefab;
    const int Level1Enemies = 2;
    const int Level2Enemies = 3;
    const int Level3Enemies = 4;
    int LevelEnemiesDawn;
    [SerializeField]
    private Transform[] SpawnPoint;
    UIManager UI;

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
        SceneLoading = true;
    }

    private void Update()
    {
        if(!InGame)
        {
            GameOver();
        }
        else
        {
            if(SceneManager.GetActiveScene().name!="MenuScene" && SceneManager.GetActiveScene().name != "EndGameScene")
            {
                Cursor.lockState=CursorLockMode.Confined;
                Cursor.visible = false;
            }
            if (SceneLoading)
            {
                if (SceneManager.GetActiveScene().name == "Level1")
                {
                    UI.SetSceneLoading(true);
                    for (int i = 0; i < Level1Enemies; i++)
                    {
                        Instantiate(EnemyPrefab,SpawnPoint[i].position,Quaternion.identity);
                        LevelEnemiesDawn = 0;
                    }
                    SceneLoading = false;
                }
                else if (SceneManager.GetActiveScene().name == "Level2")
                {
                    UI.SetSceneLoading(true);
                    for (int i = 0; i < Level2Enemies; i++)
                    {
                        Instantiate(EnemyPrefab, SpawnPoint[i].position, Quaternion.identity);
                        LevelEnemiesDawn = 0;
                    }
                    SceneLoading = false;
                }
                else if (SceneManager.GetActiveScene().name == "Level3")
                {
                    UI.SetSceneLoading(true);
                    for (int i = 0; i < Level3Enemies; i++)
                    {
                        Instantiate(EnemyPrefab, SpawnPoint[i].position, Quaternion.identity);
                        LevelEnemiesDawn = 0;
                    }
                    SceneLoading = false;
                }
            }
            else
            {
                if (SceneManager.GetActiveScene().name == "Level1")
                {
                    if (LevelEnemiesDawn == Level1Enemies)
                    {
                        SceneManager.LoadScene("Level2");
                        SceneLoading = true;
                    }
                }
                else if (SceneManager.GetActiveScene().name == "Level2")
                {
                    if (LevelEnemiesDawn == Level2Enemies)
                    {
                        SceneManager.LoadScene("Level3");
                        SceneLoading = true;
                    }
                }
                else if (SceneManager.GetActiveScene().name == "Level3")
                {
                    if (LevelEnemiesDawn == Level3Enemies)
                    {
                        SceneManager.LoadScene("EndGameScene");
                        SceneLoading = true;
                    }
                }
            }
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
        LevelEnemiesDawn++;
        score += 100;
    }

    public int EnemiesDawn
    {
        get { return enemiesDawn; }
        set { enemiesDawn = value; }
    }

    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    public bool InGame
    {
        get { return inGame; }
    }

    public void SetUIManager(UIManager ui)
    {
        UI = ui;
    }
}
