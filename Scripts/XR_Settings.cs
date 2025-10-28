using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;

public class XR_Settings : MonoBehaviour
{
    public XRManagerSettings managerSettings;

    void Start()
    {
        managerSettings.InitializeLoader();
    }

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {

    }
}
