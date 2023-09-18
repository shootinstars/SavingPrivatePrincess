using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndlessBackground : MonoBehaviour
{

    public void LoadEndless()
    {
        string name = gameObject.name;
        PlayerPrefs.SetString("bgName", name);
        SceneManager.LoadScene("ENDLESS");
    }
    
    public void LoadNewEndless() 
    {
        SceneManager.LoadScene("ENDLESS");
    }
}
