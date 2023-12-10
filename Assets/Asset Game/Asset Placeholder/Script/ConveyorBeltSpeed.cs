using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ConveyorBeltSpeed : MonoBehaviour
{
    SurfaceEffector2D surfaceEffector;
    TilemapRenderer tileMapRenderer;

    private void Awake()
    {
        surfaceEffector = GetComponent<SurfaceEffector2D>();
        tileMapRenderer = GetComponent<TilemapRenderer>();
    }

    public float ChangeSpeed(float speed)
    {
        return surfaceEffector.speed = speed;
    }

    public bool RenderTile(bool input)
    {
        return tileMapRenderer.enabled = input;
    }
}
