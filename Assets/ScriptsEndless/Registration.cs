using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using ScriptsEndless;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;

public class Registration : MonoBehaviour
{
    // Start is called before the first frame update


    public TMP_Text warningText;
    public TMP_InputField usernameInput;
    private FirebaseSetup _firebaseSetup;
    private List<string> players;


    void Start()
    {
        if (PlayerPrefs.GetString("Username")!=(""))
        {
            GoToMainMenuScene();
        }
        else
        {
            players = new List<string>();
            _firebaseSetup = new FirebaseSetup();
            StartCoroutine(GetAllPlayersFromDatabase());
        }
    }

    public void OnRegister()
    {
        if (usernameInput.text.Equals(""))
        {
            warningText.SetText("You must have a name!");
        }
        else if (isTaken())
        {
            warningText.SetText("Damn! Someone already has that name.");
        }
        else
        {
            _firebaseSetup.WriteUserToDatabase(usernameInput.text, 0);
            PlayerPrefs.SetString("Username", usernameInput.text);
            GoToMainMenuScene();
        }
    }

    private bool isTaken()
    {
        foreach (var username in players)
        {
            if (username == usernameInput.text)
            {
                Debug.Log("uz take je meno");
                return true;
            }
        }

        return false;
    }

    public void GoToMainMenuScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Update is called once per frame
    private IEnumerator GetAllPlayersFromDatabase()
    {
        yield return new WaitForSeconds(0.5f);
        _firebaseSetup.databaseReference.Child("Players")
            .GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    string json = snapshot.GetRawJsonValue();
                    JObject jObject = JObject.Parse(json);


                    foreach (var pair in jObject)
                    {
                        players.Add(pair.Key);
                        Debug.Log(pair.Key);
                    }
                }
            });
    }
}