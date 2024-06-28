using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed;
    [SerializeField]
    public bool scrollDown;

    private float singleTexturedHeight;

    void Start()
    {
        SetupTexture();
        if (scrollDown)
        {
            moveSpeed = -moveSpeed;
        }
    }

    void SetupTexture()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        singleTexturedHeight = sprite.texture.height / sprite.pixelsPerUnit;
    }

    void Scroll()
    {
        float delta = moveSpeed * Time.deltaTime;
        transform.position += new Vector3(0f, delta, 0f);
    }

    void CheckReset()
    {
        if ((Mathf.Abs(transform.position.y) - singleTexturedHeight) > 0)
        {
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        }
    }

    void Update()
    {
        Scroll();
        CheckReset();
    }
}
