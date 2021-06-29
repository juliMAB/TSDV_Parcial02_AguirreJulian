using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonoBehaviourSingletonScript;
using System;

public class LevelManager : MonoBehaviourSingleton<LevelManager>
{
    [SerializeField] ProceduralGeneration generator;
    [SerializeField] GameObject player;

    ShipManager shipManager;
    [SerializeField] float timeToNextLevel;
    public Action<int> OnScore;
    public Action OnReset;
    int score;
    float timeInGame;
    [SerializeField] List<LandPoint> landzones = new List<LandPoint>();
    public float timeGame { get { return timeInGame; } }
    bool timeRuning;

    TRS initialShip;

    struct TRS
    {
        public Vector3 t;
        public Quaternion r;
        public Vector3 s;
    }

    public ShipManager GetShip()
    {
        return shipManager;
    }
    void Start()
    {
        timeRuning = true;
        shipManager = player.GetComponent<ShipManager>();
        initialShip.t = player.transform.position;
        initialShip.r = player.transform.rotation;
        initialShip.s = player.transform.localScale;
        shipManager.OnLanding += WinMatch;
        shipManager.OnDestroy += LoseMatch;
        generator.GenerateTerrain();
        landzones = generator.GetLandZones();
    }

    void Update()
    {
        if (timeRuning)
            timeInGame += Time.deltaTime;
        
        
        if (Input.GetKey(KeyCode.R))
        {
            ResetLevel();
        }
    }

    void LoseMatch()
    {
        print("Perdite");
        //mostrar la derrota.
        //sacarte y llevarte al scoreboard.
        Invoke("ResetLevel", timeToNextLevel);
        landzones.Clear();
    }

    void WinMatch()
    {
        print("Ganaste");
        //mostrar la victoria. //listo desde ui.
        score += 50* getMultiply(); //sumar los puntos. 
        print("score");
        OnScore?.Invoke(score);
        Invoke("ResetLevel", timeToNextLevel);
        landzones.Clear();
    }

    private void ResetLevel()
    {
        player.transform.position = initialShip.t;
        player.transform.rotation = initialShip.r;
        player.transform.localScale = initialShip.s;
        generator.GenerateTerrain();
        shipManager.show();
        timeInGame = 0;
        OnReset?.Invoke();
    }
    int getMultiply()
    {
        foreach (var go in landzones)
        {
            if (go.onContact)
            {
                return go.multiplier;
            }
            
        }
        return 1;
    }
}
