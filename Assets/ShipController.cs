using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] float MotorForce;
    [SerializeField] float rotationIntervalsOnDgres;
    private Rigidbody2D rb2d;
    public Rigidbody2D rb { get { return rb2d; } }
    private ParticleSystem particleS;


    public Action OnFuelUse;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        particleS = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
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
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            rb2d.AddForce(transform.up * MotorForce*0.0001f);
            particleS.Play();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            particleS.Stop();
        }
    }
}
