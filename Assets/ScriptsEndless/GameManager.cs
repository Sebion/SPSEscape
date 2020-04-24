using System;
using System.Collections;
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
    public int health;


    // Start is called before the first frame update
    void Start()
    {
        _firebaseSetup = new FirebaseSetup();
        highScore = PlayerPrefs.GetFloat("HighScore");
        health = 3;
        Debug.Log("gm"+health);
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
            PlayerPrefs.SetFloat("HighScore", (int) highScore);
            _firebaseSetup.WriteUserToDatabase(PlayerPrefs.GetString("Username"), (int) highScore);
        }


        pauseButton.SetActive(false);
        deathMenu.SetActive(true);
        highScoreText.text = "High Score: " + (int) highScore;
    }

    public void AddScore(int bonusScore)
    {
        score += bonusScore;
    }

    public void takeHealth()
    {
        
        if (health > 0)
        {
            health--;
            healthBar.sprite = hearts[health];
        }
       
        
    }

    public void giveHealth()
    {
        if (health < 3)
        {
            health++;
            healthBar.sprite = hearts[health];
        }
        
    }
}