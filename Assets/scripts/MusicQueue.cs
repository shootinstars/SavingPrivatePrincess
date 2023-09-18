using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random=UnityEngine.Random;

public class MusicQueue : MonoBehaviour
{
    public AudioSource[] audioSourceArray;
    public AudioClip[] audioClipArray;
    public int nextClip;
    int toggle;
    double nextStartTime;

    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "ENDLESS") {
            ShuffleArray(audioClipArray);
        }
        nextStartTime = AudioSettings.dspTime;
    }

    // Update is called once per frame
    void Update()
    {
      if(AudioSettings.dspTime > nextStartTime - 1) {
        AudioClip clipToPlay = audioClipArray[nextClip];
    // Loads the next Clip to play and schedules when it will start
        toggle = 1 - toggle;
        audioSourceArray[toggle].clip = clipToPlay;
        audioSourceArray[toggle].PlayScheduled(nextStartTime);
    // Checks how long the Clip will last and updates the Next Start Time with a new value
        double duration = (double)clipToPlay.samples / clipToPlay.frequency;
        nextStartTime = nextStartTime + duration;
    // Switches the toggle to use the other Audio Source next
        toggle = 1 - toggle;
    // Increase the clip index number, reset if it runs out of clips
        nextClip = nextClip < audioClipArray.Length - 1 ? nextClip + 1 : 0;
        } 
    }

    private void ShuffleArray(AudioClip[] array)
    {
    int n = array.Length;
    while (n > 1) {
        n--;
        int k = Random.Range(0, n + 1);
        AudioClip temp = array[k];
        array[k] = array[n];
        array[n] = temp;
        }
    }
}
