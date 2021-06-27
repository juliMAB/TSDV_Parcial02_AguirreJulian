using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipCheats : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
            transform.position += (Vector3)Vector2.up;
        }
        else if (Input.GetKey(KeyCode.J))
        {
            transform.position += (Vector3)Vector2.left;
        }
        else if (Input.GetKey(KeyCode.K))
        {
            transform.position += (Vector3)Vector2.down;
        }
        else if (Input.GetKey(KeyCode.L))
        {
            transform.position += (Vector3)Vector2.right;
        }

    }
}
