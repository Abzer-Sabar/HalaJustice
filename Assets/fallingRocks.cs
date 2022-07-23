using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingRocks : MonoBehaviour
{
    public GameObject fallingRock;
    public GameManager gameManager;
    public Transform spawnPoint;

    public float rockSpawnTime = 1f;

    private Vector2 screenBounds;

    private void Update()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(rockWave());
        }
    }

    private void spawnRocks()
    {
        GameObject rock = Instantiate(fallingRock) as GameObject;
        //rock.transform.position = new Vector2(Random.Range(new Vector2( screenBounds.y * 2);
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
