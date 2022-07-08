using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    public Transform pos1, pos2;
    public float moveSpeed;
    public Transform startPos;
    Vector3 nextpos;

    private void Start()
    {
        nextpos = startPos.position;
    }

    private void Update()
    {
        if(transform.position == pos1.position)
        {
            nextpos = pos2.position;
        }
        if (transform.position == pos2.position)
        {
            nextpos = pos1.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextpos, moveSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }

}
