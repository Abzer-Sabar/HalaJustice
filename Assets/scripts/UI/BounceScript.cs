using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceScript : MonoBehaviour
{
    public float BounceHeight, bounceTime;

    private float yValue;

  
    private void OnEnable()
    {
        LeanTween.moveY(gameObject, BounceHeight, bounceTime).setLoopPingPong();
    }
}
