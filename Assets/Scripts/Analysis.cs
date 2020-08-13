using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using Firebase.Firestore;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.IO;

public class Analysis : MonoBehaviour
{

    public bool statusBar;
    public AndroidStatusBar.States states = AndroidStatusBar.States.Visible;
    public Text totalSessionsPlayed, totalDistanceMoved,  totalTimePlayed, totalRepetitions;

    void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidStatusBar.dimmed = !statusBar;
            AndroidStatusBar.statusBarState = states;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Screen.fullScreen = false;
        LoadDashboardNumbers();
    }


    public void Testyourselfbtn()
    {
        SceneManager.LoadScene("TestYourselfscene");
    }

    public void MessagePanelbtn()
    {
        SceneManager.LoadScene("Message_1");
    }

    public void NotificationPanelbtn()
    {
        SceneManager.LoadScene("Notfication");
    }

    public void HomePanelbtn()
    {
        SceneManager.LoadScene("Devices");
    }

    public void Analysisbtn()
    {
        SceneManager.LoadScene("Analysis");
    }

    public void LoadDashboardNumbers()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(Login.userUID).Collection("armable").Document("overallProgress");

        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task1 =>
        {
            DocumentSnapshot snapshot = task1.Result;
            if (snapshot.Exists)
            {
                Debug.Log(string.Format("Document data for {0} document:", snapshot.Id));
                Dictionary<string, object> user1 = snapshot.ToDictionary();

                totalSessionsPlayed.text = user1["totalGameSessionsPlayed"].ToString();
                totalDistanceMoved.text = user1["totalGameDistanceMoved"].ToString() + " cms";
                totalTimePlayed.text = user1["totalTimePlayed"].ToString() + " mins";
                totalRepetitions.text = user1["totalRepetitions"].ToString() + " tms";
                


            }
        });

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // profilepanel.SetActive(false);

            SceneManager.LoadScene("Devices");

        }
    }
}
