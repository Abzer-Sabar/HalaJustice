using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanTweenAnimations : MonoBehaviour
{

    private Vector3 finalPos;
    void Start()
    {
        finalPos = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
        fadeAnimation();
    }

   private void fadeAnimation()
    {
        LeanTween.alpha(gameObject.GetComponent<RectTransform>(), 1f, 0.5f).setDelay(1f).setEase(LeanTweenType.easeInBack);
        LeanTween.move(gameObject, finalPos, 0.5f).setEase(LeanTweenType.easeOutQuad);
    }
}
