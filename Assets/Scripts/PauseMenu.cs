using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused;
    public GameObject pauseMenu;
    public PlayerEndless PlayerEndless;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

   public  void Pause()
    {
        PlayerEndless.runningSound.Stop();
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

    }

   public void Quit()
   {
       Application.Quit();
   }

   public void MainMenu()
   {
       Time.timeScale = 1f;
       SceneManager.LoadScene(0);
   }

}
