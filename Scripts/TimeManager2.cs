//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class TimeManager : MonoBehaviour
//{
//    public static float time = 0.00f;
//    public static float bestTime = 0.00f;

//    public Text timeText;
//    public Text bestTimeText;
//    void Start()
//    {
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        time += Time.deltaTime;
//        timeText.text = "Time: " + time.ToString();

//        if(time > bestTime)
//        {
//            bestTime = time;
//            bestTimeText.text = "Best time: " + bestTime.ToString();
//            PlayerPrefs.SetFloat("BestTime", bestTime);
//        }
//    }
//}
