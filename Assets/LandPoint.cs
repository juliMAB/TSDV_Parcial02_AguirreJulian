using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandPoint : MonoBehaviour
{
    public bool onContact;
    public int multiplier;
    private void OnCollisionStay2D(Collision2D collision)
    {
        onContact = true;
    }
}
