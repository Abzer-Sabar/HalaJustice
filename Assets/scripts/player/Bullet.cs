using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 normalizeDirection;

    public float speed = 5f;
    private Transform target;
    private Rigidbody2D rb;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Aim").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        normalizeDirection = (target.position - transform.position).normalized;
        rb.velocity = normalizeDirection * speed;
    }

   
}
