using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ther_das : MonoBehaviour
{

    [SerializeField] GameObject createyourownsessions;

    [SerializeField] GameObject gamesessions;

    [SerializeField] GameObject dashboard;

    public bool statusBar;
    public AndroidStatusBar.States states = AndroidStatusBar.States.Visible;

    void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidStatusBar.dimmed = !statusBar;
            AndroidStatusBar.statusBarState = states;
        }
    }

    void Start()
    {
        gamesessions.SetActive(false);
        createyourownsessions.SetActive(false);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !(gamesessions.activeInHierarchy == true))
        {
            // profilepanel.SetActive(false);

            SceneManager.LoadScene("Therapist_Notif");

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && (gamesessions.activeInHierarchy == true))
        {

            SceneManager.LoadScene("Therapist_Dashboard");
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

    // Update is called once per frame

}
