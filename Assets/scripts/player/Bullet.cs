using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   

    public float speed = 5f, bulletDamage;
    private Transform aim;
    
    private Vector2 target, currentPosition;
    private Vector3 dir;

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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.transform.SendMessage("Damage", bulletDamage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Sayah"))
        {
            collision.transform.SendMessage("BulletDamage", bulletDamage);
            Destroy(gameObject);
        }
    }


}
