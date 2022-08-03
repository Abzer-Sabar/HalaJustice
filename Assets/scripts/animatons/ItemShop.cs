using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ItemShop : MonoBehaviour
{
    public Color canBuy, cannotBuy;
    public LeanTweenType inType;
    public LeanTweenType outType;
    public Image dateButtonUI, coffeeButtonUI, teaButtonUI, labanButtonUI;
    public GameObject itemShopUI, failedPurchaseUI, dateButton, labanButton, coffeeButton, teaButton, dateInfo, labanInfo, coffeeInfo, teaInfo;
    public TextMeshProUGUI failedPurchaseText;
    public int datePrice = 10, labanPrice = 19, coffeePrice = 15, teaPrice = 17, silverArmorPrice = 30, goldArmorPrice = 40;
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
        assignbuttonColor();
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
            closeShopElements();
            closeShop();
        }
    }

    private void openShop()
    {

        itemShopUI.SetActive(true);
        LeanTween.move(itemShopUI, finalPos, 0.3f).setEase(LeanTweenType.easeOutQuad);
        LeanTween.alpha(itemShopUI.GetComponent<RectTransform>(), 1f, 0.3f).setDelay(0f).setEase(LeanTweenType.easeInBack).setOnComplete(openShopElements);
    }

    private void closeShop()
    {
        itemShopUI.SetActive(false);
        LeanTween.move(itemShopUI, startPos, 0.3f).setEase(LeanTweenType.easeOutQuad);
        LeanTween.alpha(itemShopUI.GetComponent<RectTransform>(), 0f, 0.3f).setDelay(0f).setEase(LeanTweenType.easeInBack);
    }

    private void openShopElements()
    {
        LeanTween.scale(dateIcon, new Vector3(1f, 1f, 1f), 0.3f).setEase(inType);
        LeanTween.scale(teaIcon, new Vector3(1f, 1f, 1f), 0.3f).setEase(inType);
        LeanTween.scale(coffeeIcon, new Vector3(1f, 1f, 1f), 0.3f).setEase(inType);
        LeanTween.scale(labanIcon, new Vector3(1f, 1f, 1f), 0.3f).setEase(inType);
        LeanTween.scale(dateInfo, new Vector3(1f, 1f, 1f), 0.3f).setEase(inType);
        LeanTween.scale(coffeeInfo, new Vector3(1f, 1f, 1f), 0.3f).setEase(inType);
        LeanTween.scale(teaInfo, new Vector3(1f, 1f, 1f), 0.3f).setEase(inType);
        LeanTween.scale(labanInfo, new Vector3(1f, 1f, 1f), 0.3f).setEase(inType);
    }

    private void closeShopElements()
    {
        LeanTween.scale(dateIcon, new Vector3(0f, 0f, 0f), 0.3f).setEase(outType);
        LeanTween.scale(teaIcon, new Vector3(0f, 0f, 0f), 0.3f).setEase(outType);
        LeanTween.scale(coffeeIcon, new Vector3(0f, 0f, 0f), 0.3f).setEase(outType);
        LeanTween.scale(labanIcon, new Vector3(0f, 0f, 0f), 0.3f).setEase(outType);
        LeanTween.scale(dateInfo, new Vector3(0f, 0f, 0f), 0.3f).setEase(outType);
        LeanTween.scale(coffeeInfo, new Vector3(0f, 0f, 0f), 0.3f).setEase(outType);
        LeanTween.scale(teaInfo, new Vector3(0f, 0f, 0f), 0.3f).setEase(outType);
        LeanTween.scale(labanInfo, new Vector3(0f, 0f, 0f), 0.3f).setEase(outType);
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

   private void assignbuttonColor()
    {
        int gold = manager.goldAmount;
        if(gold >= datePrice)
        {
            dateButtonUI.color = canBuy;
        }
        else 
        {
            dateButtonUI.color = cannotBuy;
        }


        if (gold >= coffeePrice)
        {
            coffeeButtonUI.color = canBuy;
        }
        else
        {
            coffeeButtonUI.color = cannotBuy;
        }


        if (gold >= labanPrice)
        {
            labanButtonUI.color = canBuy;
        }
        else
        {
            labanButtonUI.color = cannotBuy;
        }


        if (gold >= teaPrice)
        {
            teaButtonUI.color = canBuy;
        }
        else
        {
            teaButtonUI.color = cannotBuy;
        }


    }

    //item shop buttons
    public void buyDates()
    {
        int gold = manager.goldAmount;

        if (gold >= datePrice)
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    //add item to inventory
                    FindObjectOfType<AudioManager>().play("Pickup");
                    inventory.isFull[i] = true;
                    Instantiate(dateButton, inventory.slots[i].transform, false);
                    manager.deductGold(datePrice);
                   
                   
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
                    manager.deductGold(labanPrice);
                  
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
                    manager.deductGold(coffeePrice);
                   
                    break;
                }
                
            }
        }
        else
        {

            failedPurchaseUIOpen(notEnoughGold);
        }
    }

    public void buyTea()
    {
        int gold = manager.goldAmount;
        if (gold >= teaPrice)
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    //add item to inventory
                    FindObjectOfType<AudioManager>().play("Pickup");
                    inventory.isFull[i] = true;
                    Instantiate(teaButton, inventory.slots[i].transform, false);
                    Debug.Log("added tea");
                    manager.deductGold(teaPrice);
                   
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

        assignbuttonColor();
    }


}
