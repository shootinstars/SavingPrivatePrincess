using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] public RawImage _img;
    [SerializeField] public float x;
    private float y;

    void Awake()
    {
        x = 0.3f;
        y = 0f;
    }

    void Update()
    {
        _img.uvRect = new Rect(_img.uvRect.position + new Vector2(x,y) * Time.deltaTime, _img.uvRect.size);
    }
}
