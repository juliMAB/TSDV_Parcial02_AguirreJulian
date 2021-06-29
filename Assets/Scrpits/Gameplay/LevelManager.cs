using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] ProceduralGeneration generator=null;
    [SerializeField] GameObject player = null;
    [NonSerialized] ShipManager shipManager = null;
    //------------------------
    public Action<int> OnScore;
    public Action OnReset;
    //------------------------
    [NonSerialized] int score;
    [NonSerialized] float timeToNextLevel;
    [NonSerialized] List<LandPoint> landzones = new List<LandPoint>();
    [NonSerialized] float timeInGame; public float timeGame { get { return timeInGame; } }

    [NonSerialized] bool timeRuning;
    [NonSerialized] bool pause=false;

    TRS initialShip;


    struct TRS
    {
        public Vector3 t;
        public Quaternion r;
        public Vector3 s;
    }
    public void Pause()
    {
        pause = !pause;
        if (!pause)
        {
            Time.timeScale = 1;
            shipManager.GetData().enabled = true;
        }
        else
        {
            Time.timeScale = 0;
            shipManager.GetData().enabled = false;
        }
    }

    public ShipManager GetShip()
    {
        return shipManager;
    }
    void Start()
    {
        generator = FindObjectOfType<ProceduralGeneration>();
        player = FindObjectOfType<ShipManager>().gameObject;
        pause = false;
        timeRuning = true;
        shipManager = player.GetComponent<ShipManager>();
        shipManager.StartPlayer();
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
            ResetLevel();
    }

    void LoseMatch()
    {
        print("Perdite");   //mostrar la derrota.
        //sacarte y llevarte al scoreboard.
        Invoke("ResetLevel", timeToNextLevel);
        landzones.Clear();

    }

    void WinMatch()
    {
        print("Ganaste");
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
        shipManager.StartPlayer();
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
