using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

public class ProceduralGeneration : MonoBehaviour
{
    [Tooltip("the next point takes a random height between these two values, adding the current height ")]
    [SerializeField] int minHeight, maxHeight;
    [Tooltip("height of the first point")]
    [SerializeField] int FirstHeight;
    [Range(6, 100)]
    [Tooltip("number of points on the map")]
    [SerializeField] int cantNodosExtras;
    [Range(1, 6)]
    [SerializeField] int landZones;

    [SerializeField] SpriteShapeController spriteShapeController;
    [SerializeField] GameObject bounds;

    [SerializeField] GameObject prefabText;
    [SerializeField] GameObject textConteiner;


    float maxHight;

    private List <zones> zonesList = new List<zones>();
    private enum zones
    {
        bounds,
        normal,
        save,
        mountain,
        last
    }
    public void GenerateTerrain()
    {
        maxHight = bounds.transform.localScale.y / 2;
        float hightPoint=0;
        zonesList.Clear();
        Vector3 posinitial = Vector3.left * bounds.transform.localScale.x/2 + Vector3.down * bounds.transform.localScale.y/2;
        Vector3 lastposition;
        spriteShapeController.spline.Clear();
        spriteShapeController.spline.InsertPointAt(0, Vector3.up * FirstHeight + posinitial);
        lastposition = spriteShapeController.spline.GetPosition(0);
        zonesList.Add(zones.bounds);
        //generar terreno basico con elevaciones y bajadas.
        for (int i = 1; i < cantNodosExtras; i++)
        {

            spriteShapeController.spline.InsertPointAt(i, Vector3.up * (Random.Range(minHeight, maxHeight) * 0.1f) + Vector3.up * lastposition.y + Vector3.right * posinitial.x + Vector3.right * i * bounds.transform.localScale.x / (cantNodosExtras - 1));
            lastposition = spriteShapeController.spline.GetPosition(i);
            if (hightPoint<= spriteShapeController.spline.GetPosition(i).y)
            {
                hightPoint = spriteShapeController.spline.GetPosition(i).y;
            }
            //si esta por debajo del piso lo sube.
            if (posinitial.y+1 >= lastposition.y)
            {
                spriteShapeController.spline.SetPosition(i, new Vector3(spriteShapeController.spline.GetPosition(i).x, posinitial.y+2, 0f));
                lastposition = spriteShapeController.spline.GetPosition(i);
            }
            //si esta por ensima del maximo, lo baja.
            else if (maxHight <= lastposition.y)
            {
                spriteShapeController.spline.SetPosition(i, new Vector3(spriteShapeController.spline.GetPosition(i).x, maxHight - 2, 0f));
                lastposition = spriteShapeController.spline.GetPosition(i);
            }
            zonesList.Add(zones.normal);
        }
        

        //generar zonas de aterrizaje que sean rectas.
        spriteShapeController.spline.InsertPointAt(cantNodosExtras, posinitial + Vector3.right * bounds.transform.localScale.x);
        spriteShapeController.spline.InsertPointAt(cantNodosExtras + 1, posinitial);
        zonesList.Add(zones.bounds);
        foreach (Transform child in textConteiner.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < landZones; i++)
        {
            int index = Random.Range(3, cantNodosExtras - 2);
            if (zonesList[index] == zones.save || zones.save == zonesList[index + 1] || zones.save == zonesList[index - 1])
            {
                i--; //cuidado puede quedar atrapado en el bucle.
            }
            else
            {
                for (int j = index-1; j < index - 1+3; j++)
                {
                    Vector3 pos = new Vector3(spriteShapeController.spline.GetPosition(j).x, spriteShapeController.spline.GetPosition(index).y);
                    spriteShapeController.spline.SetPosition(j, pos);
                }
                GameObject go = new GameObject(name = "landZoneCol");
                go.transform.position = spriteShapeController.spline.GetPosition(index);
                go = Instantiate(prefabText, spriteShapeController.spline.GetPosition(index), Quaternion.identity, textConteiner.transform);
                go.name = "LandZone" + i;
                go.transform.position += Vector3.down * 0.2f;
                go.GetComponentInChildren<TextMeshPro>().text = "X2";
                

                zonesList[index--] = zones.save; index++;
                zonesList[index] = zones.save;
                zonesList[index++] = zones.save; index--;
            }
        }  
    }
}
