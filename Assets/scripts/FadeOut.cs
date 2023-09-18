using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public bool fade_out = false;
    public bool timer_running = false;
    public GameObject Credits;
    float timer = 2f;
    public CanvasGroup element;

    // Update is called once per frame
    void Update()
    {
        if (element.alpha >= 1) {
            timer_running = true;
        }
        if (timer_running) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                timer_running = false;
                fade_out = true;
                if (Credits != null) {
                    Credits.SetActive(true);
                }
            }
        }
        FadingOut();
    }

    void FadingOut() 
    {
        if (fade_out && element.alpha > 0)
        {
            element.alpha -= Time.deltaTime;
        }
    }
}
