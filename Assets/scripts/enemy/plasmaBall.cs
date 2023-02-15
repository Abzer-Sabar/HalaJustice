using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plasmaBall : MonoBehaviour
{
    public GameObject explodeEffect;
    public float moveSpeed, playerDamage, lifeTime = 5f;

    private Transform player;
    private Vector2 targetPosition, currenPosition;
    private playerHealth ph;
    private float timer;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        targetPosition = new Vector2(player.position.x, player.position.y);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        currenPosition = transform.position;
        timer += Time.deltaTime;
        if(timer >= lifeTime)
        {
            Destroy(gameObject);
        }
        if(currenPosition == targetPosition)
        {
            DestroyPlasmaBall();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ph = collision.gameObject.GetComponent<playerHealth>();
            ph.takeDamage(playerDamage);
            DestroyPlasmaBall();
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            DestroyPlasmaBall();
        }

    }
    
   
    private void DestroyPlasmaBall()
    {
        Instantiate(explodeEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
