using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject bulletBurstEffect;
    public float speed = 5f, bulletDamage, lifeTime = 4f;
    private Transform aim;
    
    private Vector2 target, currentPosition;
    private Vector3 dir;
    private float timer = 0f;

    void Start()
    {
        aim = GameObject.FindGameObjectWithTag("Aim").GetComponent<Transform>();
        target = new Vector2(aim.position.x, aim.position.y);
        dir = target - new Vector2(transform.position.x, transform.position.y);
    }

    private void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        transform.Translate(dir.normalized * speed * Time.deltaTime);
        timer += Time.deltaTime;
        if(timer >= lifeTime)
        {
            burstBullet();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.transform.SendMessage("Damage", bulletDamage);
            burstBullet();
        }
         if (collision.gameObject.CompareTag("Sayah"))
        {
            collision.transform.SendMessage("BulletDamage", bulletDamage);
            burstBullet();
        }

            burstBullet();
    }

    private void burstBullet()
    {
        Instantiate(bulletBurstEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
