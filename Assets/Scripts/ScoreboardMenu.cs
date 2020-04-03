using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Firebase.Database;
using Newtonsoft.Json.Linq;
using UnityEngine;
using TMPro;

namespace DefaultNamespace
{
    public class ScoreboardMenu : MonoBehaviour
    {
        public TMP_Text[] scoreboard;
        private FirebaseSetup _firebaseSetup;
        private List<User> players;
        

        private void Start()
        {
            players = new List<User>();
            _firebaseSetup = new FirebaseSetup();
            StartCoroutine(GetAllPlayersFromDatabase());
        }


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
                    }
                });
            yield return new WaitForSeconds(2f);
            var orderedByScore = players.OrderByDescending(player => player.getScore()).ToList();
            int i = 1;
            foreach (var player in orderedByScore)
            {
                if (i<11)
                {
                    
                    Debug.Log(player.ToString());
                    scoreboard[i-1].text = i+". "+ player.getUsername() +" - "+player.getScore();
                    i++;
                }else break;
                
            }
        }
    }
}