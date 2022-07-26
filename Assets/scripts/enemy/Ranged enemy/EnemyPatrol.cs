using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private Transform enemy;

    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D enemyRb;

    [Header("Health Attributes")]
    [SerializeField] private float maxHealth;
    [SerializeField] private GameObject hitParticle, deathParticle;
    [SerializeField] private playerAttributes playerAtt;
    private float currentHealth;

    [SerializeField] private float moveSpeed, idleDuration;

    private float idleTimer;

    private Vector3 initScale;
    private Vector2 movement;

    private bool movingLeft;

    private Rigidbody rb;
    private int damageDirection;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }
    private void Awake()
    {
        initScale = enemy.localScale;
    }
    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
            {
                moveInDirection(-1);
            }
            else
            {
                directionChange();
            }
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
            {
                moveInDirection(1);
            }
            else
            {
                directionChange();
            }
        }

    }

    private void directionChange()
    {
        anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;

        if(idleTimer > idleDuration)
        movingLeft = !movingLeft;
    }
    private void moveInDirection(int direction)
    {
        idleTimer = 0;
        anim.SetBool("moving", true);
        this.enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * direction, initScale.y, initScale.z);

        //enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * moveSpeed, enemy.position.y, enemy.position.z);
        movement.Set(moveSpeed * direction, enemyRb.velocity.y);
        enemyRb.velocity = movement;
    }

    private void Damage(float[] attackDetails)
    {
        anim.SetTrigger("hurt");
        currentHealth -= attackDetails[0];
        Instantiate(hitParticle, enemy.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

        if (attackDetails[1] > this.transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }

        if (currentHealth > 0.0f)
        {
            applyKnockBack();
        }
        else if (currentHealth <= 0.0f)
        {
            die();
        }

    }

    private void applyKnockBack()
    {

    }

    private void die()
    {
        anim.SetTrigger("die");
        this.enabled = false;
        //playerAtt.setGold();
        StartCoroutine(killGameObject());
        Debug.Log("Enemy died");
    }

    IEnumerator killGameObject()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
