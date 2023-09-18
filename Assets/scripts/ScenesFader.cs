using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesFader : MonoBehaviour
{

    public Animator animator;
    public bool fading;
    public bool start_timer;
    float timer = 25f;
    int indexToLoad;
    // Update is called once per frame
    void Update()
    {
        if (start_timer && fading && timer > 0) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
            FadeToLevel(0);
            }
        }
    }

    void FadeToLevel(int levelIndex) 
    { 
        indexToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    void OnFadeComplete()
    {
        PlayerPrefs.SetInt("TutorialComplete", 1);
        SceneManager.LoadScene(indexToLoad);
    }
}
