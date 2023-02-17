using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot2 : MonoBehaviour
{
    private playerInventory inventory;
    public playerController controller;
    public combat combat;
    public Manager2 manager;
    public playerHealth health;
    public GameObject falconPrefab;
    public Transform falconSpawn;
    public KeyCode keycode = KeyCode.E;
    public int i;

    private float tempMoveSpeed = 10f, tempAttackDamage = 40;



    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<playerInventory>();
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<playerHealth>();
        combat = GameObject.FindGameObjectWithTag("Player").GetComponent<combat>();
    }

    private void Update()
    {
        if (transform.childCount <= 0)
        {
            inventory.isFull[i] = false;
        }

        if (Input.GetKey(keycode) && transform.childCount != 0)
        {
            foreach (Transform child in transform)
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

                if (child.CompareTag("Tea"))
                {
                    StartCoroutine(consumeTea());
                    Destroy(child.gameObject);
                }
                if (child.CompareTag("Falcon"))
                {
                    StartCoroutine(falconAbility());
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
        Debug.Log("Movement increased");
        controller.applyMovementPowerup(tempMoveSpeed);
        yield return new WaitForSeconds(10);
        Debug.Log("Time up");
        controller.revertMovement();
    }

    IEnumerator consumeLaban()
    {
        Debug.Log("Damage Increased");
        combat.attackPowerup(tempAttackDamage);
        yield return new WaitForSeconds(10);
        combat.revertAttackDamage();
    }

    IEnumerator consumeTea()
    {
        Debug.Log("Gold is doubled");
        manager.goldMultiplier = 2;
        yield return new WaitForSeconds(10f);
        revertGoldMultiplier();
    }

    IEnumerator falconAbility()
    {
        GameObject falcon = Instantiate(falconPrefab, falconSpawn.position, Quaternion.identity);
        yield return new WaitForSeconds(10f);
        Destroy(falcon);
    }

    private void revertGoldMultiplier()
    {
        manager.goldMultiplier = 1;
    }
}
