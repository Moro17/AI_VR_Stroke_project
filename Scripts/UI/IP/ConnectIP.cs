using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using TMPro;

#if WINDOWS_UWP
using Windows.Storage;
#endif

public class ConnectIP : MonoBehaviour
{ 
    ROSConnection m_Ros;

    [SerializeField]
    TextMeshProUGUI m_text;

    string rosIP;

    public string ROSIP{ get => rosIP; set => rosIP = value; }

    // Start is called before the first frame update
    void Start()
    {
        m_Ros = ROSConnection.GetOrCreateInstance();
        rosIP = m_Ros.RosIPAddress;
#if WINDOWS_UWP
        // Get IP address from localSettings
        var localSettings = ApplicationData.Current.LocalSettings;
        if(localSettings.Values["IP"] != null){
            rosIP = localSettings.Values["IP"].ToString();
            m_text.text = rosIP;
        }
#endif
    }

    public void Connect()
    {
        rosIP = m_text.text;
        m_Ros.Disconnect();
        m_Ros.Connect(rosIP, m_Ros.RosPort);

#if WINDOWS_UWP
        // Save IP address to localSettings
        var localSettings = ApplicationData.Current.LocalSettings;
        localSettings.Values["IP"] = rosIP;   
#endif
    }
}
