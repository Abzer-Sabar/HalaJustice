using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    
   
    public void endCredits()
    {
        FindObjectOfType<MainMenu>().endCredits();
    }

}
