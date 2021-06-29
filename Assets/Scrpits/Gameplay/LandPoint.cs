using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandPoint : MonoBehaviour
{
    public bool onContact=false;
    public int multiplier;
    //es trigget
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            onContact = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            onContact = false;
    }
}
