using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private ShipData data;
    public ShipData setData { set { data = value; } }

    public ShipData GetData() { return data; }
    public Action OnFuelUse;
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward, data.angle, Space.Self);

        }
        else if (Input.GetKeyDown(KeyCode.D)|| Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.forward, -data.angle, Space.Self);
        }
        else if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.Space)) && data.fuel > 0)
        {
            EngineRunning();
        }
        else if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) && data.fuel > 0)
        {
            EngineRunning();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            data.particleS.Stop();
        }
        //actualizar la distacian al piso.
        data.altitude = Vector3.Distance(transform.position+Vector3.down* transform.localScale.y/2,(Physics2D.Raycast(transform.position+transform.localScale/2, Vector2.down)).point);
        Debug.DrawRay(transform.position + Vector3.down * transform.localScale.y / 2,Vector3.down);
    }
    void UseFuel()
    {
        data.lessFuel(0.1f);
        OnFuelUse?.Invoke();
    }
    void EngineRunning()
    {
        data.rb2d.AddForce(transform.up * data.force * 0.0001f);
        data.particleS.Play();
        UseFuel();
    }
}
