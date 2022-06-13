using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAi : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed, attackDistance, awakeRange;

    [SerializeField]
    private Transform player;

    private Rigidbody2D rb;
    private Animator anim;

    private bool isFacingRight = false;
    private states state;
    public enum states
    {
        idle,
        chasing,
        attacking
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        state = states.idle;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        switch (state)
        {
            case states.idle:
                playerCheck();
                break;

                case states.chasing:
                applyMovement();
                break;
        }
    }

    private void playerCheck()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        if(distToPlayer < awakeRange)
        {
            state = states.chasing;
            anim.SetBool("moving", true);
        }
        else
        {
            state = states.idle;
            anim.SetBool("moving", false);
        }
    }

    private void applyMovement()
    {
        if (Vector2.Distance(transform.position, player.position) > attackDistance)
        {
            if (transform.position.x < player.position.x)
            {
                // the enemy is on the left
                rb.velocity = new Vector2(moveSpeed, 0f);
                transform.localScale = new Vector2(3, 3);
            }
            else
            {
                // the enemy is on the right
                rb.velocity = new Vector2(-moveSpeed, 0f);
                transform.localScale = new Vector2(-3, 3);
            }
        }
    }

    private void stopChasing()
    {
        rb.velocity = Vector2.zero;
    }

    private void flip()
    { 
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);

    }

}
