using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] public RawImage img;
    [SerializeField] public float x;
    private float y;

    void Awake()
    {
        x = 0.32f;
        y = 0f;
    }

    void Update()
    {
        img.uvRect = new Rect(img.uvRect.position + new Vector2(x,y) * Time.deltaTime, img.uvRect.size);
    }
}
