using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plasmaBall : MonoBehaviour
{
    public float moveSpeed;

    private Transform player;
    private Vector2 targetPosition;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        targetPosition = new Vector2(player.position.x, player.position.y);
    }

    private void Update()
    {
       transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DestroyPlasmaBall();
        }
    }

    private void DestroyPlasmaBall()
    {
        Destroy(this.gameObject);
    }
}
