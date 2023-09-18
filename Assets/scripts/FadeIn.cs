using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour

{
    public bool fade_img = true;
    public CanvasGroup element;

    void Update()
    {
        if (fade_img && element.alpha < 1)
        {
            element.alpha += (Time.deltaTime / 1.75f);
        }
    }
}
