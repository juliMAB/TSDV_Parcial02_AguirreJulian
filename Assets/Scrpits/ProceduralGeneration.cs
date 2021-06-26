using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] int minHeight, maxHeight;
    
    [SerializeField] int FirstHeight;
    [Range(6, 100)]
    [SerializeField] int cantNodosExtras;

    [Range(1, 6)]
    [SerializeField] int landZones;

    [Tooltip("Components")]
    [SerializeField] SpriteShapeController sSC;
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

    void terrenogernera()
    {
        maxHight = bounds.transform.localScale.y / 1.5f;
        float hightPoint=0;
        zonesList.Clear();
        Vector3 posinitial = Vector3.left * bounds.transform.localScale.x/2 + Vector3.down * bounds.transform.localScale.y/2;
        Vector3 lastposition;
        sSC.spline.Clear();
        sSC.spline.InsertPointAt(0, Vector3.up * FirstHeight + posinitial);
        lastposition = sSC.spline.GetPosition(0);
        zonesList.Add(zones.bounds);
        //generar terreno basico con elevaciones y bajadas.
        for (int i = 1; i < cantNodosExtras; i++)
        {
            sSC.spline.InsertPointAt(i, Vector3.up * (Random.Range(minHeight, maxHeight) * 0.1f) + Vector3.up * lastposition.y + Vector3.right * posinitial.x + Vector3.right * i * bounds.transform.localScale.x / (cantNodosExtras - 1));
            lastposition = sSC.spline.GetPosition(i);
            if (hightPoint<= sSC.spline.GetPosition(i).y)
            {
                hightPoint = sSC.spline.GetPosition(i).y;
            }
            //si esta por debajo del piso lo sube.
            if (posinitial.y+1 >= lastposition.y)
            {
                sSC.spline.SetPosition(i, new Vector3(sSC.spline.GetPosition(i).x, posinitial.y+2, 0f));
                lastposition = sSC.spline.GetPosition(i);
            }
            //si esta por ensima del maximo, lo baja.
            else if (maxHight <= lastposition.y)
            {
                sSC.spline.SetPosition(i, new Vector3(sSC.spline.GetPosition(i).x, maxHight - 2, 0f));
                lastposition = sSC.spline.GetPosition(i);
            }
            zonesList.Add(zones.normal);
        }
        

        //generar zonas de aterrizaje que sean rectas.
        sSC.spline.InsertPointAt(cantNodosExtras, posinitial + Vector3.right * bounds.transform.localScale.x);
        sSC.spline.InsertPointAt(cantNodosExtras + 1, posinitial);
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
                sSC.spline.SetPosition(index, new Vector3(sSC.spline.GetPosition(index).x, sSC.spline.GetPosition(index - 1).y, 0));
                GameObject go = Instantiate(prefabText, sSC.spline.GetPosition(index), Quaternion.identity, textConteiner.transform);
                go.name = "LandZone" + i;
                zonesList[index--] = zones.save; index++;
                zonesList[index] = zones.save;
            }
        }
        
    }
    void Start()
    {
        //if (sSC==null)
        //{
        //    GameObject go = new GameObject();
        //    go.name = "GROUND";
        //    go.AddComponent<SpriteShapeController>();
        //    go.GetComponent<SpriteShapeController>().spriteShape.fillTexture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/ART/Sprites/Nature2.jpg", typeof(Texture2D));
        
        //}
        
        terrenogernera();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.P)){
            terrenogernera();
        }
    }
}
