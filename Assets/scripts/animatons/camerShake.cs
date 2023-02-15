using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerShake : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Shake(100f, 0.54f));
    }
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 orignalPosition = transform.position;
        float elapsed = 0f;
        Debug.Log("camera is shaking");
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(x, y, -10f);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        transform.position = orignalPosition;
    }

}
