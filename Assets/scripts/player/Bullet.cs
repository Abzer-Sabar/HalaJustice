using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject bulletBurstEffect;
    public float speed = 50f, bulletDamage, lifeTime = 4f;
    private Transform aim;
    
    private Vector2 target, currentPosition, moveDirection;
    private Vector3 dir;
    Vector2 mousePos;
    private float timer = 0f;
    private Rigidbody2D rb;
    float[] attackDamage = new float[2];

    void Start()
    {
        aim = GameObject.FindGameObjectWithTag("Aim").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        target = new Vector2(aim.position.x, aim.position.y);
        dir = target - new Vector2(transform.position.x, transform.position.y);
        attackDamage[1] = transform.position.x;
        attackDamage[0] = bulletDamage;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        moveDirection = (mousePos - rb.position).normalized;
    }

    private void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

       //transform.Translate(dir.normalized * speed * Time.deltaTime);
        timer += Time.deltaTime;
        if(timer >= lifeTime)
        {
            burstBullet();
        }

    }

    private void FixedUpdate()
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        // OPTIONAL: Redirect the existing velocity into the new up direction 
        // without this after rotating you would still continue to move into the same global direction    
        rb.velocity = speed * moveDirection;

        rb.velocity += moveDirection * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            burstBullet();
        }
        if (collision.gameObject.CompareTag("Canon"))
        {
            collision.transform.SendMessage("Damage", attackDamage[0]);
            burstBullet();
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.transform.SendMessage("Damage", attackDamage);
            burstBullet();
        }
        if (collision.gameObject.CompareTag("Sayah"))
        {
            collision.transform.SendMessage("BulletDamage", bulletDamage);
            burstBullet();
        }

        if (collision.gameObject.CompareTag("scorpio"))
        {
            collision.transform.parent.SendMessage("bulletDamage", bulletDamage);
            burstBullet();
        }

    }

 

    private void burstBullet()
    {
        Instantiate(bulletBurstEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
