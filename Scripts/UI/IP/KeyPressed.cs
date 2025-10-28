//using System.Linq;
//using TMPro;
//using Unity.Robotics.ROSTCPConnector;
//using UnityEngine;

//public class KeyPressed : MonoBehaviour
//{
//    string ip;
//    int maxLength = 15;
//    int nrDots = 0;

//    [SerializeField]
//    TextMeshProUGUI textMeshPro;

//    ROSConnection m_Ros;

//    // Start is called before the first frame update
//    void Start()
//    {
//        m_Ros = ROSConnection.GetOrCreateInstance();
//        ip = m_Ros.RosIPAddress;
//        nrDots = ip.Count(c => c == '.');
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        textMeshPro.text = ip;
//        // If someone inputs a new ip address and presses enter, check if it connects successfully
//        if (m_Ros.HasConnectionThread && !m_Ros.HasConnectionError)
//        {
//            ConnectSuccess();
//        }
//        else if (m_Ros.HasConnectionError)
//        {
//            ConnectFail();
//        }
//    }

//    public void ConnectFail()
//    {
//        textMeshPro.color = Color.red;
//    }

//    public void ConnectSuccess()
//    {
//        textMeshPro.color = Color.green;
//    }

//    void ProcessKey(string key)
//    {
//        if (key != "DEL" && ip.Length > maxLength)
//        {
//            return;
//        }

//        switch (key)
//        {
//            case "DEL":
//                if(ip.Length > 0)
//                {
//                    if (ip[^1] == '.')
//                    {
//                        nrDots--;
//                    }
//                    ip = ip[..^1];
//                }
//                break;
//            case ".":
//                if (CanAddDot())
//                {
//                    ip += ".";
//                    nrDots++;
//                }
//                break;
//            default:
//                if (CanAddDigit(key))
//                {
//                    ip += key;
//                }
//                break;
//        }
//    }

//    bool CanAddDot()
//    {
//        if (ip.Length == 0 || ip[^1] == '.' || nrDots >= 3)
//        {
//            return false;
//        }

//        // Check if the last segment is complete (i.e., not too long and not starting with zero if more than 1 digit)
//        var lastSegment = ip.Split('.').Last();
//        if (lastSegment.StartsWith("0") && lastSegment.Length > 1)
//        {
//            return false;
//        }

//        return true;
//    }
    
//    bool CanAddDigit(string digit)
//    {
//        if (ip.Length == 0 || ip[^1] == '.')
//        {
//            // Starting a new segment
//            return true;
//        }

//        // Check if adding the digit exceeds the segment length of 3
//        var lastSegment = ip.Split('.').Last();
//        if (lastSegment.Length >= 3)
//        {
//            return false;
//        }

//        // Construct potential new segment to check its numeric value
//        var newSegment = int.Parse(lastSegment + digit);
//        if (newSegment > 255)
//        {
//            return false;
//        }

//        // Prevent leading zeros
//        if (lastSegment == "0")
//        {
//            return false;
//        }

//        return true;
//    }

//    #region Key pressed events
//    public void Key1()
//    {
//        ProcessKey("1");
//    }

//    public void Key2()
//    {
//        ProcessKey("2");
//    }

//    public void Key3()
//    {
//        ProcessKey("3");
//    }

//    public void Key4()
//    {
//        ProcessKey("4");
//    }

//    public void Key5()
//    {
//        ProcessKey("5");
//    }

//    public void Key6()
//    {
//        ProcessKey("6");
//    }

//    public void Key7()
//    {
//        ProcessKey("7");
//    }

//    public void Key8()
//    {
//        ProcessKey("8");
//    }

//    public void Key9()
//    {
//        ProcessKey("9");
//    }

//    public void Key0()
//    {
//        ProcessKey("0");
//    }

//    public void KeyDel()
//    {
//        ProcessKey("DEL");
//    }

//    public void KeyDot()
//    {
//        ProcessKey(".");
//    }
//    #endregion
//}
