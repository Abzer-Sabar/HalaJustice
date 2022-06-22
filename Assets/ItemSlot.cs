using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public playerInventory inventory;
    public playerController controller;
    public playerCombat combat;
    public playerHealth health;
    public KeyCode keycode = KeyCode.E;
    public int i;

    private float tempMoveSpeed = 10f, tempAttackDamage = 40;


    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<playerInventory>();
    }

    private void Update()
    {
        if(transform.childCount <= 0)
        {
            inventory.isFull[i] = false;
        }

        if(Input.GetKey(keycode) && transform.childCount != 0)
        {
            foreach(Transform child in transform)
            {
                if (child.CompareTag("Date"))
                {
                    consumeDate();
                    Destroy(child.gameObject);
                }

                if (child.CompareTag("Coffee"))
                {
                    StartCoroutine(consumeCoffee());
                    Destroy(child.gameObject);
                }

                if (child.CompareTag("Laban"))
                {
                    StartCoroutine(consumeLaban());
                    Destroy(child.gameObject);
                }
            }
        }
    }
    private void consumeDate()
    {
        float healValue = 20;
        health.healPlayer(healValue);
    }

   IEnumerator consumeCoffee()
    {
        controller.applyMovementPowerup(tempMoveSpeed);
        yield return new WaitForSeconds(10);
        Debug.Log("Time up");
        controller.revertMovement();
    }

    IEnumerator consumeLaban()
    {
        combat.attackPowerup(tempAttackDamage);
        yield return new WaitForSeconds(10);
        combat.revertAttackDamage();
    }
}
