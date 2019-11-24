using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform platformGenerator;
    private Vector3 platformStartPositon;

    public PlayerEndless thePlayer;

    private Vector3 thePlayerStartPosition;

    // Start is called before the first frame update
    void Start()
    {
        platformStartPositon = platformGenerator.position;
        thePlayerStartPosition = thePlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void RestartGame()
    {
        Debug.Log("restartin game");
        
        //adding a delay
        Invoke("RestartGameInvoke",1f);
       
    }

    public void RestartGameInvoke()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
}