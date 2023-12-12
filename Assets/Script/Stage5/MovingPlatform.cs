using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Animator Platform;

    public void movingPlatform()
    {
      Platform.SetTrigger("OpenPlatform");
    }

}
