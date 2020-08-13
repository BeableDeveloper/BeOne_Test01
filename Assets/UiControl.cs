using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiControl : MonoBehaviour
{
    [SerializeField]  GameObject UIpanel;

    [SerializeField]  GameObject Pause;

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Landscape;
    }

    // Start is called before the first frame update
    void Start()
    {
        UIpanel.SetActive(false);
    }

   public void Pausebtn()
    {
        GameScript.timerIsRunning = false;
        UIpanel.transform.SetAsLastSibling();
        UIpanel.SetActive(true);
        
        Pause.SetActive(false);
    }

   public void Resumebtn()
    {

        UIpanel.SetActive(false);
        Pause.SetActive(true);
        GameScript.timerIsRunning = true;
    }

    public void Restartbtn()
    {
        SceneManager.LoadScene("Gamescene");
    }

    public void HomeBtn()
    {
        SceneManager.LoadScene("Dashboard");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
