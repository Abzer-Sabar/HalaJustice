using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   

    public float speed = 5f;
    private Transform aim;
    
    private Vector2 target, currentPosition;

    void Start()
    {
        aim = GameObject.FindGameObjectWithTag("Aim").GetComponent<Transform>();
        target = new Vector2(aim.position.x, aim.position.y);
        currentPosition = new Vector2(transform.position.x, transform.position.y);
        //rb = GetComponent<Rigidbody2D>();
        //normalizeDirection = (target.position - transform.position).normalized;
        //rb.velocity = normalizeDirection * speed;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target) <= 0)
        {
            Destroy(gameObject);
        }
    }


}
