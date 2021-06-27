using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] float MotorForce;
    [SerializeField] float fuel;
    [SerializeField] float rotationIntervalsOnDgres;
    [SerializeField] float zoomDistance;
    [SerializeField] LayerMask floorLayer;


    float distancetoFloor;
    private Rigidbody2D rb2d;
    public Rigidbody2D rb { get { return rb2d; } }
    private ParticleSystem particleS;
    public Action OnFuelUse;
    

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        particleS = GetComponent<ParticleSystem>();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Rotate(Vector3.forward, rotationIntervalsOnDgres, Space.Self);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Rotate(Vector3.forward, -rotationIntervalsOnDgres, Space.Self);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            rb2d.AddForce(transform.up* MotorForce * 0.0001f);
            particleS.Play();
            UseFuel();
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            rb2d.AddForce(transform.up * MotorForce*0.0001f);
            particleS.Play();
            UseFuel();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            particleS.Stop();
        }
        //actualizar la distacian al piso.
        distancetoFloor = Vector3.Distance(transform.position,(Physics2D.Raycast(transform.position+transform.localScale/2, Vector2.down)).point);
    }
    void UseFuel()
    {
        fuel -= 0.1f;
        OnFuelUse?.Invoke();
    }
    public bool OnZoomDistance()
    {
        return Physics2D.OverlapCircle(transform.position, zoomDistance, floorLayer) !=null;
    }
}
