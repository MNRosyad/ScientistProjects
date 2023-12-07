using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltSpeed : MonoBehaviour
{
    SurfaceEffector2D surfaceEffector;

    private void Awake()
    {
        surfaceEffector = GetComponent<SurfaceEffector2D>();
    }

    public float ChangeSpeed(float speed)
    {
        return surfaceEffector.speed = speed;
    }
}
