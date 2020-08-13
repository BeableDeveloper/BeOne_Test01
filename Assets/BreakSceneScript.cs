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

public class BreakSceneScript : MonoBehaviour
{

    
    public float _breakTime;
    public bool timerIsRunning = false;
   // public Text devicePath;
    Texture2D area;
    public GameObject photoHolder;
    // Start is called before the first frame update
    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        timerIsRunning = true;
        _breakTime = Dashboard.breakTime*20;//minutes to seconds
        var path = Application.persistentDataPath + "/"+GameScript.FileCounter + "area.png";
        //devicePath.text = path;

       // StartCoroutine(loadSpriteImageFromUrl(path)); //To load game path 
        //_breakTime = 10;
    }
    void Start()
    {
        var path = Application.persistentDataPath + "/"+ GameScript.FileCounter + "area.png";
        area = LoadPNG(path);
        photoHolder.GetComponent<RawImage>().texture = area;
        UploadGameData();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (_breakTime > 0)
            {
                
                _breakTime -= Time.deltaTime;
                
            }
            else
            {
                timerIsRunning = false;
                SceneManager.LoadScene("Dashboard");
            }
        }

    }

   public void UploadGameData()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        var CurrentDate = DateTime.Now.ToShortDateString();
        Debug.Log("CurrentDate" + CurrentDate);
        var sessionName = "SessionData" + DateTime.Now.ToString();

        DocumentReference docRef_Overall = db.Collection("users").Document(Login.userUID).Collection("armable").Document("overallProgress");

        docRef_Overall.GetSnapshotAsync().ContinueWithOnMainThread(task => {
            DocumentSnapshot GetUserDocument = task.Result;

            Debug.Log("Query snapshot is " + GetUserDocument);


            Debug.Log(String.Format("Document data for {0} document:", GetUserDocument.Id));
            var user = GetUserDocument.ToDictionary();

            int i = System.Convert.ToInt32(user["totalGameSessionsPlayed"].ToString());
            i = i + 1;
            user["totalGameSessionsPlayed"] = i;

            int j = Convert.ToInt32(user["totalGameDistanceMoved"].ToString());
            j = j + Convert.ToInt32(GameScript.DistanceTravelled);
            user["totalGameDistanceMoved"] = j;

            int k = Convert.ToInt32(user["totalRepetitions"].ToString());
            k = k + 1;//k = k + Convert.ToInt32(EnemyTriggerScript.scoreCount);
            user["totalRepetitions"] = k;

            int l = Convert.ToInt32(user["currentSessionNmbr"].ToString());
            l = l + 1;//k = k + Convert.ToInt32(EnemyTriggerScript.scoreCount);
            user["currentSessionNmbr"] = k;

            int m = Convert.ToInt32(user["totalTimePlayed"].ToString());
            m = m + Convert.ToInt32(Dashboard.gameTime);//k = k + Convert.ToInt32(EnemyTriggerScript.scoreCount);
            user["totalTimePlayed"] = k;



            docRef_Overall.UpdateAsync(user).ContinueWithOnMainThread(task2 => {
                Debug.Log("Overall Session details added");
            });


        });



    }


    public static Texture2D LoadPNG(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }
}
