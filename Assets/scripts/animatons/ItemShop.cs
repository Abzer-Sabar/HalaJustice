using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ItemShop : MonoBehaviour
{
    public LeanTweenType inType;
    public LeanTweenType outType;
    public GameObject itemShopUI, failedPurchaseUI, dateButton, labanButton, coffeeButton;
    public TextMeshProUGUI failedPurchaseText;
    public int datePrice = 10, labanPrice = 15, coffeePrice = 15, silverArmorPrice = 30, goldArmorPrice = 40;
    public string notEnoughGold, inventoryFull;

    private playerInventory inventory;
    [SerializeField]
    private GameManager manager;
    private Vector3 finalPos, startPos;

    //item shop ui elements
    public GameObject dateIcon, labanIcon, coffeeIcon, teaIcon, silverArmorIcon, goldArmorIcon;
    private void Start()
    {
        finalPos = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
        startPos = new Vector3(Screen.width * 0.5f, 6f, 0);
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<playerInventory>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            openShop();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            closeShop();
        }
    }

    private void openShop()
    {

        // LeanTween.scale(itemShopUI, new Vector3(1f, 1f, 1f), 0.3f).setEase(inType);
        LeanTween.move(itemShopUI, finalPos, 0.3f).setEase(LeanTweenType.easeOutQuad);
        LeanTween.alpha(itemShopUI.GetComponent<RectTransform>(), 1f, 0.3f).setDelay(0f).setEase(LeanTweenType.easeInBack).setOnComplete(openShopElements);
    }

    private void closeShop()
    {
        //LeanTween.scale(itemShopUI, new Vector3(0f, 0f, 0f), 0.5f).setDelay(0.3f).setEase(outType);
        LeanTween.move(itemShopUI, startPos, 0.3f).setEase(LeanTweenType.easeOutQuad);
        LeanTween.alpha(itemShopUI.GetComponent<RectTransform>(), 0f, 0.3f).setDelay(0f).setEase(LeanTweenType.easeInBack);
    }

    private void openShopElements()
    {
        LeanTween.scale(dateIcon, new Vector3(1f, 1f, 1f), 0.3f).setEase(inType);
    }

    private void failedPurchaseUIOpen(string text)
    {
        failedPurchaseText.text = "";
        failedPurchaseText.text = text;
        LeanTween.scale(failedPurchaseUI, new Vector3(1f, 1f, 1f), 0.3f).setEase(inType).setOnComplete(failedPurchaseClose);
    }

    private void failedPurchaseClose()
    {
        StartCoroutine(delay());
         LeanTween.scale(failedPurchaseUI, new Vector3(0f, 0f, 0f), 0.3f).setDelay(1f).setEase(outType);
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(2f);
    }

    //item shop buttons
    public void buyDates()
    {
        int gold = manager.goldAmount;
        
        if(gold >= datePrice)
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    //add item to inventory
                    FindObjectOfType<AudioManager>().play("Pickup");
                    inventory.isFull[i] = true;
                    Instantiate(dateButton, inventory.slots[i].transform, false);
                    break;
                }
            }
        }
        else
        {
            failedPurchaseUIOpen(notEnoughGold); 
        }
    }

    public void buyLabanUp()
    {
        int gold = manager.goldAmount;
        if (gold >= labanPrice)
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    //add item to inventory
                    FindObjectOfType<AudioManager>().play("Pickup");
                    inventory.isFull[i] = true;
                    Instantiate(labanButton, inventory.slots[i].transform, false);
                    break;
                }
            }
        }
        else
        {

            failedPurchaseUIOpen(notEnoughGold);
        }
    }


    public void buyCoffee()
    {
        int gold = manager.goldAmount;
        if (gold >= coffeePrice)
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    //add item to inventory
                    FindObjectOfType<AudioManager>().play("Pickup");
                    inventory.isFull[i] = true;
                    Instantiate(coffeeButton, inventory.slots[i].transform, false);
                    break;
                }
            }
        }
        else
        {

            failedPurchaseUIOpen(notEnoughGold);
        }
    }

    public void silverArmorUpgrade()
    {
        int gold = manager.goldAmount;

        if (gold >= silverArmorPrice)
        {
            //upgrade the armor

        }
        else
        {

            failedPurchaseUIOpen(notEnoughGold);
        }
    }


}
