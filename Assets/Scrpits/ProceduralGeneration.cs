using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] int minHeight, maxHeight;
    [Range(6,100)]
    [SerializeField] int cantNodosExtras;
    [SerializeField] int widht;
    [SerializeField] SpriteShapeController sSC;
    [SerializeField] GameObject bounds;


    void terrenogernera()
    {
        Vector3 posinitial = Vector3.left * bounds.transform.localScale.x/2 + Vector3.down * bounds.transform.localScale.y/2;
        sSC.spline.Clear();
        for (int i = 0; i < cantNodosExtras; i++)
        {
            sSC.spline.InsertPointAt(i, Vector3.up* Random.Range(minHeight,maxHeight)+posinitial +Vector3.right*i* bounds.transform.localScale.x/(cantNodosExtras-1));
        }
        sSC.spline.InsertPointAt(cantNodosExtras, posinitial +Vector3.right * bounds.transform.localScale.x);
        sSC.spline.InsertPointAt(cantNodosExtras+1, posinitial);
    }
    void Start()
    {
        terrenogernera();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.P)){
            terrenogernera();
        }
    }



    GameObject spawnObj(GameObject obj,int width,int height)//What ever we spawn will be a child of our procedural generation gameObj
    {
        obj = Instantiate(obj, new Vector2(width, height), Quaternion.identity);
        obj.transform.parent = this.transform;
        return obj;
    }
}
