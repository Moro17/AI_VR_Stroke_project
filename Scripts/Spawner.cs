using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Management;
using TMPro;


public class Spawner : MonoBehaviour
{
    // Camera
    public Camera camera;
    public TextMeshProUGUI distanceC2OText;
    public TextMeshProUGUI distanceH2OText;
    public float distanceC2O = 0.00f;
    public float distanceH2O = 0.00f;

    public GameObject myCube;
    public bool firstTouch = false;
    public TextMeshProUGUI username;

    //hand stuff
    public GameObject rightHand;
    public GameObject leftHand;

    // Score
    public static int score = 0;
    public static int highScore = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    // Time
    public static float time = 0.00f;
    public static float highScoreTime = 0.00f;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI highScoreTimeText;
    public bool timerStarted = false;


    public PoseGoalSender poseSender;

    public void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreTime = PlayerPrefs.GetFloat("BestTime");

        highScoreText.text = "HighScore: " + highScore;
        highScoreTimeText.text = "Highscore time: " + highScoreTime.ToString("0.00");

        username.text = PlayerPrefs.GetString("username");
    }

    public void MoveCube()
    {
        Debug.Log("Cube was touched");
        if (!firstTouch)
        {
            firstTouch = true;
            timerStarted = true;
        }

        float spawnPointX = UnityEngine.Random.Range(-0.4f, 0.1f);
        float spawnPointY = UnityEngine.Random.Range(1.0f, 1.2f);

        Vector3 spawnPosition = new Vector3(spawnPointX, spawnPointY, -0.59f);

        var newDistance = Vector3.Distance(spawnPosition, myCube.transform.position);

        while (newDistance < 0.35)
        {
            spawnPointX = UnityEngine.Random.Range(-0.2f, 0.3f);
            spawnPointY = UnityEngine.Random.Range(1.05f, 1.2f);

            spawnPosition = new Vector3(spawnPointX, spawnPointY, -0.59f);
            newDistance = Vector3.Distance(spawnPosition, myCube.transform.position);
        }

        Debug.Log("New position: " + spawnPosition.ToString());
        myCube.transform.position = spawnPosition;

        score++;

        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = "HighScore: " + highScore;

            PlayerPrefs.SetInt("HighScore", highScore);
        }

        scoreText.text = "Score: " + score;

        if (poseSender != null)
        {
            poseSender.targetObject = myCube;
            poseSender.SendPose();
        }

    }

    public void Update()
    {
        var cameraReference = camera.GetComponent<Camera>();

        distanceC2O = Vector3.Distance(cameraReference.transform.position, myCube.transform.position);
        distanceC2OText.text = "DistanceC2O: " + distanceC2O.ToString("0.00");

        distanceH2O = Vector3.Distance(rightHand.transform.position, myCube.transform.position);
        distanceH2OText.text = "DistanceH2O: " + distanceH2O.ToString("0.00");

        if (timerStarted)
        {
            time += Time.deltaTime;
            timeText.text = "Time: " + time.ToString("0.00");

            // Update HighScore Time only if the HighScore has been beaten
            if (score >= highScore)
            {
                highScoreTime = time;
                highScoreTimeText.text = "Highscore time: " + highScoreTime.ToString("0.00");

                PlayerPrefs.SetFloat("BestTime", highScoreTime);
            }
        }
    }
}
