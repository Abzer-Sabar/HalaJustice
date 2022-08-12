using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed;

    private Transform player;
    private Vector2 targetPos;
    private Rigidbody2D rb;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        targetPos = new Vector2(player.position.x, player.position.y);
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(targetPos * bulletSpeed, ForceMode2D.Impulse);
    }
}
