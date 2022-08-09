using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalconPlasma : MonoBehaviour
{
    public float moveSpeed = 5f, enemyDamage, lifeTime = 5f;
    [HideInInspector]
    public GameObject enemy;
    private Vector2 targetPosition, currenPosition;
    private float timer;
    private float[] damage = new float[2];

    private void Start()
    {
        damage[0] = enemyDamage;
    }
    private void Update()
    {

        targetPosition = new Vector2(enemy.gameObject.transform.position.x, enemy.gameObject.transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponentInParent<SmallEnemy>().Damage(damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
