using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Rotate(Vector3.forward, -45, Space.Self);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Rotate(Vector3.forward, 45, Space.Self);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            rb2d.AddForce(transform.up);
        }
    }
}
