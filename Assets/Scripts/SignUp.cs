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
using System.Globalization;
using System.Text.RegularExpressions;

public class SignUp : MonoBehaviour
{
   


    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;


    [Header("Input Fields")]

    public InputField firstName;
    public InputField lastName;
    public InputField email;
  
    public InputField pincode;
    public InputField address1;
    public InputField address2;
    public InputField city;
    public InputField mobile;
    public InputField password;
    public InputField otp;

    //Toggle Buttons
    [Header("Buttons")]
    public ToggleGroup male;
    public ToggleGroup female;
    public ToggleGroup Other;

    

    //DropDown Lists
    [Header("DropDown Lists")]
    public Dropdown StateDropDown;
    public Dropdown dayDropdown;
    public Dropdown monthDropDown;
    public Dropdown yearDropDown;
    public Dropdown countryCodeDD;
    public Dropdown countryListDD;
    public Dropdown cityListDD;
   
    bool register = false;
    public GameObject otpPanel;
    bool allFieldsAreCorrect = false;
    

    //public Image loadBar;

    //On Script Variables
    public static string phoneVerificationId;
    public static string patient_UId;
    public Text savedPhoneNmbr;


    //Credentials

    public Credential phoneCredential;
    Firebase.Auth.FirebaseUser newUser;

    

