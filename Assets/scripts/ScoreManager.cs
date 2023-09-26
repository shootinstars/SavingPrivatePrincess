using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

[Serializable]
public class ScoreData
{
    public List<Score> scores;

    public ScoreData()
    {
        scores = new List<Score>();
    }
}


[Serializable]
public class Score
{
    public float id;
    public string name;
    public float score;

    public Score(string name, int score, float id)
    {
        this.id = id;
        this.name = name;
        this.score = score;
    }
}

public class ScoreManager : MonoBehaviour
{
    private ScoreData sd;
    public TMP_Text resultText;
    GameObject positionInfo;
    void Awake()
    {
        var json = PlayerPrefs.GetString("scores", "{}");
        sd = JsonUtility.FromJson<ScoreData>(json);
    }

    public IEnumerable<Score> GetHighScores()
    {
        return sd.scores.OrderByDescending(x => x.score);
    }

    public void AddScore(string name, int score)
    {
        positionInfo = GameObject.Find("PositionInfo");
        float id = PlayerPrefs.GetFloat("id");
        Score newScore = new Score(name, score, id);
        sd.scores.Add(newScore);
        PlayerPrefs.SetFloat("id", id + 1);
        var scores = GetHighScores().ToArray();
        for (int i = 0; i < scores.Length; i++) {
            Debug.Log(id);
            if (scores[i].id == newScore.id) {
                string suffix;
                switch((i + 1) % 10) 
                {
                    case 1:
                        suffix = "st";
                        break;
                    case 2:
                        suffix = "nd";
                        break;
                    case 3:
                        suffix = "rd";
                        break;
                    default:
                        suffix = "th";
                        break;
                }
                resultText.text = $"Wow, that was  good enough for {i + 1}{suffix}";
                positionInfo.GetComponent<FadeIn>().fadeImg = true;
                return;
            }

        }
    }
    private void OnDestroy()
    {
        SaveScore();
    }

    public void SaveScore()
    {
        var json = JsonUtility.ToJson(sd);
        PlayerPrefs.SetString("scores", json);
    }
}























