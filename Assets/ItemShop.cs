using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShop : MonoBehaviour
{
    public GameObject itemShopUI;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            openShop();
        }
    }

    private void openShop()
    {
        LeanTween.scale(itemShopUI, new Vector3(1f, 1f, 1f), 0.3f).setEase(LeanTweenType.easeInElastic);
    }
}
