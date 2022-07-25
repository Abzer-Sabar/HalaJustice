using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combat : MonoBehaviour
{
    [HideInInspector]
    public float increaseAttackDamage = 0f;

    public Transform attackPoint;
    public float attackRange = 0.2f;
    public LayerMask enemyLayers;
    public float attackDamage = 20f, attackRate = 3f;
    public GameObject powerupEffect;



    private Animator animator;
    private float nextAttackTime = 0f;
 
    private playerController pc;
    private playerHealth ph;
    private float[] attackDetails = new float[2];
  
    private void Start()
    {
        animator = this.GetComponent<Animator>();
        pc = GetComponent<playerController>();
        ph = GetComponent<playerHealth>();
        powerupEffect.SetActive(false);
    }

    private void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                AttackAnimation();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        
    }

    private void AttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    public void Attack()
    {

            Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            attackDetails[0] = attackDamage + increaseAttackDamage;
            attackDetails[1] = transform.position.x;
            foreach (Collider2D enemy in hit)
            {
                enemy.transform.parent.SendMessage("Damage", attackDetails);
            }
            

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void Damage(float[] attackDetails)
    {
        if (pc.getDashStatus() == false)
        {
            int direction;
            ph.takeDamage(attackDetails[0]);

            if (attackDetails[1] < transform.position.x)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
            pc.applyKnockBack(direction);

        }

    }



    //powerups
    public void attackPowerup(float attackDamage)
    {
        this.attackDamage = attackDamage;
        powerupEffect.SetActive(true);
    }

    public void revertAttackDamage()
    {
        this.attackDamage = 20f;
        powerupEffect.SetActive(false);
    }
}
