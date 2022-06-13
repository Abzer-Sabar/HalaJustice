using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombat : MonoBehaviour
{
    [SerializeField]
    private bool combatEnabled;
    [SerializeField]
    private float inputTimer, attackRadius, attackDamage;
    [SerializeField]
    private Transform attackHitboxPos;
    [SerializeField]
    private LayerMask whatIsDamageble;

    private bool gotInput, isAttacking, isFirstAttack;

    private float lastInputTime = Mathf.NegativeInfinity;

    private Animator animator;
    private float[] attackDetails = new float[2];
    private playerController pc;
    private playerHealth ph;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
        pc = this.GetComponent<playerController>();
        ph = this.GetComponent<playerHealth>();
        animator.SetBool("canAttack", combatEnabled);
    }
    private void Update()
    {
        checkCombatInput();
        checkAttack();
    }

    private void checkCombatInput()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (combatEnabled)
            {
                gotInput = true;
                lastInputTime = Time.time;
            }
        }
    }

    private void checkAttack()
    {
        if (gotInput)
        {
            if (!isAttacking)
            {
                gotInput = false;
                isAttacking = true;
                isFirstAttack = !isFirstAttack;
                animator.SetBool("attack1", true);
                animator.SetBool("firstAttack", isFirstAttack);
                animator.SetBool("isAttacking", isAttacking);
            }

        }

        if(Time.time >= lastInputTime + inputTimer)
        {
            gotInput = false;
        }
    }

    public void checkAttackHitBox()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(attackHitboxPos.position, attackRadius, whatIsDamageble);

        attackDetails[0] = attackDamage;
        attackDetails[1] = transform.position.x;

        foreach (Collider2D colliders in objects)
        {
            colliders.transform.parent.SendMessage("Damage", attackDetails);

            
        }   
    }

    public void finishAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("attack1", false);
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackHitboxPos.position, attackRadius);
    }
}
