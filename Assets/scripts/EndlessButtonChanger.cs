using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndlessButtonChanger : MonoBehaviour
{
    Image image;
    public TMP_Text endlessModeText;
    Button endlessButton;
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        endlessButton = GameObject.Find("EndlessModeButton").GetComponent<Button>();
    }

    void Update()
    {
       if (image.color.a <= 0.425f) {
            GetTutorialInfo();
        }
        if (image.color.a == 0) {
            Destroy(gameObject);
        }
    }
    void GetTutorialInfo() 
    {
        int TutorialComplete = PlayerPrefs.GetInt("TutorialComplete");
        if (TutorialComplete == 0) {
            endlessModeText.color = Color.grey;
            endlessButton.interactable = false;
        }
    }
}
