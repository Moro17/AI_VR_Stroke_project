using UnityEngine;
using System.Collections.Generic;

public class RobotHome : MonoBehaviour
{
    [Header("Articulatii robot ghost")]
    public List<ArticulationBody> ghostJoints;

    [Header("Articulatii robot real")]
    public List<ArticulationBody> realJoints;

    [Header("Unghiuri pentru pozitia HOME (grade)")]
    public List<float> homeAngles = new List<float> { 25f, -99.4f, 106.4f, -95f, 95f, -78.9f };

    [ContextMenu("Set Home Pose")]
    public void SetHome()
    {
       // SetHome();
        ApplyAnglesToJoints(ghostJoints, "ghost");
        ApplyAnglesToJoints(realJoints, "real");
    }

    private void ApplyAnglesToJoints(List<ArticulationBody> joints, string label)
    {
        if (joints == null || joints.Count != homeAngles.Count)
        {
            Debug.LogWarning($"Lista de articulații pentru {label} nu e completă!");
            return;
        }

        for (int i = 0; i < joints.Count; i++)
        {
            var joint = joints[i];
            var drive = joint.xDrive;
            drive.target = homeAngles[i];
            joint.xDrive = drive;
        }

        Debug.Log($"✅ Setat poziția HOME pe robotul {label}.");
    }
}
