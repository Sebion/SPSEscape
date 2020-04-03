using System;
using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class FirebaseSetup
{
    public DatabaseReference databaseReference;
    private FirebaseApp app;
    private bool isTaken ;


    public FirebaseSetup()
    {
        
        SetupFirebase();
    }

    private void SetupFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                AppOptions options = AppOptions.LoadFromJsonConfig(
                    "{\"project_info\":{\"project_number\":\"391065412110\",\"firebase_url\":\"https://spsescape-c4e57.firebaseio.com\",\"project_id\":\"spsescape-c4e57\",\"storage_bucket\":\"spsescape-c4e57.appspot.com\"},\"client\":[{\"client_info\":{\"mobilesdk_app_id\":\"1:391065412110:android:27d960f49843694ee3925f\",\"android_client_info\":{\"package_name\":\"com.spse.game\"}},\"oauth_client\":[{\"client_id\":\"391065412110-8puh0m93l82ft948236eqcujg0n7nogg.apps.googleusercontent.com\",\"client_type\":3}],\"api_key\":[{\"current_key\":\"AIzaSyDExt8V83H7OHqyfquP8YvpUyszljmGCJw\"}],\"services\":{\"appinvite_service\":{\"other_platform_oauth_client\":[{\"client_id\":\"391065412110-8puh0m93l82ft948236eqcujg0n7nogg.apps.googleusercontent.com\",\"client_type\":3}]}}}],\"configuration_version\":\"1\"}");
                app = FirebaseApp.Create(options);
                app.SetEditorDatabaseUrl("https://spsescape-c4e57.firebaseio.com/");
                databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            }
            else
            {
                Debug.LogError(String.Format(
                    "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });
    }


    public void WriteUserToDatabase(string username, int score)
    {
        databaseReference.Child("Players").Child(username).Child("Highscore").SetValueAsync(score);
        Debug.Log("Zapisalo");
    }

    public void GetAllPlayersFromDatabase(string username)
    {
        isTaken = false;
         databaseReference.Child("Players")
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
                    Debug.Log("toto je zadane meno"+username);
                    
                    foreach (var pair in jObject)
                    {
                        
                        Debug.Log(pair.Key);
                        
                        if (username == pair.Key)
                        {
                            Debug.Log("Uz je take meno");
                            isTaken = true;
                            Debug.Log("v prvej metode"+isTaken);
                            break;
                            

                        }
                    }
                }
            });
        
    }

    
    
    

// Update is called once per frame
}