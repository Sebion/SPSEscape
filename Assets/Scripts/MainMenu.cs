using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider MusicSlider;
    public Slider FxSlider;


    private void Start()
    {
        SetVolume();
    }

   

    public void PlayEndlessGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void Quit()
    {
        Application.Quit();
    }

    public void SetVolume()
    {
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        FxSlider.value = PlayerPrefs.GetFloat("FxVolume");
    }
}