using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonoBehaviourSingletonScript;

public class LevelManager : MonoBehaviourSingleton<LevelManager>
{
    [SerializeField] int score;
    [SerializeField] public int timeInGame;

    public GameObject player;
    public ShipController shipController;
    public ShipManager shipManager;

    void Start()
    {
        shipController = player.GetComponent<ShipController>();
        shipManager = player.GetComponent<ShipManager>();
    }

    public void AddScore(int _score)
    {
        score += _score;
    }

    void LoseMatch()
    {
        //mostrar la derrota.
        //sacarte y llevarte al scoreboard.
    }

    void WinMatch()
    {
        //mostrar la victoria.
        //sumar los puntos.
        //cargar siguiente nivel.
    }
}
