using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    public float timeRemaining = 100;
    public static bool timerIsRunning = false;
    public Text timeText;
    public GameObject Cube;
    public Text liveTimer;
    public Button right, left, up, down;
    public static int FileCounter = 0;
    public string playerPositions;
    public static float DistanceTravelled;
    public Vector3 lastPosition;
    public Camera mainCamera;
    public Text score;
    
    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Landscape;
    }
    private void Start()
    {
        // Starts the timer automatically

         timeRemaining = Dashboard.gameTime * 60;
        //timeRemaining = 5;
        timerIsRunning = true;
        lastPosition = Cube.transform.position;

    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 1)
            {
                float minutes = Mathf.FloorToInt(timeRemaining / 60);
                float seconds = Mathf.FloorToInt(timeRemaining % 60);
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);

               
               

            }
            else
            {

                


                CamCapture();
                


                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                SceneManager.LoadScene("BreakScene");
            }
        }


        Cube.transform.Translate(0, 20 * Input.GetAxis("Vertical") * Time.deltaTime, 0);

        Cube.transform.Translate(20 * Input.GetAxis("Horizontal") * Time.deltaTime, 0, 0);

        right.onClick.AddListener(() => { Cube.transform.Translate(Time.deltaTime, 0, 0); });
        left.onClick.AddListener(() => { Cube.transform.Translate(-Time.deltaTime, 0, 0); });
        up.onClick.AddListener(() => { Cube.transform.Translate(0, Time.deltaTime, 0); });
        down.onClick.AddListener(() => { Cube.transform.Translate(0, -Time.deltaTime, 0); });

        
        playerPositions += Cube.transform.position.x + "," + Cube.transform.position.y + "," + Cube.transform.position.z + "\n";

        DistanceTravelled += Vector3.Distance(Cube.transform.position, lastPosition);
        lastPosition = Cube.transform.position;

    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        liveTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        score.text ="score:"+ (60 - seconds).ToString();
    }

    void OnMouseDown()
    {
        Debug.Log("Hey, tell me what");
    }

    public void OnclickRight()
    {

        Cube.transform.Translate(100 * Time.deltaTime, 0, 0);
    }


    void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.position = objPosition;
    }


    void CamCapture()
    {

        Camera Cam = mainCamera.GetComponent<Camera>();

        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = Cam.targetTexture;
        Cam.backgroundColor = Color.red;
        Cam.Render();

        Texture2D Image = new Texture2D(Cam.targetTexture.width, Cam.targetTexture.height);

        Image.ReadPixels(new Rect(0, 0, Cam.targetTexture.width, Cam.targetTexture.height), 0, 0, false);
        Image.Apply();
        RenderTexture.active = currentRT;

        var Bytes = Image.EncodeToPNG();
        Destroy(Image);
        FileCounter++;
        // File.WriteAllBytes(Application.persistentDataPath + "/area.png", Bytes);
        var path = Application.persistentDataPath + "/" +FileCounter + "area.png";
        //File.WriteAllBytes(Application.persistentDataPath + "/" + DataManagement.PlayerName + Standard.theDate + "_" + DataManagement.gameName + "_" + "area.png", Bytes);
        File.WriteAllBytes(path, Bytes);
        //screenshot_done = true;

        

        mainCamera.enabled = false;
    }

    public void RestartBtn()
    {
        SceneManager.LoadScene("Gamescene");
    }

    public void HomeBtn()
    {
        SceneManager.LoadScene("Dashboard");
    }
}