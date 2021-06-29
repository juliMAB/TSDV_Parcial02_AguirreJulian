using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class BackgroundController : MonoBehaviourSingleton<BackgroundController>
{
    [SerializeField] GameObject layerPrefab = null;
    [SerializeField] [Range(0, 1)] float backgroundSizeX = .5f;
    [SerializeField] List<Background> backgroundList = null;
    private List<GameObject> currentBackground = new List<GameObject>();

    private void Start()
    {
        StartBackground();
    }

    public void StartBackground()
    {
        foreach (var layer in currentBackground)
        {
            Destroy(layer);
        }

        currentBackground.Clear();

        foreach (var layer in backgroundList[LoaderManager.Get().GetCurrentLevel()].backgroundLayersList)
        {
            GameObject go = Instantiate(layerPrefab);
            go.name = layer.layerName;
            go.transform.parent = transform;
            currentBackground.Add(go);

            go.GetComponent<Animator>().SetFloat("SpeedMultiplier", layer.speedMultiplier);

            var renderer = go.GetComponent<SpriteRenderer>();
            renderer.sprite = layer.sprite;
            renderer.sortingOrder = layer.drawOrder;

            go.transform.localScale = Vector3.one;

            var width = renderer.sprite.bounds.size.x;
            var height = renderer.sprite.bounds.size.y;

            float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
            float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

            go.transform.localScale = new Vector2((worldScreenWidth / width) * backgroundSizeX, worldScreenHeight / height);
            renderer.size = new Vector2(renderer.size.x, renderer.size.y * 3);
        }
    }
}