using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoneSwitcher : MonoBehaviour
{
    public string triggerTag;

    public CinemachineVirtualCamera primaryCam;
    public CinemachineVirtualCamera[] virtualCam;

    void Start()
    {
        SwitchToCamera(primaryCam);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(triggerTag))
        {
            CinemachineVirtualCamera targetCamera = collision.GetComponentInChildren<CinemachineVirtualCamera>();
            SwitchToCamera(targetCamera);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(triggerTag))
        {
            SwitchToCamera(primaryCam);
        }
    }

    private void SwitchToCamera(CinemachineVirtualCamera targetCamera)
    {
        foreach (CinemachineVirtualCamera camera in virtualCam)
        {
            camera.enabled = camera == targetCamera;
        }
    }

    [ContextMenu("Get All Virtual Cameras")]
    private void GetAllVirtualCameras()
    {
        virtualCam = GameObject.FindObjectsOfType<CinemachineVirtualCamera>();
    }
}
