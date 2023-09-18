using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{

    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] Slider volumeSlider;
    AudioSource menuTheme;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;


    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject backButton;
    public GameObject endlessMode;
    public TMP_Text endlessModeText;
    public GameObject mapChooser;
    public GameObject confirmExit;
    public MenuScript menu;
    public GameObject tutorialInfo;
    public GameObject leaderboard;
    public GameObject leaderboardBack;

    private double currentRefreshRate;
    private int currentResolutionIndex = 0;

    void Start()
    {
        GetTutorialInfo();
        menuTheme = GameObject.Find("MainTheme").GetComponent<AudioSource>();
        LoadVolume();
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();
        currentRefreshRate = Screen.currentResolution.refreshRateRatio.value;

        for (int i = 0; i < resolutions.Length; i++) {
            if (resolutions[i].refreshRateRatio.value == currentRefreshRate && 
                    CheckResolution(resolutions[i].width, resolutions[i].height)) {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        List<string> options = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; i++) {
            string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height + " " + Mathf.Round((float)filteredResolutions[i].refreshRateRatio.value) + "Hz";
            options.Add(resolutionOption);
            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height) {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (mainMenu.activeSelf) {
                menu.ShowConfirm();
            } else if (leaderboard.activeSelf) {
                HideLeaderboard();
            }
            else {
                menu.HideSettings();
                menu.HidemapChooser();
                menu.HideConfirm();
            }
        }
    }

    bool CheckResolution(int width, int height) 
    {
        var allowedResolutions = new HashSet<(int width, int height)>() {(1280, 720), (1280, 768),
                                                                (1360, 768), (1366, 768),
                                                                (1600, 900), (1920, 1080)};
        return allowedResolutions.Contains((width, height));
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }

    void SwitchUI(GameObject ui_1, GameObject ui_2)
    {
        ui_1.SetActive(false);
        ui_2.SetActive(true);
    }

    public void ShowSettings()
    {
        SwitchUI(mainMenu, settingsMenu);
        backButton.SetActive(true);
    }

    public void ShowConfirm()
    {
        SwitchUI(mainMenu, confirmExit);
    }

    public void HideConfirm()
    {
        SwitchUI(confirmExit, mainMenu);
    }

    public void HideSettings()
    {
        SwitchUI(settingsMenu, mainMenu);
        backButton.SetActive(false);
    }

    public void ShowmapChooser() 
    {
        SwitchUI(mainMenu, mapChooser);
        backButton.SetActive(true);
    }

    public void HidemapChooser() 
    {
        SwitchUI(mapChooser, mainMenu);
        backButton.SetActive(false);
    }


    public void LoadNewGame()
    {
        menuTheme.Stop();
        SceneManager.LoadScene("GAME");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MENU");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadtutorialInfo() 
    {
        tutorialInfo.SetActive(true);
    }

    public void LoadLeaderboard() 
    {
        SwitchUI(mainMenu, leaderboard);
        leaderboardBack.SetActive(true);
    }

    public void HideLeaderboard()
    {
        SwitchUI(leaderboard, mainMenu);
        leaderboardBack.SetActive(false);
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

    void GetTutorialInfo() 
    {
        int TutorialComplete = PlayerPrefs.GetInt("TutorialComplete");
        if (TutorialComplete == 1) {
            endlessMode.GetComponent<Button>().interactable = true;
            endlessModeText.GetComponent<TMP_Text>().color = Color.black;
            var image = endlessMode.GetComponent<Image>();
            image.color =  new Color(255,255,255,255);
        }
    }
}
