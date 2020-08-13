using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ther_not : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.fullScreen = false;
    }

    public void NotificationPanelbtn_th()
    {
        SceneManager.LoadScene("Notfication");
    }


    public void MessagePanelbtn_th()
    {
        SceneManager.LoadScene("Message_1");
    }
    public void HomePanelbtn_th()
    {
        SceneManager.LoadScene("Devices");
    }
    public void Testyourselfbtn_th()
    {
        SceneManager.LoadScene("TestYourselfscene");
    }

    public void Analysisbtn_th()
    {
        SceneManager.LoadScene("Analysis");
    }

    public void Ther_das()
    {
        SceneManager.LoadScene("Therapist_Dashboard");
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
