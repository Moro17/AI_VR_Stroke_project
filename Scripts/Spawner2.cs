using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Management;
using UnityEngine.XR;
using System;

public class Spawner2 : MonoBehaviour
{
    // Camera
    public Camera camera;
    public Text distanceC2OText;
    public Text distanceH2OText;
    public float distanceC2O = 0.00f;
    public float distanceH2O = 0.00f;

    public GameObject myCube;
    public bool firstTouch = false;
    public Text username;

    // Score
    public static int score = 0;
    public static int highScore = 0;
    public Text scoreText;
    public Text highScoreText;

    // Time
    public static float time = 0.00f;
    public static float highScoreTime = 0.00f;
    public Text timeText;
    public Text highScoreTimeText;
    public bool timerStarted = false;

    public void Start()
    {
        //Recenter();

        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreTime = PlayerPrefs.GetFloat("BestTime");

        highScoreText.text = "HighScore: " + highScore;
        highScoreTimeText.text = "Highscore time: " + highScoreTime.ToString("0.00");

        username.text = PlayerPrefs.GetString("username");

        Debug.Log("Test");

        GetTransformationMatrix();
    }

    public void MoveCube()
    {
        if (!firstTouch)
        {
            firstTouch = true;
            timerStarted = true;
        }

        float spawnPointX = UnityEngine.Random.Range(-0.3f, 0.3f);
        float spawnPointY = UnityEngine.Random.Range(0.85f, 1.4f);

        Vector3 spawnPosition = new Vector3(spawnPointX, spawnPointY, 0.5f);

        var newDistance = Vector3.Distance(spawnPosition, myCube.transform.position);

        while(newDistance < 0.2)
        {
            spawnPointX = UnityEngine.Random.Range(-0.3f, 0.3f);
            spawnPointY = UnityEngine.Random.Range(0.8f, 1.4f);

            spawnPosition = new Vector3(spawnPointX, spawnPointY, 0.5f);
            newDistance = Vector3.Distance(spawnPosition, myCube.transform.position);
        }

        myCube.transform.position = spawnPosition;

        score++;

        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = "HighScore: " + highScore;

            PlayerPrefs.SetInt("HighScore", highScore);
        }

        scoreText.text = "Score: " + score;
    }

    public void Update()
    {
        var cameraReference = camera.GetComponent<Camera>();

        distanceC2O = Vector3.Distance(cameraReference.transform.position, myCube.transform.position);
        distanceC2OText.text = "DistanceC2O: " + distanceC2O.ToString("0.00");

        Vector3 rightControllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);

        distanceH2O = Vector3.Distance(rightControllerPosition, myCube.transform.position);
        distanceH2OText.text = "DistanceH2O: " + distanceH2O.ToString("0.00");

        if (timerStarted)
        {
            time += Time.deltaTime;
            timeText.text = "Time: " + time.ToString("0.00");

            // Update HighScore Time only if the HighScore has been beaten
            if (score > highScore)
            {
                highScoreTime = time;
                highScoreTimeText.text = "Highscore time: " + highScoreTime.ToString("0.00");

                PlayerPrefs.SetFloat("BestTime", highScoreTime);
            }
        }
    }

    public void Recenter()
    {
        var xrSettings = XRGeneralSettings.Instance;
        if (xrSettings == null)
        {
            Debug.Log($"XRGeneralSettings is null.");
            return;
        }

        var xrManager = xrSettings.Manager;
        if (xrManager == null)
        {
            Debug.Log($"XRManagerSettings is null.");
            return;
        }

        var xrLoader = xrManager.activeLoader;
        if (xrLoader == null)
        {
            Debug.Log($"XRLoader is null.");
            return;
        }

        Debug.Log($"Loaded XR Device: {xrLoader.name}");

        var xrDisplay = xrLoader.GetLoadedSubsystem<XRDisplaySubsystem>();
        Debug.Log($"XRDisplay: {xrDisplay != null}");

        if (xrDisplay != null)
        {
            if (xrDisplay.TryGetDisplayRefreshRate(out float refreshRate))
            {
                Debug.Log($"Refresh Rate: {refreshRate}hz");
            }
        }

        var xrInput = xrLoader.GetLoadedSubsystem<XRInputSubsystem>();
        Debug.Log($"XRInput: {xrInput != null}");

        if (xrInput != null)
        {
            xrInput.TrySetTrackingOriginMode(TrackingOriginModeFlags.Device);
            xrInput.TryRecenter();
        }
    }

    public void GetTransformationMatrix()
    {
        var matrix = transform.localToWorldMatrix;
        Debug.Log(matrix);

        Console.WriteLine("test");
    }
}
