using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public bool fadeOut = false;
    public bool timerRunning = false;
    public GameObject Credits;
    float timer = 2f;
    public CanvasGroup element;

    // Update is called once per frame
    void Update()
    {
        if (element.alpha >= 1) {
            timerRunning = true;
        }
        if (timerRunning) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                timerRunning = false;
                fadeOut = true;
                if (Credits != null) {
                    Credits.SetActive(true);
                }
            }
        }
        FadingOut();
    }

    void FadingOut() 
    {
        if (fadeOut && element.alpha > 0)
        {
            element.alpha -= Time.deltaTime;
        }
    }
}
