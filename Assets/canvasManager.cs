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
    [SerializeField] LevelManager levelManager      ;
    [SerializeField] Slider sliderFuel              ;
    [Header("ref2")]
    private ShipData shipData;
    [SerializeField] private int modificador=1;
    private int minutos;
    private int segundos;



    void Start()
    {
        shipData = levelManager.shipManager.GetData();
        sliderFuel.maxValue = shipData.initialfuel;
    }
    void Update()
    {
        segundos = Mathf.RoundToInt(levelManager.timeGame % 60);
        minutos  = Mathf.RoundToInt(levelManager.timeGame / 60);

        timer.text = minutos +":"+segundos;

        horizontalSpeed.text = "HorizontalSpeed: " + Mathf.RoundToInt(shipData.rb2d.velocity.x* modificador);

        verticalSpeed.text = "VerticalSpeed: " + Mathf.RoundToInt(shipData.rb2d.velocity.y * modificador);

        altitude.text = "Altitude: " + Mathf.RoundToInt(shipData.altitude * 10);

        sliderFuel.value = shipData.fuel;
    }
}
