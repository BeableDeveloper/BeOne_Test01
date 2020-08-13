using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MessagePanel : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        

        Screen.fullScreen = false;
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

    public void Notbtn()
    {
        SceneManager.LoadScene("Notfication");
    }


    public void Analysisbtn()
    {
        SceneManager.LoadScene("Analysis");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // profilepanel.SetActive(false);

            SceneManager.LoadScene("Devices");

        }
    }
}
