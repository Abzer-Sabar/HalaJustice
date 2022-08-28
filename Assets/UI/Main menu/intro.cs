using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class intro : MonoBehaviour
{
    public float waitTime = 10f;

    private void Start()
    {
        StartCoroutine(changeScene());
    }

    IEnumerator  changeScene()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Main Menu");
    }
}
