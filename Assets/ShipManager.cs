using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : ShipController
{
    public Action OnDestroy;
    [SerializeField] float maxVelocityToLand;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("CollisionEnter " + collision.collider.name);
        if (Mathf.Abs (rb.velocity.x)+ Mathf.Abs(rb.velocity.y)>maxVelocityToLand)
        {
            print("Explote!");
            OnDestroy?.Invoke();
        }
        
    }
}
