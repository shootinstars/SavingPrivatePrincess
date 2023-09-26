using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour

{
    public bool fadeImg = true;
    public CanvasGroup element;

    void Update()
    {
        if (fadeImg && element.alpha < 1)
        {
            element.alpha += (Time.deltaTime / 1.75f);
        }
    }
}
