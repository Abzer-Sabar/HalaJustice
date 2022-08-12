using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [HideInInspector]  
    public bool canPatrol;
    public float speed;
    public float distance;

    private bool movingRight = true;

    public Transform groundDetection;

    private void Start()
    {
        canPatrol = true;
    }
    private void Update()
    {
        startPatrol();
        flip();
    }

    private void startPatrol()
    {
        if (canPatrol)
        {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        }
    }

    private void flip()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2.0f, LayerMask.GetMask("Ground"));
        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
}
