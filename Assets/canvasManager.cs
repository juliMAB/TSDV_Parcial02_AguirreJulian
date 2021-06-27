using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class canvasManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score          ;
    [SerializeField] TextMeshProUGUI timer          ;
    [SerializeField] TextMeshProUGUI horizontalSpeed;
    [SerializeField] TextMeshProUGUI verticalSpeed  ;
    [SerializeField] TextMeshProUGUI altitude       ;
    [SerializeField] LevelManager levelManager      ;
    [Header("ref2")]
    private ShipData shipData;
    [SerializeField] private int modificador=10;
    


    void Start()
    {
        shipData = levelManager.shipController.GetData();
    }
    void Update()
    {
        timer.text = levelManager.timeInGame.ToString();

        horizontalSpeed.text = "HorizontalSpeed: " + Mathf.RoundToInt(shipData.rb2d.velocity.x* modificador);

        verticalSpeed.text = "VerticalSpeed: " + Mathf.RoundToInt(shipData.rb2d.velocity.y * modificador);

        altitude.text = "Altitude: " + Mathf.RoundToInt(shipData.altitude * modificador);
    }
}
