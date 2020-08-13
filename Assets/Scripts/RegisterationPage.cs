using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegisterationPage : MonoBehaviour
{
    [SerializeField] GameObject   PatientScrollView;

    [SerializeField] GameObject TherapistScrollView;

    [SerializeField] GameObject Panel;

    [SerializeField] GameObject OtpPanelthe;

    [SerializeField] GameObject OtpPanelpatient;

    public bool statusBar;
    public AndroidStatusBar.States states = AndroidStatusBar.States.Visible;

    public Toggle warriorToggleBtn, healerToggleBtn, warriorToggleBtnTher, healerToggleBtnTher;

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
      

        TherapistScrollView.SetActive(false);

        PatientScrollView.SetActive(true);
        Panel.SetActive(false);
        OtpPanelthe.SetActive(false);
        OtpPanelpatient.SetActive(false);



    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("SignIn");
        }

        //switch tabs warrior/healer here

        if (warriorToggleBtnTher.isOn)
        {
           
            PatientScrollView.SetActive(true);
            TherapistScrollView.SetActive(false);
            Debug.Log("in warrior toggle");
            healerToggleBtn.isOn = false;
            warriorToggleBtn.isOn = true;

        }
        else if (healerToggleBtn.isOn)
        {
            
            PatientScrollView.SetActive(false);
            TherapistScrollView.SetActive(true);
            healerToggleBtnTher.isOn = true;
            Debug.Log("in healer toggle");
        }


    }

    public void Registerbtn()
    {
        SceneManager.LoadScene("SignIn");
    }

    public void RegsterasHealer()
    {
        TherapistScrollView.SetActive(true);
        PatientScrollView.SetActive(false);
    }

    public void RegsisterasWarrior()
    {
        TherapistScrollView.SetActive(false);
        PatientScrollView.SetActive(true);
    }

    public void Backbtn()
    {
        SceneManager.LoadScene("SignIn");
    }

    public void Addbtn()
    {
        Panel.SetActive(true);
        TherapistScrollView.SetActive(false);
    }

    public void Savebtn()
    {
        TherapistScrollView.SetActive(true);
        Panel.SetActive(false);
    }

    public void verifybtnpatient()
    {
        OtpPanelpatient.SetActive(true);
        TherapistScrollView.SetActive(false);
        Panel.SetActive(false);
        PatientScrollView.SetActive(false);
    }

    public void verifybtntherapist()
    {
        OtpPanelthe.SetActive(true);
        TherapistScrollView.SetActive(false);
        Panel.SetActive(false);
        PatientScrollView.SetActive(false);
    }



    public void ContinuebtnPatient()
    {
       // PatientScrollView.SetActive(true);
        OtpPanelpatient.SetActive(false);
        // TherapistScrollView.SetActive(false);

        PatientScrollView.SetActive(true);

    }

    public void Continuebtntherapist()
    {
        TherapistScrollView.SetActive(true);
        OtpPanelthe.SetActive(false);

    }

    public void Cancelbtn()
    {
        SceneManager.LoadScene("SignIn");
    }

    public void Cancelpanelbtn()
    {
        Panel.SetActive(false);
        TherapistScrollView.SetActive(true);
    }



    // Update is called once per frame
    
}
