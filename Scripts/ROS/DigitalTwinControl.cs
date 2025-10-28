using System;
using System.Collections.Generic;
using Unity.Robotics.ROSTCPConnector;
using UnityEngine;

public class DigitalTwinControl : MonoBehaviour
{
    [SerializeField] float maxStep;
    [SerializeField] List<ArticulationBody> joints;
    [SerializeField] float jointSpeed;

    private JointPublisher jointPublisher;
    private JointSubscriber jointSubscriber;

    private float[] jointTargets;
    private bool synced = false;
    private ROSConnection m_Ros;

    void Start()
    {
        Application.runInBackground = true;
        jointPublisher = GetComponent<JointPublisher>();
        jointSubscriber = GetComponent<JointSubscriber>();
        m_Ros = ROSConnection.GetOrCreateInstance();

        for (int i = 0; i < joints.Count; i++)
        {
            var urdfInertial = joints[i].GetComponent<Unity.Robotics.UrdfImporter.UrdfInertial>();
            urdfInertial.useUrdfData = false;
            joints[i].automaticCenterOfMass = true;
            joints[i].automaticInertiaTensor = true;
        }

        jointTargets = new float[joints.Count];
    }
    //UI
    public void SetExternalJointTargets(float[] targets)
    {
        if (targets.Length != joints.Count)
        {
            Debug.LogWarning("Received invalid joint targets.");
            return;
        }

        for (int i = 0; i < targets.Length; i++)
        {
            jointTargets[i] = targets[i];
        }

        synced = true;
    }
    //public void SetExternalJointTargets(float[] targets)
    //{
    //    Debug.Log("📡 [DigitalTwin] Targete primite: " + string.Join(", ", targets));

    //    if (targets.Length != joints.Count)
    //    {
    //        Debug.LogWarning("⚠️ [DigitalTwin] Numărul de targete nu corespunde articulațiilor.");
    //        return;
    //    }

    //    for (int i = 0; i < targets.Length; i++)
    //    {
    //        jointTargets[i] = targets[i];
    //        Debug.Log($"👉 Set joint[{i}] target = {targets[i]}");
    //    }

    //    synced = true;
    //}

    public void PublishCurrentPosition()
    {
        if (m_Ros.HasConnectionError)
        {
            Debug.LogWarning("No ROS connection, only moving digital twin.");
            return;
        }

        double[] targetPositions = new double[jointTargets.Length];
        float maxTime = 0;

        for (int i = 0; i < jointTargets.Length; i++)
        {
            targetPositions[i] = jointTargets[i] * Mathf.Deg2Rad;

            float distance = Mathf.Abs(jointTargets[i] - jointSubscriber.Joints[i].xDrive.target);
            float time = distance / jointSpeed;
            if (time > maxTime)
                maxTime = time;
        }

        int timeInSeconds = Mathf.CeilToInt(maxTime);
        jointPublisher.PublishMessage(targetPositions, timeInSeconds);
        jointSubscriber.msgSent = true;
        jointSubscriber.timeStamp = DateTime.Now;
    }


    void FixedUpdate()
    {
        if (!synced) return;

        for (int i = 0; i < joints.Count; i++)
        {
            var jointXDrive = joints[i].xDrive;
            float currentTarget = jointXDrive.target;
            float desired = jointTargets[i];
            float diff = desired - currentTarget;

            if (Mathf.Abs(diff) > maxStep)
                currentTarget += Mathf.Sign(diff) * maxStep;
            else
                currentTarget = desired;

            jointXDrive.target = currentTarget;
            joints[i].xDrive = jointXDrive;
        }
    }
    public float[] GetCurrentJointTargets()
    {
        float[] current = new float[joints.Count];
        for (int i = 0; i < joints.Count; i++)
        {
            current[i] = joints[i].xDrive.target;
        }
        return current;
    }

    public int GetJointCount()
    {
        return joints.Count;
    }



}
