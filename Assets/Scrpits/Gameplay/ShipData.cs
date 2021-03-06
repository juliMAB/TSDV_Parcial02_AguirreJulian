using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShipData
{
    public Action<int> OnLowFuel;
    public bool enabled;

    [SerializeField] float motorForce;
    public float force { get { return motorForce; } }

    [SerializeField] float fuelShip;
    public float fuel { get { return fuelShip; } }

    [SerializeField] float totalFuel;
    public float initialfuel { get { return totalFuel; } }

    [SerializeField] float rotationAngle;
    public float angle { get { return rotationAngle; } }

    [SerializeField] float zoomDistance;
    public float distanceToZoom { get { return zoomDistance; } }
    
    [SerializeField] LayerMask floorLayer;
    public LayerMask layerfloor { get { return floorLayer; } }

    [SerializeField] float maxVelocityToLand;
    public float maxVelocityToLanding { get { return maxVelocityToLand; } }

    //------------------------------------
    float distancetoFloor;
    public float altitude { get { return distancetoFloor; } set { distancetoFloor = value; } }

    private Rigidbody2D rigidbody2D;
    public Rigidbody2D rb2d { get { return rigidbody2D; } set { rigidbody2D = value; } }

    private ParticleSystem particleSystem;
    public ParticleSystem particleS { get { return particleSystem; } set { particleSystem = value; } }

    //------------------------------------
    public void initialFuel()
    {
        fuelShip = totalFuel;
    }

    public void lessFuel(float cant)
    {
        fuelShip -= cant;
        if (0.20f > fuel / totalFuel)
        {
            OnLowFuel?.Invoke(20);
            if (0 >= fuel / totalFuel)
                OnLowFuel?.Invoke(0);
        }
            

    }

    
}
