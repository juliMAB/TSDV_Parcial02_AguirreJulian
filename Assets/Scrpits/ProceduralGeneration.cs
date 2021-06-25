using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] int minHeight, maxHeight;
    [Range(6,100)]
    [SerializeField] int cantNodosExtras;
    [SerializeField] int landZones;
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
        for (int i = 0; i < landZones; i++)
        {
            int index = Random.Range(3, cantNodosExtras - 2);
            sSC.spline.SetPosition (index, new Vector3 ( sSC.spline.GetPosition(index).x, sSC.spline.GetPosition(index-1).y,0));
        }
        sSC.spline.InsertPointAt(cantNodosExtras, posinitial +Vector3.right * bounds.transform.localScale.x);
        sSC.spline.InsertPointAt(cantNodosExtras+1, posinitial);
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


    GameObject spawnObj(GameObject obj,int width,int height)//What ever we spawn will be a child of our procedural generation gameObj
    {
        obj = Instantiate(obj, new Vector2(width, height), Quaternion.identity);
        obj.transform.parent = this.transform;
        return obj;
    }
}
