using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class ConveyorBeltControl : MonoBehaviour
{
    private bool changeDirection = false;
    [SerializeField]
    private GameObject conveyorBelt;
    private ConveyorBeltSpeed beltSpeed;
    private SurfaceEffector2D effector;
    private TilemapRenderer tileMap;
    private PlayerController playerControl;

    private void Awake()
    {
        beltSpeed = conveyorBelt.GetComponent<ConveyorBeltSpeed>();
        effector = conveyorBelt.GetComponent<SurfaceEffector2D>();
        tileMap = conveyorBelt.GetComponent<TilemapRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerControl = collision.gameObject.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (playerControl.Interaction == true)
        {
            if (!changeDirection)
            {
                effector.speed = beltSpeed.ChangeSpeed(2f);
                tileMap.enabled = beltSpeed.RenderTile(true);
                changeDirection = true;
            }
            else if (changeDirection)
            {
                effector.speed = beltSpeed.ChangeSpeed(-2f);
                tileMap.enabled = beltSpeed.RenderTile(false);
                changeDirection = false;
            }
        }
    }
}
