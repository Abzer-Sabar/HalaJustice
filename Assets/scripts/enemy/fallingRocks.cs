using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingRocks : MonoBehaviour
{
    public GameObject[] fallingRock;
    public GameManager gameManager;

    public float rockSpawnTime = 1f;

    private Vector2 screenBounds;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(rockWave());
        }
    }

    private void spawnRocks()
    {
        // screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.Range(0f, 2.0f), 1.1f, 0));
        pos.z = 0.0f;
        var prefab = fallingRock[Random.Range(0, fallingRock.Length)]; 
        Instantiate(prefab, pos, Quaternion.identity);
        
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
