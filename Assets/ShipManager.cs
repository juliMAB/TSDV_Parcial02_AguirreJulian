using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    [SerializeField] ShipData data = new ShipData();
    //------------------------
    public Action OnDestroy;
    public Action OnFastMove;
    public Action OnZoomIn;
    public Action OnZoomOut;
    //------------------------
    [SerializeField] GameObject destroyShip;
    bool zoomIn=false;
    ShipController ShipController;
    Vector2 lastVelocity;

    private void Start()
    {
        data.rb2d = GetComponent<Rigidbody2D>();
        data.particleS = GetComponent<ParticleSystem>();
        ShipController = GetComponent<ShipController>();
        ShipController.setData = data;
        OnDestroy += Destroing; 

    }
    void Destroing()
    {
        //GetComponent<SpriteRenderer>().enabled = false;
        //GetComponent<BoxCollider2D>().enabled = false;
        //Instantiate(destroyShip,transform.position,Quaternion.identity, (new GameObject("Basura")).transform);
        //Destroy(gameObject, 1);
    }
    private void FixedUpdate()
    {
        lastVelocity = data.rb2d.velocity;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Mathf.RoundToInt((Mathf.Abs (lastVelocity.x)+ Mathf.Abs(lastVelocity.y)))*10>data.maxVelocityToLanding)
        {
            print("velocidad actual: " + Mathf.RoundToInt( (Mathf.Abs(lastVelocity.x * 10) + Mathf.Abs(lastVelocity.y * 10))));
            print("Explote!");
            Instantiate(destroyShip, transform.position, Quaternion.identity, (new GameObject("Basura")).transform);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            OnDestroy?.Invoke();
        }
        print("Choque!");
    }
    bool OnZoomDistance()
    {
        return Physics2D.OverlapCircle(transform.position, data.distanceToZoom, data.layerfloor) != null;
    }
    void Update()
    {
        if (OnZoomDistance()!=zoomIn)
        {
            if (zoomIn==true)
            {
                print("Saliendo del zoom");
                zoomIn = false;
                OnZoomOut?.Invoke();
            }
            else
            {
                print("Entrando al zoom");
                zoomIn = true;
                OnZoomIn?.Invoke();
            }
        }
    }
}
