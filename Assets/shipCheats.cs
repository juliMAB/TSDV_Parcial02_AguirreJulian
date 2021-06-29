using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipCheats : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
            transform.position += (Vector3)Vector2.up*Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.J))
        {
            transform.position += (Vector3)Vector2.left * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.K))
        {
            transform.position += (Vector3)Vector2.down * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.L))
        {
            transform.position += (Vector3)Vector2.right * Time.deltaTime;
        }

    }
}
