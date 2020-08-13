using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NotficationScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.fullScreen = false;
    }

    public void NotificationPanelbtn()
    {
        SceneManager.LoadScene("Notfication");
    }


    public void MessagePanelbtn()
    {
        SceneManager.LoadScene("Message_1");
    }
    public void HomePanelbtn()
    {
        SceneManager.LoadScene("Devices");
    }
    public void Testyourselfbtn()
    {
        SceneManager.LoadScene("TestYourselfscene");
    }

    public void Analysisbtn()
    {
        SceneManager.LoadScene("Analysis");
    }





    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) )
        {
            // profilepanel.SetActive(false);

            SceneManager.LoadScene("Devices");

        }
    }
}
