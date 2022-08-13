using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falcon : MonoBehaviour
{
    public float moveSpeed, offset, fireRate =1f;
    public GameObject plasmaBallPrefab;
    [HideInInspector]
    public bool enemyInSight;
    private Transform player;
    private FalconPlasma falconPlasma;
    private Vector2 target;
    private float fireCountDown = 0f;
    float someScale;
    int direction;

    private GameObject enemy;
   
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
    }

    private void Update()
    {
        target = new Vector2(player.position.x, player.position.y + offset);
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        transform.rotation = player.rotation;
        if (enemyInSight)
        { 
            if (fireCountDown <= 0)
            {
                shoot();
                fireCountDown = 1f / fireRate;
            }
            fireCountDown -= Time.deltaTime;
        }


        
    }
           
    private void shoot()
    {
       
            GameObject plasma = Instantiate(plasmaBallPrefab, transform.position, Quaternion.identity);
            plasma.GetComponent<FalconPlasma>().enemy = enemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyInSight = true;
            enemy = collision.gameObject;
        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            enemyInSight = true;
            enemy = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyInSight = false;
            enemy = null;
        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            enemyInSight = false;
            enemy = null;
        }
        else
        {
            //Destroy(gameObject);
        }
    }
}
