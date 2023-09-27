using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector2 land = new Vector2(0, 0);
    private Vector2 screenBounds;


    public int lives = 4;
    public int score = 0;

    public AudioSource source;
    public ScoreManager scoreManager;

    public TMP_InputField inputField;
    public TMP_Text livesText;
    public TMP_Text livesUI;
    public TMP_Text scoreUI;
    public TMP_Text scoreText;
    public TMP_Text resultText;

    public GameObject bg;
    public GameObject playerClone;
    public GameObject player;
    public GameObject brick;
    public GameObject bg1;
    public GameObject bg2;
    public GameObject bg3;
    public GameObject bg4;
    public GameObject gameOver;
    public GameObject texts;
    public GameObject spawner;
    public GameObject ground;
    public GameObject princess;
    public GameObject end;
    public GameObject pauseScreen;
    public GameObject submitScoreScreen;
    public List<GameObject> groundList;
    Color newColor;

    public Slider volumeSlider;

    public bool paused = false;
    public Transform NewParent;

    void Awake()
    {
        LoadVolume();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        GameObject a = Instantiate(player) as GameObject;
        a.transform.SetParent(NewParent);
        a.transform.position = new Vector2 (brick.transform.position.x, brick.transform.position.y + brick.GetComponent<Renderer>().bounds.size.y / 2 + 0.4f);
        rb = GameObject.FindGameObjectWithTag("player").GetComponent<Rigidbody2D>();
        livesText.text = lives.ToString();
        scoreText.text = score.ToString();
        ChangeGroundColor();
    }
    

    private void Update()
    {
        Jump();
        PauseState();
    }

    public void Jump()
    {
        if (rb != null && rb.velocity == land && Input.GetKeyDown(KeyCode.Space))
        { 
            rb.velocity = new Vector2(0, 12f);
        }
    }

    public void Pause() 
    {
        pauseScreen.SetActive(true);
        AudioListener.pause = true;
        Time.timeScale = 0f;
        paused = true;
    }

    public void Resume()
    {
        pauseScreen.SetActive(false);
        AudioListener.pause = false;
        Time.timeScale = 1f;
        paused = false;
    }

    void PauseState()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver.activeSelf) {
            if (!paused) {
                Pause();
            } else {
                Resume();
            }
        }
    }

    private void ChangeGroundColor()
    {
        string bgName = PlayerPrefs.GetString("bgName");
        Debug.Log(bgName);
        if (bgName == "village" || bgName == "forest") {
            newColor = new Color(
                185.0f / 255.0f,
                139.0f / 255.0f,
                90.0f / 255.0f
                );
        } else {
            newColor = new Color(255, 255, 255);
        }
        Debug.Log(newColor);
        for (int i = 0; i < groundList.Count; i++) {
            Debug.Log(i);
            groundList[i].GetComponent<SpriteRenderer>().color = newColor;
        }
    }

    public void LoadNewGame()
    {
        SceneManager.LoadScene("GAME");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MENU");
    }

    public void StopAudio()
    {
        source.Stop();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

// functions for tutorial mode
    public void ChangeBackground(GameObject bg1,GameObject bg2)
    {
        bg1.SetActive(false);
        bg2.SetActive(true);
    }
    
    public void LoadVolume() 
    {
        float volumeValue = PlayerPrefs.GetFloat("volume");
        float volumeInfo = PlayerPrefs.GetFloat("volumeSaved");
        if (volumeInfo == 0) {
            volumeValue = 0.5f;
        }
        volumeSlider.value = volumeValue;
        AudioListener.volume = volumeValue;
    }

    public void SaveVolume() 
    { 
        float volumeValue = volumeSlider.value;
        PlayerPrefs.SetFloat("volume", volumeValue);
        PlayerPrefs.SetFloat("volumeSaved", 1);
        AudioListener.volume = volumeValue;
    }


    public void ChangeTextColor(Color color)
    {  
        livesText.color = color;
        livesUI.color = color;
        scoreText.color = color;
        scoreUI.color = color;
    }


    void DestroyWithTag (string destroyTag)
    {
        GameObject[] destroyObject;
        destroyObject = GameObject.FindGameObjectsWithTag(destroyTag);
        foreach (GameObject oneObject in destroyObject)
            Destroy (oneObject);
    }

    void SetEndState()
    {
        texts.SetActive(false);
        spawner.SetActive(false);
        ground.SetActive(false);
        gameOver.SetActive(true);
    }

    void SetFade(string name)
    {
        GameObject.Find(name).GetComponent<FadeIn>().fadeImg = true;
    }
    void FadeIn()
    {
        SetFade("GameOver");
        SetFade("GameOverSign");
        SetFade("MainMenu");
        SetFade("NewGame");
    }
    void GameOverChanges()
    {
        SetEndState();
        if (SceneManager.GetActiveScene().name == "ENDLESS") {
            ShowScoreSaving();
        }
        else {
            FadeIn();
        }
    }

    public void StartSavingScore(string name)
    {
        submitScoreScreen.SetActive(false);
        scoreManager.AddScore(name, score);
        
    }

    void HideBG()
    {
        GameObject[] hideObject;
        hideObject = GameObject.FindGameObjectsWithTag("background");
        foreach (GameObject bg in hideObject)
            bg.SetActive(false);
    }

    public void ShowScoreSaving() 
    {
        SetFade("GameOver");
        submitScoreScreen.SetActive(true);
        resultText.text = $"Congratulations, your score is {score}.";
    }


    public void Submit() 
    {
        string name = inputField.text;
        HashSet<char> alphSet = new HashSet<char> {'a', 'b', 'c', 'd', 'e', 'f', 'g',
                                                 'h', 'i', 'j', 'k', 'l', 'm', 'n',
                                                 'o', 'p', 'q', 'r', 's', 't', 'u',
                                                 'v', 'w', 'x', 'y', 'z'};
        for (int i = 0; i < name.Length; i++) {
            if (!alphSet.Contains(char.ToLower(name[i]))) {
                return;
            }
        }
        StartSavingScore(name);
        FadeIn();
    }


    public void GameOver()
    {
        StopAudio();
        HideBG();
        GameOverChanges();
        playerClone = GameObject.FindGameObjectWithTag("player");
        Destroy(playerClone);
        DestroyWithTag("rock");
        DestroyWithTag("ring");
    }

    public void LastRun()
    {
        GameObject.Find("BgCastle").GetComponent<Scroller>().x = 0;
        rb.gravityScale = 3;
        rb.velocity = new Vector2(12f, 0);
        rb.freezeRotation = false;
        spawner.SetActive(false);
        DestroyWithTag("rock");
        DestroyWithTag("ring");
        princess.SetActive(true);
    }

    public void EndGame()
    {
        Destroy(princess);
        texts.SetActive(false);
        ground.SetActive(false);
        end.SetActive(true);
        bg4.SetActive(false);
        bg.GetComponent<ScenesFader>().startTimer = true;
    }
}
