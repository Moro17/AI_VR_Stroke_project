using UnityEngine;
using System.IO;

public class AlignWorkspaceFromPose : MonoBehaviour
{
    public Transform workspaceRoot;   // Workspace complet: robot, cabină, etc.
    public Transform arucoMarker;     // Marker virtual din scenă
    public string poseFilePath = "C:/Users/Moro/Downloads/ZED/pose.txt";

    [ContextMenu("Align Workspace From pose.txt")]
    public void AlignWorkspace()
    {
        if (!File.Exists(poseFilePath))
        {
            Debug.LogError("pose.txt not found at: " + poseFilePath);
            return;
        }

        string[] lines = File.ReadAllLines(poseFilePath);
        if (lines.Length < 2) return;

        string[] posTokens = lines[0].Split(',');
        string[] rotTokens = lines[1].Split(',');

        Vector3 detectedMarkerPos = new Vector3(
            float.Parse(posTokens[0]),
            float.Parse(posTokens[1]),
            float.Parse(posTokens[2])
        );

        float detectedYaw = float.Parse(rotTokens[1]);
        Quaternion detectedRotY = Quaternion.Euler(0, detectedYaw, 0);

        // Calculăm delta între markerul fizic detectat și cel virtual din scenă
        Vector3 virtualMarkerPos = arucoMarker.position;
        Quaternion virtualMarkerRot = Quaternion.Euler(0, arucoMarker.eulerAngles.y, 0);

        Vector3 offset = virtualMarkerPos - detectedMarkerPos;
        float deltaYaw = arucoMarker.eulerAngles.y - detectedYaw;

        workspaceRoot.position += offset;
        workspaceRoot.rotation = Quaternion.Euler(0, workspaceRoot.rotation.eulerAngles.y + deltaYaw, 0);

        Debug.Log("Workspace aliniat cu poziția reală.");
    }
}
