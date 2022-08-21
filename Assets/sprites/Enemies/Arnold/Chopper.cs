using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chopper : MonoBehaviour
{
    public float moveSpeed;

    Vector3 nextpos;
    public Vector3 position1, position2, startPosition;
   
    private void Start()
    {
        nextpos = startPosition;
    }

    private void Update()
    {
       
        if (transform.position == position1)
        {
           
            nextpos = position2;
        }
        if (transform.position == position2)
        {
            
            nextpos = position1;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextpos, moveSpeed * Time.deltaTime);
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(position1, position2);
    }

}
