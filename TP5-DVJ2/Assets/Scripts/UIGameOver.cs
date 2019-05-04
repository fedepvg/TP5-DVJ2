using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    int FinalScore;
    int FinalEnemiesDawn;
    [SerializeField]
    Text ScoreText;
    [SerializeField]
    Text EnemiesDawnText;
    GameManager GM;

    void Awake()
    {
        if (!GameManager.Instance)
        {
            GM = new GameManager();
        }
        else
        {
            GM = GameManager.Instance;
        }
        FinalScore = GM.Score;
        FinalEnemiesDawn = GM.EnemiesDawn;
    }

    private void OnEnable()
    {
        if(ScoreText)
            ScoreText.text = "Score: " + FinalScore.ToString();
        if(EnemiesDawnText)
            EnemiesDawnText.text = "Enemies Dawn: " + FinalEnemiesDawn;
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
}
