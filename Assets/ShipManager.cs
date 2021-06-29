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
    public Action<Vector3> OnZoomIn;
    public Action OnZoomOut;
    public Action OnLanding;
    //------------------------
    [SerializeField] GameObject destroyShip;
    bool zoomIn=false;
    ShipController ShipController;
    Vector2 lastVelocity;
    float ofset;
    BoxCollider2D boxCollider2D;
    RaycastHit2D hitImpact;
    SpriteRenderer spriteRenderer;
    public ShipData GetData()
    {
        return data;
    }

    private void Start()
    {
        data.enabled = true;
        data.rb2d = GetComponent<Rigidbody2D>();
        data.particleS = GetComponent<ParticleSystem>();
        ShipController = GetComponent<ShipController>();
        ShipController.setData = data;
        OnDestroy += Destroing;
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ofset = boxCollider2D.size.y/2;

    }
    void Destroing()
    {
        print("Explote!");
        GameObject go = Instantiate(destroyShip, transform.position, Quaternion.identity, (new GameObject("Basura")).transform);
        hide();
        Detonate();
        Destroy(go, 5);
    }
    void hide()
    {
        spriteRenderer.enabled = false;
        boxCollider2D.enabled = false;
        data.enabled = false;
        data.rb2d.velocity = Vector2.zero;
        data.rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    public void show()
    {
        spriteRenderer.enabled = true;
        boxCollider2D.enabled = true;
        data.enabled = true;
        data.rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    private void FixedUpdate()
    {
        lastVelocity = data.rb2d.velocity;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (Mathf.RoundToInt((Mathf.Abs(lastVelocity.x) + Mathf.Abs(lastVelocity.y))) > data.maxVelocityToLanding /*|| Vector2.Distance(hitImpact.normal, transform.position) > 10*/)
        {
            OnDestroy?.Invoke();
            print("me rompi por exceso de facha.");
        }
        if (transform.rotation.eulerAngles != Vector3.zero)
        {
            OnDestroy?.Invoke();
            print("me rompi por no estar recto.");
            print(transform.rotation.eulerAngles);
        }
        //print("Choque!");
    }
    void Update()
    {
        if (Vector3.Distance(hitImpact.point,transform.position)<data.distanceToZoom)
        {
            if (zoomIn == false)
            {
                print("Entrando al zoom");
                zoomIn = true;
                OnZoomIn?.Invoke(hitImpact.point);
            }
        }
        else
        {
            if (zoomIn == true)
            {
                print("Saliendo del zoom");
                zoomIn = false;
                OnZoomOut?.Invoke();
            }
        }

        //actualizar la distacian al piso.
        hitImpact = (Physics2D.Raycast(transform.position ,Vector2.down,20,data.layerfloor));
        data.altitude = Vector3.Distance(transform.position+ Vector3.down*ofset, hitImpact.point);
        //borrar.
        circulo(hitImpact.point);
        Debug.DrawRay(transform.position +Vector3.down* ofset, Vector3.down);
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
    void Detonate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);
        foreach (Collider2D hit in colliders)
        {
            Vector2 direction = hit.transform.position - transform.position;

            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
            if (rb!=null)
            {
                rb.AddForce(direction * 100);
            }


        }
    }
    //borrar.
    void circulo(Vector3 a)
    {
        Debug.DrawLine(a, a + Vector3.left);
        Debug.DrawLine(a, a + Vector3.right);
        Debug.DrawLine(a, a + Vector3.up);
        Debug.DrawLine(a, a + Vector3.down);
    }
    
}
