using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonoBehaviourSingletonScript;

public class LevelManager : MonoBehaviourSingleton<LevelManager>
{
    [SerializeField] int score;
    [SerializeField] float timeInGame;
    [SerializeField] ProceduralGeneration generator;
    public float timeGame { get { return timeInGame; } }

    [SerializeField] GameObject player;
    public ShipManager shipManager;
    TRS initialShip;
    

    struct TRS
    {
        public Vector3 t;
        public Quaternion r;
        public Vector3 s;
    }
    void Start()
    {
        shipManager = player.GetComponent<ShipManager>();
        initialShip.t = player.transform.position;
        initialShip.r = player.transform.rotation;
        initialShip.s = player.transform.localScale;
        shipManager.OnLanding += WinMatch;
        shipManager.OnDestroy += LoseMatch;
        generator.GenerateTerrain();
    }

    void Update()
    {
        timeInGame += Time.deltaTime;  
    }
    public void AddScore(int _score)
    {
        score += _score;
    }

    void LoseMatch()
    {
        print("Perdite");
        //mostrar la derrota.
        //sacarte y llevarte al scoreboard.
    }

    void WinMatch()
    {
        print("Ganaste");
        //mostrar la victoria.
        //sumar los puntos.
        //cargar siguiente nivel.
    }

    private void ResetLevel()
    {
        player.transform.position = initialShip.t;
        player.transform.rotation = initialShip.r;
        player.transform.localScale = initialShip.s;
        generator.GenerateTerrain();
    }
}
