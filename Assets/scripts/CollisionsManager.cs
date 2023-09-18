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
    GameManager game_manager;
    int lives;
    int score;
    TMP_Text livesText;
    TMP_Text livesUI;
    TMP_Text scoreUI;
    TMP_Text scoreText;
    GameObject village;
    GameObject forrest;
    GameObject cave;
    GameObject castle;
    int sceneIndex;


    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        game_manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        lives = game_manager.lives;
        score = game_manager.score;
        livesText = game_manager.livesText;
        livesUI = game_manager.livesUI;
        scoreText = game_manager.scoreText;
        scoreUI = game_manager.scoreUI;
        village = game_manager.bg1;
        forrest = game_manager.bg2;
        cave = game_manager.bg3;
        castle = game_manager.bg4;
        
        if (sceneIndex == 2) {
            string bgName = PlayerPrefs.GetString("bgName");
            switch (bgName) {
                case "village":
                village.SetActive(true);
                break;

                case "forrest":
                forrest.SetActive(true);
                game_manager.ChangeTextColor(livesText, livesUI, scoreText, scoreUI, Color.white);
                break;

                case "cave":
                cave.SetActive(true);
                game_manager.ChangeTextColor(livesText, livesUI, scoreText, scoreUI, Color.white);
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
                    game_manager.score = score;
                    game_manager.GameOver();
                }
        } else if (collision.gameObject.tag == "ring") {
            Destroy(collision.gameObject);
            score += 1;
            scoreText.text = score.ToString();
            if (sceneIndex == 1 && score == 5) {
                game_manager.ChangeBackground(village,forrest);
                game_manager.ChangeTextColor(livesText, livesUI, scoreText, scoreUI, Color.white);
            } else if (sceneIndex == 1 && score == 10) {
                game_manager.ChangeBackground(forrest,cave);
            } else if (sceneIndex == 1 && score == 15) {
                game_manager.ChangeBackground(cave,castle);
                game_manager.ChangeTextColor(livesText, livesUI, scoreText, scoreUI, Color.black);
            } else if(sceneIndex == 1 && score == 20) {
                game_manager.LastRun();
            }
        } else if(collision.gameObject.tag == "princess") {
            game_manager.EndGame();
            Destroy(gameObject);
        }
    }
}
