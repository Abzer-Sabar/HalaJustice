using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingRocks : MonoBehaviour
{
    public GameObject[] fallingRock;
    public GameManager gameManager;
    public camerShake camera;

    public float rockSpawnTime = 1f;

    private Vector2 screenBounds;
    private bool playerInRange = false;


  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            StartCoroutine(rockWave());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void spawnRocks()
    {
        if (playerInRange)
        {
            // screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
            Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.Range(0f, 2.0f), 1.1f, 0));
            pos.z = 0.0f;
            var prefab = fallingRock[Random.Range(0, fallingRock.Length)];
            Instantiate(prefab, pos, Quaternion.identity);
           
        }else
        {
            return;
        }
       
    }

    IEnumerator rockWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(rockSpawnTime);
            spawnRocks();
        }
    }
}
