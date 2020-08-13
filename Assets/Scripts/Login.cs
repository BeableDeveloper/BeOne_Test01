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


public class Login : MonoBehaviour
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
    public static int totalCoins;
    public Text savedPhoneNumber;
    public Toggle remeberMe;
    public GameObject resendOTPText;
    public GameObject resendOTPButton;
    public bool runOTPTimer = false;
    public float timeRemaining = 30;
    public Text OTPTimerText;
    public static string emailSignInException;
    public GameObject errorText;
    //public Text statusTxt;
    Firebase.Auth.FirebaseUser user;
    void Awake()
    {
        //check "remember me status here"


        if (PlayerPrefs.HasKey("rememberUserLogin"))
        {
            Debug.Log("key is present");
            if (PlayerPrefs.GetInt("rememberUserLogin", 0) == 1)
            {
                InitializeFirebase();
                
                Debug.Log("key value is "+ PlayerPrefs.GetInt("rememberUserLogin", 0));
            }
        }

       
    }

    void Start()
    {
        errorText.SetActive(false);
        emailSignInException = "";
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        resendOTPText.SetActive(false);
        resendOTPButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        
            errorText.GetComponent<Text>().text = emailSignInException;
            errorText.SetActive(true);

        
        

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
                //errorText.SetActive(true);
                //errorText.gameObject.GetComponent<Text>().text = "Entered email/password is incorrect. Please check";
                emailSignInException = "Entered email/password is incorrect. Please check";
                Debug.Log("test");
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                errorText.SetActive(true);
                //emailSignInException = task.Exception.ToString();

                return;
            }
            
            Firebase.Auth.FirebaseUser newUser = task.Result;



            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            
            userUID = newUser.UserId;

            GetUserName();
            //to get user name from db

           /* FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

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
                    Debug.Log("remeberMe.isOn value is " + remeberMe.isOn);
                    if (remeberMe.isOn)
                    {
                        Debug.Log("in remember me if condition");
                        RememberMe();
                    }
                    signin = true;

                    // signin = true;

                }
            });*/

            
            
           

        });
        
        //signin = true;
    }


    public void GetUserName()
    {
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
                totalCoins = int.Parse(user1["TotalCoins"].ToString());

                Debug.Log("userName is " + userName);

                Debug.Log("remeberMe.isOn value is " + remeberMe.isOn);
                if (remeberMe.isOn)
                {
                    Debug.Log("in remember me if condition");
                    RememberMe();
                }
                signin = true;

            }
        });

    }




    public void GetOTP()                                                                 //Function to send otp to the valid user mobile number
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;    //Firebase auth instance creation to start authorization activity


        uint phoneAuthTimeoutMs = 10000;                                                 //
        PhoneAuthProvider provider = PhoneAuthProvider.GetInstance(auth);
        provider.VerifyPhoneNumber("+91"+phonenumber.text, phoneAuthTimeoutMs, null,
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
                Debug.Log("+91"+phonenumber.text);
                Debug.Log("SMS Has been sent and the verification Id is  " + id);
                runOTPTimer = true;
                resendOTPText.SetActive(false);
                resendOTPButton.SetActive(false);
            },
            codeAutoRetrievalTimeOut: (id) =>
            {
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
                OTPTimerText.text = "Otp entered is invalid";
                return;
            }

            Debug.Log("Phone Sign In successed.");
            Firebase.Auth.FirebaseUser newUser = task.Result;

            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
           
            userUID = newUser.UserId;


            GetUserName();
            
        });

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

    public void RememberMe()
    {
        PlayerPrefs.SetInt("rememberUserLogin", 1);

        Debug.Log("player pref value set for the remember me option");
        
    }

    void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

   

    // Track state changes of the auth object.
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
                userUID = user.UserId;
                GetUserName();
                Debug.Log("already logged in");
            }
        }
    }

    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }

    

    
}
