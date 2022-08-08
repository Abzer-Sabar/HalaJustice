using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalconTrigger : MonoBehaviour
{
    public falcon Falcon;
    public float moveSpeed, playerDamage, lifeTime = 5f;
    private Vector2 targetPosition, currenPosition;
    private float timer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Falcon.enemyInSight = true;
        }
    }
}
