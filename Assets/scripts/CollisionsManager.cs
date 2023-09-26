using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CollisionsManager : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector2 land = new Vector2(0, 0);
    GameManager gameManager;
    int lives;
    int score;
    TMP_Text livesText;
    TMP_Text livesUI;
    TMP_Text scoreUI;
    TMP_Text scoreText;
    GameObject village;
    GameObject forest;
    GameObject cave;
    GameObject castle;
    int sceneIndex;


    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        lives = gameManager.lives;
        score = gameManager.score;
        livesText = gameManager.livesText;
        livesUI = gameManager.livesUI;
        scoreText = gameManager.scoreText;
        scoreUI = gameManager.scoreUI;
        village = gameManager.bg1;
        forest = gameManager.bg2;
        cave = gameManager.bg3;
        castle = gameManager.bg4;
        
        if (sceneIndex == 2) {
            string bgName = PlayerPrefs.GetString("bgName");
            switch (bgName) {
                case "village":
                village.SetActive(true);
                break;

                case "forest":
                forest.SetActive(true);
                gameManager.ChangeTextColor(Color.white);
                break;

                case "cave":
                cave.SetActive(true);
                gameManager.ChangeTextColor(Color.white);
                break;

                case "castle":
                castle.SetActive(true);
                break;
            }
        }
    }

    public void Jump()
    {
        if (rb != null && rb.velocity == land && Input.GetKeyDown(KeyCode.Space))
        { 
            rb.velocity = new Vector2(0, 12f);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Jump();
        if (collision.gameObject.tag == "rock") {
                Destroy(collision.gameObject);
                lives -= 1;
                livesText.text = lives.ToString();
                if (lives == 0){
                    gameManager.score = score;
                    gameManager.GameOver();
                }
        } else if (collision.gameObject.tag == "ring") {
            Destroy(collision.gameObject);
            score += 1;
            scoreText.text = score.ToString();
            if (sceneIndex == 1 && score == 5) {
                gameManager.ChangeBackground(village,forest);
                gameManager.ChangeTextColor(Color.white);
            } else if (sceneIndex == 1 && score == 10) {
                gameManager.ChangeBackground(forest,cave);
            } else if (sceneIndex == 1 && score == 15) {
                gameManager.ChangeBackground(cave,castle);
                gameManager.ChangeTextColor(Color.black);
            } else if(sceneIndex == 1 && score == 20) {
                gameManager.LastRun();
            }
        } else if(collision.gameObject.tag == "princess") {
            gameManager.EndGame();
            Destroy(gameObject);
        }
    }
}
