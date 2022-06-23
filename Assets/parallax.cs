using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    public Vector2 parallaxEffect;

    public Transform camera;
    private Vector3 lastCamPosition;
    private float textureUnitSizeX;

    private void Start()
    {
        camera = Camera.main.transform;
        lastCamPosition = camera.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = (texture.width / sprite.pixelsPerUnit) * transform.localScale.x;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = camera.position - lastCamPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffect.x, deltaMovement.y * parallaxEffect.y);
        lastCamPosition = camera.position;

        if(Mathf.Abs(camera.position.x - transform.position.x) >= textureUnitSizeX)
        {
            float offsetPositionX = (camera.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(camera.position.x + offsetPositionX, transform.position.y);
        }
    }
}
