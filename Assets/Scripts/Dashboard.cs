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
public class Dashboard : MonoBehaviour
{

    [SerializeField] GameObject createyourownsessions;

    [SerializeField] GameObject gamesessions;

    [SerializeField] GameObject dashboard;

    [SerializeField] GameObject Homescrollview;

    [SerializeField] GameObject ProfilePanel;




    static Firebase.Auth.FirebaseAuth auth;

    public Slider sessionTimeSlider, breakTimeSlider, playNmbrSlider;
    public Text sessionTimeText, breakTimeText, playNmbrText;
    public Text nmbrOfSelectedGames, nmbrOfGamesRemaining;
    public Text totalSessionsPlayed, totalDistanceMoved, totalScoreAchieved, totalTimePlayed, totalRepetitions, currentSessionNmbr;
    public static float gameTime, breakTime;
    public bool statusBar;
    public AndroidStatusBar.States states = AndroidStatusBar.States.Visible;
    public Text _totalCoins;
    public Text profileName;
    public Button playSessionGamesBtn;
    public Text gamePanelCoins;

    void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidStatusBar.dimmed = !statusBar;
            AndroidStatusBar.statusBarState = states;
        }
        LoadDashboardNumbers();
    }

    void Start()
    {
        profileName.text = Login.userName;
        _totalCoins.text = Login.totalCoins.ToString();
        gamePanelCoins.text = Login.totalCoins.ToString();
        Homescrollview.SetActive(false);
        ProfilePanel.SetActive(false);
        gamesessions.SetActive(false);
        createyourownsessions.SetActive(false);
        playSessionGamesBtn.interactable = false;
        //LoadDashboardNumbers();


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !(gamesessions.activeInHierarchy == true))
        {
            // profilepanel.SetActive(false);

            SceneManager.LoadScene("Devices");

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && (gamesessions.activeInHierarchy == true) && (Homescrollview.activeInHierarchy == true)&&(ProfilePanel.activeInHierarchy == true))
        {

            SceneManager.LoadScene("Dashboard");
        }



        if(sessionTimeSlider.value < 5)
        {
            sessionTimeSlider.value = 5;

        }
        else if(sessionTimeSlider.value > 5 && (sessionTimeSlider.value) < 10){
            sessionTimeSlider.value = 10;

        }
        else if (sessionTimeSlider.value > 10 && (sessionTimeSlider.value) < 15)
        {
            sessionTimeSlider.value = 15;

        }
        else if (sessionTimeSlider.value > 15 && (sessionTimeSlider.value) < 20)
        {
            sessionTimeSlider.value = 20;

        }
        else if (sessionTimeSlider.value > 20 && (sessionTimeSlider.value) < 25)
        {
            sessionTimeSlider.value = 25;

        }
        else if (sessionTimeSlider.value > 25 && (sessionTimeSlider.value) < 30)
        {
            sessionTimeSlider.value = 30;

        }



        sessionTimeText.text = sessionTimeSlider.value.ToString() + "mins";
        breakTimeText.text = breakTimeSlider.value.ToString() + " mins";
        playNmbrText.text ="Play " + playNmbrSlider.value.ToString();
        gameTime = sessionTimeSlider.value;
        breakTime = breakTimeSlider.value;


        //session game selection list code here


        GameObject[] selectedGames = GameObject.FindGameObjectsWithTag("selectedGameButton");
        Debug.Log("selected games " + selectedGames.Length);

        nmbrOfSelectedGames.text = "Play(" + selectedGames.Length.ToString() + ")";

        nmbrOfGamesRemaining.text ="("+ (playNmbrSlider.value - selectedGames.Length).ToString() +" more remaining"+")";

        if(selectedGames.Length == playNmbrSlider.value)
        {
            GameObject[] notSelectedGames = GameObject.FindGameObjectsWithTag("gameButton");
            playSessionGamesBtn.interactable = true;
            nmbrOfSelectedGames.color = Color.red;
            foreach (GameObject gameButton in notSelectedGames)
            {
                gameButton.GetComponent<Button>().interactable = false;


            }

        }
        else
        {
            playSessionGamesBtn.interactable = false;
            nmbrOfSelectedGames.color = Color.grey;
            GameObject[] notSelectedGames = GameObject.FindGameObjectsWithTag("gameButton");

            foreach (GameObject gameButton in notSelectedGames)
            {
                gameButton.GetComponent<Button>().interactable = true;


            }

        }

    }


    public void Selectgames()
    {
        gamesessions.SetActive(true);
        createyourownsessions.SetActive(false);
        dashboard.SetActive(false);
    }

    public void CreateSession()
    {
        createyourownsessions.SetActive(true);
        gamesessions.SetActive(false);
    }

    public void Backbtn()
    {
        SceneManager.LoadScene("Devices");
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

    //session play game selection code here

    public void GameButton()
    {
       var btn = EventSystem.current.currentSelectedGameObject;

        if(btn.GetComponent<Image>().color == Color.white)
        {
            btn.GetComponent<Image>().color = Color.yellow;
            btn.tag = "selectedGameButton";

        }
        else
        {
            btn.GetComponent<Image>().color = Color.white;
            btn.tag = "gameButton";
        }
        

    }

    //selected session games function code here on click play 

    public void playSelectedGames()
    {


       // GameObject[] buttons = GameObject.FindGameObjectsWithTag("gameButton");
       // Debug.Log("buttons found " + buttons.Length);


    }


    //load dashboard db data code here

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
                currentSessionNmbr.text = user1["currentSessionNmbr"].ToString();


            }
        });

    }


    public void OnclickPlay()
    {
        SceneManager.LoadScene("Gamescene");
    }

    public void profileClick()
    {
        Homescrollview.SetActive(true);
        ProfilePanel.SetActive(true);
        dashboard.SetActive(false);
    }

    public void OpenProgressScene()
    {
        SceneManager.LoadScene("Analysis");
    }

    public  void SignOut()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        Debug.Log("user signed out and removed from remember-me option , need to enter credentials on the next app open");
        PlayerPrefs.SetInt("rememberUserLogin", 0);
        
            auth.SignOut();
        SceneManager.LoadScene("SignIn");
        
    }


}
