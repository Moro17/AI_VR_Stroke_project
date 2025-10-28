//using OVRSimpleJSON;
//using System;
//using System.IO;
//using System.Net;
//using System.Net.Http;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading;
//using TMPro;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UIElements;

//public class ScriptConexiuneMeniu : MonoBehaviour
//{
//    private TcpClient tcpClient;
//    private NetworkStream stream;
//    private Thread receiveThread;

//    public TextMeshProUGUI textToShow;
//    public TextMeshProUGUI distance;
//    public TextMeshProUGUI coords;

//    public GameObject myCube;
//    public GameObject firstSphere;
//    public GameObject secondSphere;

//    public Vector3 lastPos = new Vector3(0, 0, 0);

//    public Camera gameCamera;

//    public int numberOfExercices = 0;

//    // Start is called before the first frame update
//    void Start()
//    {
//        receiveThread = new Thread(new ThreadStart(ReceiveData));
//        receiveThread.IsBackground = true;
//        receiveThread.Start();

//        distance.text = Vector3.Distance(firstSphere.transform.position, myCube.transform.position).ToString();

//        lastPos = myCube.transform.position;

//        if (PlayerPrefs.HasKey("nrOfEx"))
//        {
//            numberOfExercices = PlayerPrefs.GetInt("nrOfEx");
//        }
//    }       

//    void ReceiveData()
//    {
//        tcpClient = new TcpClient();
//        tcpClient.Connect("192.168.0.153", 12322);
//        stream = tcpClient.GetStream();
//        textToShow.text = "Connected to Python server";

//        byte[] buffer = new byte[1024];
//        int bytesRead;

//        int count = 0;

//        while (true)
//        {
//            try
//            {
//                if ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
//                {
//                    string json = Encoding.UTF8.GetString(buffer, 0, bytesRead);
//                    JSONNode obj = JSON.Parse(json);
//                    textToShow.text = obj.ToString();

//                    distance.text = obj["distance"];
//                    Vector3 newPosition = new Vector3(float.Parse(obj["x"]), float.Parse(obj["y"]), float.Parse(obj["z"]));

//                    // get position of the AruCo marker in the game's coordinates system
//                    Matrix4x4 cameraToWorldMatrix;
//                    cameraToWorldMatrix = gameCamera.transform.localToWorldMatrix;
//                    Vector4 transformedPoint = cameraToWorldMatrix * new Vector4(-newPosition.x, newPosition.y, newPosition.z, 1);

//                    firstSphere.transform.position = new Vector3(transformedPoint.x, transformedPoint.y, transformedPoint.z);
//                    count++;

//                    if(count == 40)
//                    {
//                        Thread.Sleep(5000);

//                        Transform sphereTransform = firstSphere.transform;
//                        Transform cubeTransform = myCube.transform;

//                        newPosition = sphereTransform.InverseTransformPoint(cubeTransform.position);

//                        string newCoord = (newPosition.x) + ", " + (newPosition.y) + ", " + newPosition.z + ", ";

//                        byte[] newBuffer = Encoding.ASCII.GetBytes(newCoord);

//                        stream.Write(newBuffer);
//                    }
//                }
//            }
//            catch (System.Exception e)
//            {
//                Debug.LogError(e.ToString());
//                textToShow.text = e.ToString();
//                break;
//            }
//        }
//    }

//    void Update()
//    {
//        if (myCube.transform.position != lastPos && numberOfExercices > 0)
//        {
//            distance.text = Vector3.Distance(firstSphere.transform.position, myCube.transform.position).ToString();

//            Transform sphereTransform = firstSphere.transform;  
//            Transform cubeTransform = myCube.transform;

//            Vector3 newPosition = sphereTransform.InverseTransformPoint(cubeTransform.position);

//            coords.text = "Coords: " + (newPosition.x) + ", " + (newPosition.y ) + ", " + (newPosition.z);

//            string newCoord = newPosition.x + ", " + newPosition.y + ", " + newPosition.z + ", ";

//            byte[] buffer = Encoding.ASCII.GetBytes(newCoord);

//            stream.Write(buffer);

//            lastPos = myCube.transform.position;

//            numberOfExercices--;

//            if (numberOfExercices == 0)
//            {
//                if (receiveThread != null)
//                    receiveThread.Abort();
//                if (stream != null)
//                    stream.Close();
//                if (tcpClient != null)
//                    tcpClient.Close();

//                SceneManager.LoadScene("MenuScene");
//            }
//        }
//    }

//    void OnApplicationQuit()
//    {
//        if (receiveThread != null)
//            receiveThread.Abort();
//        if (stream != null)
//            stream.Close();
//        if (tcpClient != null)
//            tcpClient.Close();
//    }
//}
