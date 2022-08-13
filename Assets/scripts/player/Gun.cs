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

    [SerializeField]
    private int bullets;

    private void Start()
    {
        bulletText.text = "" + bullets;
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            shoot();
        }
    }

    private void shoot()
    {
        if(bullets > 0)
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullets--;
            bulletText.text = "" + bullets;
        }
        else
        {
            StartCoroutine(outOfBulleteffect());
            Debug.Log("out of bullets");
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
}
