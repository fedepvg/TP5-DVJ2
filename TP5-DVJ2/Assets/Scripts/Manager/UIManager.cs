using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    SpaceShip PlayerShip;
    int Fuel;
    int EnemiesDawn;
    int Altitude;
    int Health;
    int Score;
    [SerializeField]
    private Text FuelText;
    [SerializeField]
    private Text EnemiesDawnText;
    [SerializeField]
    private Text AltitudeText;
    [SerializeField]
    private Text HealthText;
    [SerializeField]
    private Text ScoreText;
    [SerializeField]
    private GameObject GameOverText;
    [SerializeField]
    private GameObject Crosshair;

    private void Awake()
    {
        PlayerShip = Player.GetComponent<SpaceShip>();
    }

    private void Update()
    {
        int ActualFuel = PlayerShip.GetFuel();
        int ActualAltitude = (int)Player.transform.position.y;
        int ActualHealth = PlayerShip.GetHealth();
        int ActualEnemiesDawn = GameManager.Instance.EnemiesDawn;
        int ActualScore = GameManager.Instance.Score;

        if(ActualFuel!=Fuel)
        {
            Fuel = ActualFuel;
            FuelText.text = "Fuel:\n" + Fuel;
        }
        if (ActualAltitude != Altitude)
        {
            Altitude = ActualAltitude;
            AltitudeText.text = "Altitude: " + Altitude;
        }
        if (ActualHealth != Health)
        {
            Health = ActualHealth;
            HealthText.text = "Health: " + Health;
        }
        if (ActualEnemiesDawn != EnemiesDawn)
        {
            EnemiesDawn = ActualEnemiesDawn;
            EnemiesDawnText.text = "Enemies Dawn: " + EnemiesDawn;
        }
        if (ActualScore != Score)
        {
            Score = ActualScore;
            ScoreText.text = "Score: " + Score;
        }

        if(!GameManager.Instance.InGame)
        {
            if(!GameOverText.activeSelf)
            {
                GameOverText.SetActive(true);
                Crosshair.SetActive(false);
            }
        }
    }
}
