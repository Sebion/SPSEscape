﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //score
    public TextMeshProUGUI scoreText;
    public GameObject pauseButton;
    public GameObject deathMenu;
    public TextMeshProUGUI highScoreText;
    public Image healthBar;
    public Sprite[] hearts;
    public float score;
    private float highScore = 0;
    public float scorePerSecond;
    private FirebaseSetup _firebaseSetup;


    // Start is called before the first frame update
    void Start()
    {
        _firebaseSetup= new FirebaseSetup();
        highScore = PlayerPrefs.GetFloat("HighScore");
    }

    // Update is called once per frame
    void Update()
    {
        score += scorePerSecond * Time.deltaTime;
        scoreText.text = "Score: " + Mathf.Round(score);
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void OpenDeathMenu()
    {
        Time.timeScale = 0f;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
            _firebaseSetup.WriteUserToDatabase(PlayerPrefs.GetString("Username"),(int)highScore);
        }


        pauseButton.SetActive(false);
        deathMenu.SetActive(true);
        highScoreText.text = "High Score: " + Mathf.Round(highScore);
        
    }

    public void AddScore(int bonusScore)
    {
        score += bonusScore;
    }

    public void changeHealth(int health)
    {
        healthBar.sprite = hearts[health];
    }
}