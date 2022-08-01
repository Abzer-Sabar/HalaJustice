using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopButton : MonoBehaviour
{
    public int itemPrice;
    public Color canBuy, cannotBuy;
    public GameManager manager;

    private Image image;
    private void Start()
    {
        image = GetComponent<Image>();
        changeColor();
    }
    public void changeColor()
    {
        if(manager.goldAmount >= itemPrice)
        {
            image.color = Color.green;
        }
        else
        {
            image.color = new Color(255, 107, 129);
        }
    }
}
