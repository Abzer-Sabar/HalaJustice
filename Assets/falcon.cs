using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falcon : MonoBehaviour
{
    public float moveSpeed, offset;
    [HideInInspector]
    public bool enemyInSight;
    private Transform player;
    private Vector2 target;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        target = new Vector2(player.position.x, player.position.y + offset);
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        if (enemyInSight)
        {
            Debug.Log("Enemy is in sight");
            shoot();
        }
    }


    private void shoot()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyInSight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyInSight = false;
        }
    }
}
