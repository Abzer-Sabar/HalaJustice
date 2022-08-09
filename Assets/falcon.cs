using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falcon : MonoBehaviour
{
    public float moveSpeed, offset, startTimebtwShots;
    public GameObject plasmaBallPrefab;
    [HideInInspector]
    public bool enemyInSight;
    private Transform player;
    private FalconPlasma falconPlasma;
    private Vector2 target;
    private float timeBtwShots;

 
    private GameObject enemy; 

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timeBtwShots = startTimebtwShots;
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
        if(timeBtwShots <= 0)
        {
            GameObject plasma = Instantiate(plasmaBallPrefab, transform.position, Quaternion.identity);
            plasma.GetComponent<FalconPlasma>().enemy = enemy;
            timeBtwShots = startTimebtwShots;        
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
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
        else
        {
            Destroy(gameObject);
        }
    }
}
