using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public Action OnDestroy;
    [SerializeField] float maxVelocityToLand;
    public Action OnZoomIn;
    public Action OnZoomOut;
    bool zoomIn=false;
    ShipController shipC;
    [SerializeField] GameObject destroyShip;

    private void Start()
    {
        shipC = GetComponent<ShipController>();
        OnDestroy += Destroing;
    }
    void Destroing()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        Instantiate(destroyShip,transform.position,Quaternion.identity, (new GameObject("Basura")).transform);
        Destroy(gameObject, 2);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Mathf.Abs (shipC.rb.velocity.x)+ Mathf.Abs(shipC.rb.velocity.y)>maxVelocityToLand)
        {
            print("Explote!");
            OnDestroy?.Invoke();
        }
    }
    private void Update()
    {
        if (shipC.OnZoomDistance()!=zoomIn)
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
