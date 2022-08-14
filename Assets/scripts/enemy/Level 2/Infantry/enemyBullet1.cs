using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet1 : MonoBehaviour
{
    public float speed = 5f, bulletDamage, bulletLifeTime = 5f;
    private Transform player;

    private Vector2 target;
    private Vector3 dir;
    private float startTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        target = new Vector2(player.position.x, player.position.y);

        dir = target - new Vector2(transform.position.x, transform.position.y);
    }

    private void Update()
    {
        startTime += Time.deltaTime;
        transform.Translate(dir.normalized * speed * Time.deltaTime);
        if(startTime > bulletLifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SendMessage("takeDamage", bulletDamage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

}
