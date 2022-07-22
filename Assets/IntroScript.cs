using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScript : MonoBehaviour
{
    private GameObject introDialogueBox;

    private void Start()
    {
        introDialogueBox = this.gameObject;
        LeanTween.scale(introDialogueBox, new Vector3(1f, 1f, 1f), 1f).setDelay(0.5f).setEase(LeanTweenType.easeOutElastic);

    }

    public void onClose()
    {
        LeanTween.scale(introDialogueBox, new Vector3(0f, 0f, 0f), 1f).setDelay(0.5f).setEase(LeanTweenType.easeOutElastic);
    }
}
