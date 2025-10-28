//using UnityEngine;

//public class PoseLoader : MonoBehaviour
//{
//    public Transform robotRoot;   // Drag Workspace aici
//    public Transform arucoMarker; // Drag ARUCO markerul din scenă

//    public string poseFilePath = "C:/Users/Moro/Downloads/ZED/pose.txt"; // modificabil în inspector

//    [ContextMenu("Load Pose from File")]
//    public void LoadPoseFromFile()
//    {
//        if (!System.IO.File.Exists(poseFilePath))
//        {
//            Debug.LogError("pose.txt not found at: " + poseFilePath);
//            return;
//        }

//        string[] lines = System.IO.File.ReadAllLines(poseFilePath);
//        if (lines.Length < 2) return;

//        string[] posTokens = lines[0].Split(',');
//        string[] rotTokens = lines[1].Split(',');

//        float x = float.Parse(posTokens[0]);
//        float y = float.Parse(posTokens[1]);
//        float z = float.Parse(posTokens[2]);

//        float rx = float.Parse(rotTokens[0]);
//        float ry = float.Parse(rotTokens[1]);
//        float rz = float.Parse(rotTokens[2]);

//        Vector3 unityPosition = new Vector3(x, robotRoot.position.y, z);
//        Quaternion unityRotation = Quaternion.Euler(rx, ry, rz);

//        // mută markerul în poziție
//        if (arucoMarker != null)
//        {
//            arucoMarker.position = unityPosition;
//            arucoMarker.rotation = unityRotation;
//        }

//        // repoziționează workspace-ul
//        if (robotRoot != null)
//        {
//            robotRoot.position = unityPosition;
//            robotRoot.rotation = unityRotation;
//        }

//        Debug.Log("Pose loaded and applied.");
//        Debug.Log("Set position to: " + unityPosition.ToString("F3"));
//        Debug.Log("Set rotation to: " + unityRotation.eulerAngles.ToString("F3"));

//    }
//}
