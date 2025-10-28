using UnityEngine;
using System.Collections.Generic;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Sensor;
using System;
using System.IO;

public class JointSubscriber : MonoBehaviour
{
    public List<ArticulationBody> Joints;
    public string topic;

    private readonly float targetThreshold = 0.001f;
    [SerializeField] 
    private float deltaTime = 0.1f;
    private float lastTime = 0;

    public bool msgSent = false;
    public System.DateTime timeStamp;
    private string logFilePath;

    void Start()
    {
        ROSConnection.GetOrCreateInstance().Subscribe<JointStateMsg>(topic, JointUpdate);
        lastTime = Time.time;

        // Set up the log file path
        logFilePath = Path.Combine(Application.persistentDataPath, "robot_timestamps.log");

        // Create or clear the log file at the start
        using StreamWriter writer = new(logFilePath, false);
        writer.WriteLine("Command Sent Time, Response Received Time, Time Taken (seconds.milliseconds)");
    }

    void JointUpdate(JointStateMsg msg)
    {
        if(msgSent)
        {
            if (Mathf.Abs(Joints[2].xDrive.target - (float)(msg.position[2])*Mathf.Rad2Deg) > 0.01f)
            {
                TimeSpan timeDifference = DateTime.Now - timeStamp;
                string logMessage = $"{timeDifference.Seconds}.{timeDifference.Milliseconds}";
                Debug.Log(logMessage);
                msgSent = false;
                using StreamWriter writer = new(logFilePath, true);
                writer.WriteLine(logMessage);
            }
        }
        if (Mathf.Abs(Time.time - lastTime) < deltaTime)
            return;
        for (int i = 0; i < msg.name.Length; i++)
        {
            var jointXDrive = Joints[i].xDrive;
            var nextTarget = (float)(msg.position[i]) * Mathf.Rad2Deg;

            if (Mathf.Abs(jointXDrive.target - nextTarget) > targetThreshold)
            {
                jointXDrive.target = nextTarget;
                // jointXDrive.targetVelocity = (float)(msg.velocity[i]);
                Joints[i].xDrive = jointXDrive;
            }
        }
        lastTime = Time.time;
    }
}
