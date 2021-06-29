using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class canvasManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score          ;
    [SerializeField] TextMeshProUGUI timer          ;
    [SerializeField] TextMeshProUGUI horizontalSpeed;
    [SerializeField] TextMeshProUGUI verticalSpeed  ;
    [SerializeField] TextMeshProUGUI altitude       ;
    [SerializeField] TextMeshProUGUI advertence          ;
    [SerializeField] TextMeshProUGUI advertenceLowFuel   ;
    [SerializeField] TextMeshProUGUI end            ;
    [SerializeField] LevelManager levelManager      ;
    [SerializeField] Slider sliderFuel              ;
    [Header("ref2")]
    private ShipManager ship;
    [SerializeField] private ShipData shipData;
    [SerializeField] private int modificador=1;
    private int minutos;
    private int segundos;



    void Start()
    {
        ship = levelManager.GetShip();
        shipData = ship.GetData();
        sliderFuel.maxValue = shipData.initialfuel;
        shipData.OnLowFuel += UpdateAdvartance;
        ship.OnLanding += ShowWin;
        ship.OnDestroy += ShowLose;
        levelManager.OnScore = UpdateScore;
        levelManager.OnReset = ResetUI;
    }
    void Update()
    {
        segundos = Mathf.RoundToInt(levelManager.timeGame % 60);
        minutos  = Mathf.RoundToInt(levelManager.timeGame / 60);

        timer.text = minutos +":"+segundos;

        horizontalSpeed.text = "HorizontalSpeed: " + Mathf.RoundToInt(shipData.rb2d.velocity.x* modificador);

        verticalSpeed.text = "VerticalSpeed: " + Mathf.RoundToInt(shipData.rb2d.velocity.y * modificador);

        altitude.text = "Altitude: " + Mathf.RoundToInt(shipData.altitude);

        sliderFuel.value = shipData.fuel;

        advertence.enabled =(ship.TooFast);
    }

    void UpdateAdvartance(int value)
    {
        if (value<=20)
        {
            advertenceLowFuel.enabled = true;
            advertenceLowFuel.text = "LOW FUEL";
            if (value<=3)
            {
                advertenceLowFuel.text = "EMPTY";
            }
        }
    }
    void UpdateScore(int value)
    {
        score.text = "score: " + value;
    }

    void ShowWin()
    {
        end.enabled = true;
        end.text = "You Win, score save";
    }
    void ShowLose()
    {
        end.enabled = true;
        end.text = "You Fail no score to you";
    }

    void ResetUI()
    {
        end.enabled = false;
    }
}
