using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    [SerializeField] GameObject layer1;
    [SerializeField] GameObject layer2;
    [SerializeField] GameObject layer3;
    [SerializeField] GameObject layer4;
    [SerializeField] GameObject layer5;
    [SerializeField] GameObject ship;
     Vector3 offset2;
     Vector3 offset3;
     Vector3 offset4;
     Vector3 offset5;
    private void Start()
    {
        offset2 = layer2.transform.position;
        offset3 = layer3.transform.position;
        offset4 = layer4.transform.position;
        offset5 = layer5.transform.position;

    }

    private void Update()
    {
        layer1.transform.position = ship.transform.position * -1/2;
        layer2.transform.position = ship.transform.position * -1/3 + offset2;
        layer3.transform.position = ship.transform.position * -1/6 + offset3;
        layer4.transform.position = ship.transform.position * -1/10 + offset4;
        layer5.transform.position = ship.transform.position * -1/16 + offset5;
    }
}
