using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] ShipManager ship;

    Vector3 initialpos;
    private Camera maincamera;

    private void Start()
    {
        maincamera = Camera.main;
        initialpos = transform.position;
        ship.OnZoomIn += ZoomIn;
        ship.OnZoomOut += ZoomOut;
    }

    void ZoomIn(Vector3 point)
    {
        maincamera.orthographicSize /= 2;
        transform.position = Vector3.Lerp(ship.transform.position + Vector3.forward * initialpos.z, point, 0.5f)  ;
    }
    void ZoomOut()
    {
        maincamera.orthographicSize *= 2;
        transform.position = initialpos;
    }
}
