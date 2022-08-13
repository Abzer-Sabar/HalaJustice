using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caveBoss : MonoBehaviour
{
    public GameObject triggerPoint, blockade1, blockade2, healthBar;
    [HideInInspector]
    public bool hasPlayerEntered;

    private Animator anim;
    private caveBossScript enemy;

    private void Start()
    {
        anim = GetComponent<Animator>();
        blockade1.SetActive(false);
        blockade2.SetActive(false);
        enemy = this.GetComponent<caveBossScript>();
        enemy.enabled = false;
        healthBar.SetActive(false);
    }

    private void Update()
    {
        if (hasPlayerEntered)
        {
           
            anim.SetTrigger("Awake");
            triggerPoint.SetActive(false);
            blockade1.SetActive(true);
            blockade2.SetActive(true);
            StartCoroutine(enableEnemy());
        }
    }

    IEnumerator enableEnemy()
    {
        yield return new WaitForSeconds(3f);
        enemy.enabled = true;
        healthBar.SetActive(true);

    }
}