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
    public Action OnLanding;
    //------------------------
    [SerializeField] GameObject destroyShip;
    bool zoomIn=false;
    ShipController ShipController;
    Vector2 lastVelocity;
    float ofset;
    RaycastHit2D hitImpact;
    public ShipData GetData()
    {
        return data;
    }

    private void Start()
    {
        data.rb2d = GetComponent<Rigidbody2D>();
        data.particleS = GetComponent<ParticleSystem>();
        ShipController = GetComponent<ShipController>();
        ShipController.setData = data;
        OnDestroy += Destroing;
        ofset = transform.GetComponent<BoxCollider2D>().size.y/2;
    }
    void Destroing()
    {
        //GetComponent<SpriteRenderer>().enabled = false;
        //GetComponent<BoxCollider2D>().enabled = false;
        //Instantiate(destroyShip,transform.position,Quaternion.identity, (new GameObject("Basura")).transform);
        //Destroy(gameObject, 1);
    }
    public void hide()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<ParticleSystem>().Pause();
    }
    public void show()
    {

    }
    private void FixedUpdate()
    {
        lastVelocity = data.rb2d.velocity;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (Mathf.RoundToInt((Mathf.Abs(lastVelocity.x) + Mathf.Abs(lastVelocity.y))) * 10 > data.maxVelocityToLanding /*|| Vector2.Distance(hitImpact.normal, transform.position) > 10*/)
        {
            print("velocidad actual: " + Mathf.RoundToInt( (Mathf.Abs(lastVelocity.x * 10) + Mathf.Abs(lastVelocity.y * 10))));
            print("Explote!");
            Instantiate(destroyShip, transform.position, Quaternion.identity, (new GameObject("Basura")).transform);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            OnDestroy?.Invoke();
        }
        //print("Choque!");
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

        //actualizar la distacian al piso.
        hitImpact = (Physics2D.Raycast(transform.position ,Vector2.down,20,data.layerfloor));
        data.altitude = Vector3.Distance(transform.position+ Vector3.down*ofset, hitImpact.point);
        circulo(hitImpact.point);
        Debug.DrawRay(transform.position +Vector3.down* ofset, Vector3.down);
    }
    void circulo(Vector3 a)
    {
        Debug.DrawLine(a, a+Vector3.left);
        Debug.DrawLine(a, a+Vector3.right);
        Debug.DrawLine(a, a+Vector3.up);
        Debug.DrawLine(a, a+Vector3.down);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.gameObject.tag == "LandingZone")
        {
            if (data.rb2d.velocity.y == 0)
            {
                if (transform.up == Vector3.up)
                {
                    OnLanding?.Invoke();
                    print("aterize!");
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}
