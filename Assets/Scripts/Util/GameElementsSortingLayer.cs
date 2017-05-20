using UnityEngine;
using System.Collections;


public class GameElementsSortingLayer : MonoBehaviour
{
    public const string LAYER_NAME = "GameElements";
    public int sortingOrder = 1;

    void Start()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        if (sprite)
        {
            sprite.sortingOrder = sortingOrder;
            sprite.sortingLayerName = LAYER_NAME;
        }
    }
}