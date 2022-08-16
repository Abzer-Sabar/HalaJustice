using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenade : MonoBehaviour
{
    public float explodeTime;
    public float explodeRadius, explodeDamage;
    public GameObject explodeChunks;
    public LayerMask playerLayer;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(explodeEffect());
    }
    

    IEnumerator explodeEffect()
    {
        yield return new WaitForSeconds(explodeTime);
        explode();
    }

    private void explode()
    { 
        deathEffect();
        Collider2D hit = Physics2D.OverlapCircle(transform.position, explodeRadius, playerLayer);
        if(hit != null)
        {
            hit.transform.SendMessage("takeDamage", explodeDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, explodeRadius);
    }

    private void deathEffect()
    {
        anim.SetTrigger("explode");
        FindObjectOfType<AudioManager>().play("Grenade Explode");
        Instantiate(explodeChunks, transform.position, Quaternion.identity);
        StartCoroutine(kill());
    }

    IEnumerator kill()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}

