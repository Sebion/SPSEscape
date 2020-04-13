using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Firebase.Database;
using Newtonsoft.Json.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ScoreboardMenu : MonoBehaviour
    {
        public TMP_Text[] scoreboard;
        private FirebaseSetup _firebaseSetup;
        private List<User> players;
        public GameObject scoreBoardObject;
        public GameObject loader;
        private bool isReady;


        public void StartScoreboard()
        {
            players = new List<User>();
            _firebaseSetup = new FirebaseSetup();
            loader.SetActive(true);
            isReady = false;
            Debug.Log("start v scoreboarde");
            scoreBoardObject.SetActive(false);
            StartCoroutine(GetAllPlayersFromDatabase());
        }

        public void spin()
        {
            loader.transform.Rotate(0, 0, 60 * Time.deltaTime * 4);
        }

        public void Update()
        {
            spin();
        }

//        private IEnumerator Loading()
//        {
//            yield return new WaitWhile();
//            
//        }

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
                            var highScorePair = JObject.Parse(pair.Value.ToString());
                            foreach (var pairH in highScorePair)
                            {
                                int x = Int32.Parse(pairH.Value.ToString());
                                players.Add(new User(pair.Key, x));
                            }
                        }

                        isReady = true;
                        Debug.Log("nacitane");
                    }
                });
            Debug.Log("spustam zoradovanie");
            StartCoroutine(GetTop10());
        }

        public bool isReadyFunc()
        {
            return isReady;
        }

        private IEnumerator GetTop10()
        {
            yield return new WaitUntil(isReadyFunc);
            var orderedByScore = players.OrderByDescending(player => player.getScore()).ToList();
            int i = 1;
            foreach (var player in orderedByScore)
            {
                if (i < 11)
                {
                    Debug.Log("zoradujem");
                    Debug.Log(player.ToString());
                    if (PlayerPrefs.GetString("Username").Equals(player.getUsername()))
                    {
                        scoreboard[i-1].color=new Color32(254, 205, 0, 255);
                    }
                    else
                    {
                        scoreboard[i-1].color=new Color32(255, 255, 255, 255);
                    }

                    scoreboard[i - 1].text = i + ". " + player.getUsername() + " - " + player.getScore();
                    i++;
                }
                else break;
            }

            loader.SetActive(false);
            scoreBoardObject.SetActive(true);
        }
    }
}