    void Start()
    {
       
        patient_UId = "";

        
        List<string> dayList = new List<string>() { "01", "02", "03", "04", "05", "06", "07", "08", "09" };
        List<string> monthList = new List<string>() { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
        List<string> yearList = new List<string>();

        List<string> countryCodes = new List<string>() {"+91","+49","+1" };
        List<string> countryList = new List<string>() { "India", "Afghanistan" };
        countryListDD.AddOptions(countryList);
        List<string> cityList = new List<string>() { "Hyderabad", "Bangalore", "Delhi" };
        List<string> monthExpList = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11" };

        List<string> yearsExpList = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11" };
        List<string> cityList01 = new List<string>() { "Vijayawada", "Hyderabad", "Waranagal", "Karimnagar", "Secunderabad" };
         cityListDD.AddOptions(cityList);
        List<string> stateList = new List<string>() { "Telangana","AndhraPradesh", "Tamilnadu", "Karnataka", "Maharashtra","Punjab","Odisha", "West Bengal", "Rajasthan", "Madhya Pradesh", "Gujarat", "Assam" , "Bihar", "Haryana", "Chhattisgarh", "Himachal Pradesh", "Goa", "Jharkhand", "Uttarakand", "Tripura", "Nagaland", "Sikkim", "Mizoram", "Kerala", "Uttar Pradesh" };
        StateDropDown.AddOptions(stateList);

        var currentYear = DateTime.Now.Year;

        for (var j = 10; j < 32; j++)
        {

            dayList.Add(j.ToString());



        }


        for (var i = 1947; i <= currentYear; i++)
        {

            yearList.Add(i.ToString());

        }

       // StateDropDown.AddOptions(stateList);
        dayDropdown.AddOptions(dayList);
        monthDropDown.AddOptions(monthList);
        yearDropDown.AddOptions(yearList);
        countryCodeDD.AddOptions(countryCodes);
        

    }

    void Update()
    {
        if (register)
            SceneManager.LoadScene("SignIn");

        savedPhoneNmbr.text = mobile.text;

        if (SignUp2.isSignupDone)
        {
            CreateUserDocument();
            SignUp2.isSignupDone = false;
        }

       
    }

   
    public void LoadStateList()
    {
        if (countryListDD.options[countryListDD.value].text == "India")
        {
            StateDropDown.ClearOptions();
            List<string> stateList = new List<string>() { "Andhra Pradesh", "Arunachal Pradesh", "Assam", "Bihar", "Chattisgarh", "Goa", "Gujarat", "Haryana", "Himachal Pradesh", "Jharkhand", "Karnataka", "Kerala", "Madhya Pradesh", "Maharashtra", "Manipur", "Meghalaya", "Mizoram", "Nagaland", "Odisha", "Punjab", "Rajasthan", "Sikkim", "Tamil Nadu", "Telangana", "Tripura", "Uttar Pradesh", "Uttarakhand", "West Bengal" };
            StateDropDown.AddOptions(stateList);
           

        }

        if (countryListDD.options[countryListDD.value].text == "Afghanistan")

        {

            StateDropDown.ClearOptions();
            List<string> stateList1 = new List<string>() { "Badakhshan", "Farah", "Helmand", "Kandahar" };
            StateDropDown.AddOptions(stateList1);
            


        }
    }

    public void LoadCityList()
    {
        
         if(countryListDD.options[countryListDD.value].text == "India")
         {
             if (StateDropDown.options[StateDropDown.value].text == "Telangana")
             {
                 cityListDD.ClearOptions();
                 List<string> cityList01 = new List<string>() { "Hyderabad", "Warangal", "Nizamabad", "Karimnagar", "Ramagundam", "Mahabubnagar", "Nalgonda", "Adilabad", "Siddipet", "Suryapet", "Miryalaguda" };
                 cityListDD.AddOptions(cityList01);

             }
             else if (StateDropDown.options[StateDropDown.value].text == "AndhraPradesh")
             {
                 cityListDD.ClearOptions();
                 List<string> cityList02 = new List<string>() { "Visakhapatnam","Vijayawada","Guntur","Rajamahendravaram","Kakinada","Nellore","Kurnool","Kadapa","Tirupati","Anantapuram","Eluru","Vizianagaram","Nandyal","Ongole","Proddatur","Adoni","Madanapalle","Chittoor","Chirala","Machilipatnam","Tenali","Hindupur","Srikakulam","Bhimavaram","Guntakal","Dharmavaram","Gudivada","Narasaraopet","Tadipatri","Mangalagiri","Tadepalligudem","Chilakaluripet","Yemmiganur"
  };
                 cityListDD.AddOptions(cityList02);

             }
            else if (StateDropDown.options[StateDropDown.value].text == "Tamilnadu")
            {
                cityListDD.ClearOptions();
                List<string> cityList03 = new List<string>() { "Chennai","Coimbatore","Madurai","Tiruchirappalli","Tiruppur","Salem","Erode","Tirunelveli","Vellore","Thoothukkudi","Dindigul","Thanjavur","Ranipet","Sivakasi","Karur","Udhagamandalam","Hosur","Nagercoil","Kancheepuram","Kumarapalayam","Karaikkudi","Neyveli","Cuddalore","Kumbakonam","Tiruvannamalai","Pollachi","Rajapalayam","Gudiyatham","Pudukkottai","Vaniyambadi","Ambur","Nagapattinam"};
                cityListDD.AddOptions(cityList03);

            }
            else if (StateDropDown.options[StateDropDown.value].text == "Karnataka")
            {
                cityListDD.ClearOptions();
                List<string> cityList03 = new List<string>() { "Bengaluru","Hubli-Dharwad","Mysuru","Kalaburagi","Mangaluru","Belagavi","Davanagere","Ballari","Vijayapura","Shivamogga","Tumakuru","Raichur","Bidar","Hosapete","Gadag-Betageri","Robertsonpete","Hassan","Bhadravati","Chitradurga","Udupi","Kolara","Mandya","Chikkamagaluru","Gangavati","Bagalkote","Ranebennuru"};
                cityListDD.AddOptions(cityList03);

            }
            else if (StateDropDown.options[StateDropDown.value].text == "Maharashtra")
            {
                cityListDD.ClearOptions();
                List<string> cityList03 = new List<string>() { "Mumbai","PMC, Pune","Nagpur","Thane","PCMC, Pune","Nashik","Kalyan-Dombivli","Vasai-Virar City MC","Aurangabad","Navi Mumbai","Solapur","Mira-Bhayandar","Bhiwandi-Nizampur MC","Amravati","Nanded Waghala","Kolhapur","Akola","Panvel","Ulhasnagar","Sangli-Miraj-Kupwad","Malegaon","Jalgaon","Latur","Dhule","Ahmednagar","Chandrapur","Parbhani","Ichalkaranji","Jalna","Ambarnath","Bhusawal","Ratnagiri","Beed","Gondia","Satara","Barshi","Yavatmal","Achalpur","Osmanabad","Nandurbar","Wardha","Udgir","Hinganghat" };
                cityListDD.AddOptions(cityList03);

            }
            else if (StateDropDown.options[StateDropDown.value].text == "Punjab")
            {
                cityListDD.ClearOptions();
                List<string> cityList03 = new List<string>() { "Ludhiana","Amritsar","Jalandhar","Patiala","Bathinda","Hoshiarpur","Mohali","Batala","Pathankot","Moga","Abohar","Malerkotla","Khanna","Phagwara","Muktasar","Barnala","Rajpura","Firozpur","Kapurthala","Sunam" };
                cityListDD.AddOptions(cityList03);

            }
            else if (StateDropDown.options[StateDropDown.value].text == "Odisha")
            {
                cityListDD.ClearOptions();
                List<string> cityList03 = new List<string>() { "Bhubaneswar","Cuttack","Berhampur","Rourkela","Puri","Sambalpur","Balasore","Baripada","Bhadrak","Balangir","Jharsuguda","Jeypore","Bargarh","Rayagada","Bhawanipatna","Paradip","Phulbani","Jajpur","Angul","Dhenkanal" };
                cityListDD.AddOptions(cityList03);

            }
            else if (StateDropDown.options[StateDropDown.value].text == "West Bengal")
            {
                cityListDD.ClearOptions();
                List<string> cityList03 = new List<string>() { "Kolkata","Siliguri","Durgapur","Bardhaman","English Bazar","Baharampur","Habra","Kharagpur","Shantipur","Dankuni","Dhulian","Ranaghat","Haldia","Raiganj","Krishnanagar","Nabadwip","Medinipur","Jalpaiguri","Balurghat","Basirhat","Bankura","Chakdaha","Darjeeling","Alipurduar","Purulia","Jangipur","Bangaon","Cooch Behar" };
                cityListDD.AddOptions(cityList03);

            }
            else if (StateDropDown.options[StateDropDown.value].text == "Rajasthan")
            {
                cityListDD.ClearOptions();
                List<string> cityList03 = new List<string>() { "Jaipur","Jodhpur","Kota","Bikaner","Ajmer","Udaipur","Bhilwara","Alwar","Bharatpur","Sri Ganganagar","Sikar","Pali","Hanumangarh" };
                cityListDD.AddOptions(cityList03);

            }
            else if (StateDropDown.options[StateDropDown.value].text == "Madhya Pradesh")
            {
                cityListDD.ClearOptions();
                List<string> cityList03 = new List<string>() { "Indore","Bhopal","Jabalpur","Gwalior","Ujjain","Sagar","Dewas","Satna","Ratlam","Rewa","Murwara (Katni)","Singrauli","Burhanpur","Khandwa","Bhind","Chhindwara","Guna","Shivpuri","Vidisha","Chhatarpur","Damoh","Mandsaur","Khargone","Neemuch","Pithampur","Gadarwara","Hoshangabad","Itarsi","Sehore","Morena","Betul","Seoni","Datia","Nagda","Mundi" };
                cityListDD.AddOptions(cityList03);

            }
            else if (StateDropDown.options[StateDropDown.value].text == "Gujarat")
            {
                cityListDD.ClearOptions();
                List<string> cityList03 = new List<string>() { "Ahmedabad","Surat","Vadodara","Rajkot","Bhavnagar","Jamnagar","Junagadh","Anand","Navsari","Morbi","Gandhinagar","Gandhidham","Nadiad","Surendranagar","Bharuch","Porbandar"};
                cityListDD.AddOptions(cityList03);

            }
            else if (StateDropDown.options[StateDropDown.value].text == "Assam")
            {
                cityListDD.ClearOptions();
                List<string> cityList03 = new List<string>() { "Guwahati", "Silchar", "Dibrugarh", "Jorhat", "Nagaon", "Bongaigaon", "Tinsukia", "Tezpur" };
                cityListDD.AddOptions(cityList03);

            }
            else if (StateDropDown.options[StateDropDown.value].text == "Bihar")
            {
                cityListDD.ClearOptions();
                List<string> cityList03 = new List<string>() { "Patna", "Gaya", "Bhagalpur", "Muzaffarpur", "Purnia", "Darbhanga", "Bihar Sharif", "Arrah", "Begusarai", "Katihar", "Munger", "Chhapra", "Danapur", "Bettiah", "Saharsa", "Sasaram", "Hajipur", "Dehri", "Siwan", "Motihari", "Nawada", "Bagaha", "Buxar", "Kishanganj", "Sitamarhi", "Jamalpur", "Jehanabad", "Aurangabad" };
                cityListDD.AddOptions(cityList03);

            }
            else if (StateDropDown.options[StateDropDown.value].text == "Haryana")
            {
                cityListDD.ClearOptions();
                List<string> cityList03 = new List<string>() { "Faridabad", "Gurugram", "Panipat", "Ambala", "Yamunanagar", "Rohtak", "Hisar", "Karnal", "Sonipat", "Panchkula", "Bhiwani", "Sirsa", "Bahadurgarh", "Jind", "Thanesar", "Kaithal", "Rewari", "Palwal", "Pundri", "Kosli" };
                cityListDD.AddOptions(cityList03);

            }
            else if (StateDropDown.options[StateDropDown.value].text == "Chhattisgarh ")
            {
                cityListDD.ClearOptions();
                List<string> cityList03 = new List<string>() { "Raipur","Bhilai","korba","bilaspur","Rajnandgaon","Raigarh","jagdalpur","Ambikapur","Chirmiri","Dhamtari","Mahasamund"};
                cityListDD.AddOptions(cityList03);

            }
            else if (StateDropDown.options[StateDropDown.value].text == "Himachal Pradesh")
            {
                cityListDD.ClearOptions();
                List<string> cityList03 = new List<string>() { "Shimla","Solan","Dharamsala","Baddi","Nahan","Mandi","Paonta Sahib","Sundarnagar","Chamba","Una","Kullu","Hamirpur","Bilaspur","Yol Cantonment","Nalagarh","Nurpur","Kangra","Santokhgarh","Mehatpur Basdehra","Shamshi","Parwanoo","Manali","Tira Sujanpur","Ghumarwin","Dalhousie","Rohru","Nagrota Bagwan","Rampur","Kumarsain","Jawalamukhi","Jogindernagar","Dera Gopipur","Sarkaghat","Jhakhri","Indora","Bhuntar","Nadaun","Theog","Kasauli Cantonment","Gagret","Chuari Khas","Daulatpur","Sabathu Cantonment","Dalhousie Cantonment","Palampur","Rajgarh","Arki","Dagshai Cantonment","Seoni","Talai","Jutogh Cantonment","Chaupal","Rewalsar","Bakloh Cantonment","Jubbal","Bhota","Banjar","Naina Devi","Kotkhai","Narkanda" };
                cityListDD.AddOptions(cityList03);

            }
            else if (StateDropDown.options[StateDropDown.value].text == "Goa")
            {
                cityListDD.ClearOptions();
                List<string> cityList03 = new List<string>() { "Panaji","Bicholim","Canacona","Cuncolim","Curchorem","Mapusa","Margao","Mormugao","Pernem","Ponda","Quepem","Sanguem","Sanquelim","Valpoi" };
                cityListDD.AddOptions(cityList03);

            }
            else if (StateDropDown.options[StateDropDown.value].text == "Jharkhand")
            {
                cityListDD.ClearOptions();
                List<string> cityList03 = new List<string>() { "Jamshedpur","Dhanbad","Ranchi","Bokaro Steel City","Deoghar","Chakradharpur","Phusro","Hazaribagh","Giridih","Ramgarh","Medininagar","Chirkunda"};
                cityListDD.AddOptions(cityList03);

            }
            else if (StateDropDown.options[StateDropDown.value].text == "Uttarakand")
            {
                cityListDD.ClearOptions();
                List<string> cityList03 = new List<string>() { "Dehradun", "Haldwani-cum-Kathgodam", "Haridwar", "Roorkee", "Rudrapur", "Kashipur", "Rishikesh" };
                cityListDD.AddOptions(cityList03);

            }
            else if (StateDropDown.options[StateDropDown.value].text == "Tripura")
            {
                cityListDD.ClearOptions();
                List<string> cityList03 = new List<string>() { "Agartala","Amarpur","Belonia","Dharmanagar","Kailasahar","Kamalpur","Khowai","Kumarghat"," 	Ranirbazar","Sabroom","Sonamura","Teliamura","Udaipur","Bishalgarh","Santirbazar","Ambassa","Jirania","Mohanpur","Melaghar","Panisagar" };
                cityListDD.AddOptions(cityList03);

            }
            else if (StateDropDown.options[StateDropDown.value].text == "Nagaland")
            {
                cityListDD.ClearOptions();
                List<string> cityList03 = new List<string>() { "Dimapur","Kiphire","Kohima","Longleng","Mokokchung","Mon","Peren","Phek","Tuensang","Wokha","Zunheboto","Noklak" };
                cityListDD.AddOptions(cityList03);

            }
            else if (StateDropDown.options[StateDropDown.value].text == "Sikkim")
            {
                cityListDD.ClearOptions();
                List<string> cityList03 = new List<string>() { "Gangtok", "Jorethang", "Namchi", "Rangpo" };
                cityListDD.AddOptions(cityList03);

            }
            else if (StateDropDown.options[StateDropDown.value].text == "Mizoram")
            {
                cityListDD.ClearOptions();
                List<string> cityList03 = new List<string>() { "Aizawl", "Champha", "Kolasib", "Lawngtlai", "Lunglei", "Mamit", "Saiha", "Serchhip", "Mizoram" };
                cityListDD.AddOptions(cityList03);

            }
            else if (StateDropDown.options[StateDropDown.value].text == "Kerala")
            {
                cityListDD.ClearOptions();
                List<string> cityList03 = new List<string>() { "Thiruvananthapuram", "Kochi", "Kozhikode", "Kollam", "Thrissur", "Kannur" };
                cityListDD.AddOptions(cityList03);

            }
            else if (StateDropDown.options[StateDropDown.value].text == "Uttar Pradesh")
            {
                cityListDD.ClearOptions();
                List<string> cityList03 = new List<string>() { "Lucknow","Kanpur","Ghaziabad","Agra","Meerut","Varanasi","Prayagraj","Bareilly","Aligarh","Moradabad","Saharanpur","Gorakhpur","Ayodhya","Firozabad","Jhansi","Muzaffarnagar","Mathura-Vrindavan","Budaun","Rampur","Shahjahanpur","Farrukhabad","Ayodhya Cantt","Maunath Bhanjan","Hapur","Noida","Etawah","Mirzapur-cum-Vindhyachal","Bulandshahr","Sambhal","Amroha"," 	Hardoi","Fatehpur","Raebareli","Orai","Sitapur","Bahraich","Modinagar","Unnao","Jaunpur","Lakhimpur"," 	Hathras","Banda","Pilibhit","Deen Dayal upadhayay","Barabanki","Khurja","Gonda","Mainpuri","Lalitpur","Etah","Deoria","Ujhani"," 	Ghazipur","Sultanpur","Azamgarh","Bijnor","Sahaswan","Basti"," Chandausi","Akbarpur","Ballia"," 	Tanda","Greater Noida","Shikohabad","Shamli","Awagarh","Kasganj" };
                cityListDD.AddOptions(cityList03);

            }
        }

         else if (countryListDD.options[countryListDD.value].text == "Afghanistan")
         {
             if (StateDropDown.options[StateDropDown.value].text == "Badakhshan")
             {
                 cityListDD.ClearOptions();
                 List<string> cityList03 = new List<string>() { "Khandud", "Chakaran", "Baharak", "Jurm" };
                 cityListDD.AddOptions(cityList03);

             }
            else if (StateDropDown.options[StateDropDown.value].text == "Farah")
             {
                 cityListDD.ClearOptions();
                 List<string> cityList04 = new List<string>() { "Khandud", "Chakaran", "Baharak", "Jurm" };
                 cityListDD.AddOptions(cityList04);

             }
         }

     

    }

   
    public void GetOTP()                                                                 //Function to send otp to the valid user mobile number
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;    //Firebase auth instance creation to start authorization activity


        uint phoneAuthTimeoutMs = 10000;                                                 //
        PhoneAuthProvider provider = PhoneAuthProvider.GetInstance(auth);
        provider.VerifyPhoneNumber("+91"+mobile.text, phoneAuthTimeoutMs, null,
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
                Debug.Log("+91"+mobile.text);
                Debug.Log("SMS Has been sent and the verification Id is  " + id);
            },
            codeAutoRetrievalTimeOut: (id) =>
            {

                Debug.Log("Code Retrieval Time out");

            });





    }

   

    public void PhoneSignup()                                                              // Registers user through phone number and links with the mail
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

            otpPanel.SetActive(false);

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
            CreateUserDocument();
            register = true;
            
           
        });



    }       


    public void OnlyforEmailSignup()                         //use this function on Register button to sign in without phone authentication script..saves us from building apk to test
    {
        Validation();
        if (allFieldsAreCorrect) 
        {
            var auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
            auth.CreateUserWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(task => {

                if (task.IsCanceled)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }

                // Firebase user has been created.

                newUser = task.Result;
                Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);


                CreateUserDocument();
                register = true;

            });

        }

       

      





       


    }

    
    public void CreateUserDocument()                                 // Creates firebase document with UID in "AllUsers" collection.
    {
        Debug.Log("in create user doc");

        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        Debug.Log("in create user doc after firebase db initialization");
        DocumentReference docRef = db.Collection("users").Document(SignUp2.registeredUserID);
        Dictionary<string, object> user = new Dictionary<string, object>
              {
                  { "FirstName",firstName.text },
                  { "LastName",lastName.text },
                  { "Email",email.text },
                  { "pincode",pincode.text },
                  {"Day",dayDropdown.options[dayDropdown.value].text },
                  {"Month",monthDropDown.options[monthDropDown.value].text},
                  {"Year",yearDropDown.options[yearDropDown.value].text},
                  {"Address1",address1.text },
                  {"Address2",address2.text },
                  {"City",city.text },
                  {"Mobile","+91"+mobile.text },
                  {"Gender",male.allowSwitchOff},
                  {"Role", "Patient" },
                  {"UserID", SignUp2.registeredUserID },
                  {"TotalCoins",0 }
                //  {"AddNotification",false },
               //  {"RequestedTherapistUID","null" },
               //  {"IsRoutineSessionSet", false },

               //  {"TherapistUID","null" }

              };
        docRef.SetAsync(user).ContinueWithOnMainThread(task =>
        {
            Debug.Log("Paitent Added");
        });

        Debug.Log("Patient UID reg is  " + SignUp2.registeredUserID);

        FirebaseFirestore db2 = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef2 = db2.Collection("users").Document(SignUp2.registeredUserID).Collection("armable").Document("overallProgress");
        Dictionary<string, object> user1 = new Dictionary<string, object>
        {
            {"totalGameSessionsPlayed", 0 },
               {"totalGameDistanceMoved",0 },
               {"totalTimePlayed",0 },
               {"totalRepetitions",0 },
               {"currentSessionNmbr",0 },







        };

        docRef2.SetAsync(user1).ContinueWithOnMainThread(task =>
        {
            Debug.Log("Overall progress document for armable is initialised");


        });

        register = true;
    }


    public void Validation()

    {


        //phone validation
        string _mobile = mobile.text;
        Regex regex = new Regex(@"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$");
        Match match = regex.Match(_mobile);
        if (match.Success)
            Debug.Log(_mobile + " is correct");
        else
            Debug.Log(_mobile + " is incorrect");


        //email validation
        string _email = email.text;
        Regex regex1 = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match1 = regex1.Match(_email);
        if (match1.Success)
            Debug.Log(email + " is correct");
        else
            Debug.Log(email + " is incorrect");

        if(firstName.text!="" && lastName.text!="" && email.text!="" && password.text!="" && address1.text!="" && address2.text!="" && city.text != "")
        {
            if(match.Success && match1.Success)
            {
                allFieldsAreCorrect = true;
            }
            

        }
    }

    public void SignOut()                                            //Function for signOut button
    {
        auth.SignOut();
    }
}

