using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
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
        if(transform.position == position1)
        {
            nextpos = position2;
        }
        if (transform.position == position2)
        {
            nextpos = position1;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextpos, moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(null);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(position1, position2);
    }

}
