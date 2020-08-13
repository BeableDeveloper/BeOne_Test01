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


public class SignUp2 : MonoBehaviour
{

    static Firebase.Auth.FirebaseAuth auth;
    public InputField email;
    public InputField password;
    public InputField phonenumber;
    public static string phoneVerificationId;
    public InputField otp;
    bool signin = false;
    public static string userUID;
    public static string userName;
    public Text savedPhoneNumber;
    public static Firebase.Auth.FirebaseUser newUser;
    public static bool isSignupDone = false;
    public static string registeredUserID;
    public GameObject resendOTPText;
    public GameObject resendOTPButton;
    public bool runOTPTimer = false;
    public float timeRemaining = 30;
    public Text OTPTimerText;
    public GameObject patientOtpPanel, mainRegPanelPatient;
    // Start is called before the first frame update
    void Start()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        resendOTPText.SetActive(false);
        resendOTPButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (signin)
            SceneManager.LoadScene("Devices");

        savedPhoneNumber.text = phonenumber.text;

        if (runOTPTimer)
        {
            if (timeRemaining > 1)
            {
               
               // float seconds = Mathf.FloorToInt(timeRemaining % 60);
                timeRemaining -= Time.deltaTime;
               // OTPTimerText.text = "Code expires in "+ timeRemaining + "seconds";


                DisplayTime(timeRemaining);

            }
            else
            {
                resendOTPText.SetActive(true);
                resendOTPButton.SetActive(true);

                OTPTimerText.text = "Code expired";
                runOTPTimer = false;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        //timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        //liveTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds); ;

        OTPTimerText.text = "Code expires in " + string.Format("{0:00}:{1:00}", minutes, seconds) + " seconds";
    }
    public void SignInEmail()
    {

        Time.timeScale = 1.0f;
        auth.SignInWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;

            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            Debug.Log("Working user registered");
            Debug.Log("Patient UID in login scene " + newUser.UserId);
            userUID = newUser.UserId;


            FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

            DocumentReference docRef = db.Collection("users").Document(userUID);

            docRef.GetSnapshotAsync().ContinueWithOnMainThread(task1 =>
            {
                DocumentSnapshot snapshot = task1.Result;
                if (snapshot.Exists)
                {
                    Debug.Log(string.Format("Document data for {0} document:", snapshot.Id));
                    Dictionary<string, object> user1 = snapshot.ToDictionary();


                    userName = user1["FirstName"].ToString();

                    Debug.Log("userName is " + userName);
                    signin = true;

                }
            });

        });
        Debug.Log("user id is " + userUID);

        //signin = true;


    }




    public void GetOTP()                                                                 //Function to send otp to the valid user mobile number
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;    //Firebase auth instance creation to start authorization activity


        uint phoneAuthTimeoutMs = 30000;                                                 //
        PhoneAuthProvider provider = PhoneAuthProvider.GetInstance(auth);
        provider.VerifyPhoneNumber("+91" + phonenumber.text, phoneAuthTimeoutMs, null,
            verificationCompleted: (credential) =>
            {

                Debug.Log("1");

                //OnVerifyOTP(credential);
            },
            verificationFailed: (error) =>
            {

                Debug.Log("Verification failed");


            },
            codeSent: (id, token) =>
            {
                phoneVerificationId = id;
                Debug.Log("+91" + phonenumber.text);
                Debug.Log("SMS Has been sent and the verification Id is  " + id);
                runOTPTimer = true;
                resendOTPText.SetActive(false);
                resendOTPButton.SetActive(false);
            },
            codeAutoRetrievalTimeOut: (id) =>
            {
                //this is callback function and will be called after otp entry time is over.
                //enable resend otp button here
                resendOTPText.SetActive(true);
                resendOTPButton.SetActive(true);
                Debug.Log("Code Retrieval Time out");

            });





    }

    public void ResendOtpFn()
    {
        GetOTP();
        timeRemaining = 30;
        resendOTPText.SetActive(false);
        resendOTPButton.SetActive(false);
        runOTPTimer = true;

        
    }
   
    public void SignInPhone()
    {

        PhoneAuthProvider provider = PhoneAuthProvider.GetInstance(auth);
        Credential credential = provider.GetCredential(phoneVerificationId, otp.text);

        auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
            if (task.IsFaulted)
            {
                Debug.Log("SignInWithCredentialAsync encountered an error: " + task.Exception);

                return;
            }

            Debug.Log("Phone Sign In successed.");
            // PhoneLoginSuccess();
            patientOtpPanel.SetActive(false);
            mainRegPanelPatient.SetActive(true);




        });

    }

    public void LinkEmailToPhone()
    {


        Firebase.Auth.Credential credential = Firebase.Auth.EmailAuthProvider.GetCredential(email.text, password.text);

        auth.CurrentUser.LinkWithCredentialAsync(credential).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("LinkWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("LinkWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }

            newUser = task.Result;
            Debug.LogFormat("Credentials successfully linked to Firebase user: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            registeredUserID = newUser.UserId;
            isSignupDone = true;
           // CreateUserDocument();
           // register = true;


        });



    }

    public void OpenSignInScene()
    {

        SceneManager.LoadScene("SignIn");
    }
    public void ForgotPassword()
    {
        string emailAddress = email.text;


        auth.SendPasswordResetEmailAsync(emailAddress).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SendPasswordResetEmailAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SendPasswordResetEmailAsync encountered an error: " + task.Exception);
                return;
            }

            Debug.Log("Password reset email sent successfully.");
        });


    }


}
