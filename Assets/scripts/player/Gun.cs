using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gun : MonoBehaviour
{
    public TextMeshProUGUI bulletText;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Image bulletIcon;
    public float bulletForce = 20f; 
    public int bulletValueOnPickup = 20;

    [SerializeField]
    private int bullets;
    private float bulletDamage = 10;

    private bool paused;
    private void Start()
    {
        bullets = 250;
        bulletText.text = "" + bullets;
    }
    public void Update()
    {
        if (Time.timeScale == 1f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                shoot();
            }
        }
    }

    private void shoot()
    {
        if(bullets > 0)
        {
            FindObjectOfType<AudioManager>().play("shoot");
           GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().bulletDamage = this.bulletDamage;
            bullets--;
            bulletText.text = "" + bullets;
        }
        else
        {
            StartCoroutine(outOfBulleteffect());
            Debug.Log("out of bullets");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            FindObjectOfType<AudioManager>().play("Pickup");
            addBullets(bulletValueOnPickup);
            Destroy(collision.gameObject);
        }
    }

    IEnumerator outOfBulleteffect()
    {
        bulletIcon.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        bulletIcon.color = Color.white;
    }

    public void addBullets(int amount)
    {
        bullets += amount;
        bulletText.text = "" + bullets;
    }

    public void increaseBulletDamage(float damage)
    {
        this.bulletDamage += damage;
    }
}
