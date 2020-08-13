using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SignIn : MonoBehaviour
{



    [SerializeField] GameObject mobileOtpPanel;

    [SerializeField] GameObject emailPanel;

    [SerializeField] Image image;

    int sceneindex;

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
        Screen.fullScreen = false;

        sceneindex = SceneManager.GetActiveScene().buildIndex;

        mobileOtpPanel.SetActive(false);
        emailPanel.SetActive(false);
      //  image.IsDestroyed();//Sript attach to the SignIn page email and otp panel
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !(mobileOtpPanel.activeInHierarchy == true))
        {
            Application.Quit();
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && (mobileOtpPanel.activeInHierarchy == true))
        {

            SceneManager.LoadScene("SignIn");
        }
    }

    /* if (Input.GetKeyDown(KeyCode.Escape) && !(if (Input.GetKeyDown(KeyCode.Escape) && !(profilepanel.activeInHierarchy == true) )
         {
             // profilepanel.SetActive(false);

             SceneManager.LoadScene("SignIn");

         }
         else if(Input.GetKeyDown(KeyCode.Escape) && (profilepanel.activeInHierarchy == true))
         {

             SceneManager.LoadScene("Devices");
         }) )
         {
             // profilepanel.SetActive(false);

             SceneManager.LoadScene("SignIn");

         }
         else if(Input.GetKeyDown(KeyCode.Escape) && (profilepanel.activeInHierarchy == true))
         {

             SceneManager.LoadScene("Devices");
         }*/




    public void VerifyOtp()
    {
        SceneManager.LoadScene("Devices");
    }


    public void GenerateOtp()
    {
        mobileOtpPanel.SetActive(true);  //load the mobile otp panel
    }

    public void Emailbtn()
    {
        emailPanel.SetActive(true);
       



    }

    /*public void Login()
    {
        SceneManager.LoadScene("Devices");
    }*/

    public void SignUp()
    {
        SceneManager.LoadScene("RegisterationPage");
    }

    public void Phonebtn()
    {
        mobileOtpPanel.SetActive(false);
        emailPanel.SetActive(false);
        Login.emailSignInException = "";
    }

    

}

