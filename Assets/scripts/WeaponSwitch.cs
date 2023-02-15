using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponSwitch : MonoBehaviour
{
    public combat sword;
    public Gun gun;

    public GameObject textObj;
    public TextMeshProUGUI switchText;
    public KeyCode switchKey;

    public bool isSwordEquipped = false;
    private void Start()
    {
        sword.enabled = false;
        gun.enabled = true;
        textObj.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            isSwordEquipped = !isSwordEquipped;

            if (isSwordEquipped)
            {
                sword.enabled = true;
                gun.enabled = false;
                StartCoroutine(enableSwitchText("Weapon Equipped: Sword"));
            }
            else
            {
                sword.enabled = false;
                gun.enabled = true;
                StartCoroutine(enableSwitchText("Weapon Equipped: Gun"));
            }
        }

    }


    IEnumerator enableSwitchText(string message)
    {
        textObj.SetActive(true);
        switchText.text = "" + message;
        yield return new WaitForSeconds(2f);
        textObj.SetActive(false);
    }

}
