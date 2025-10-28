using System.Collections.Generic;
using UnityEngine;

public class GripperControllerSim : MonoBehaviour
{
    public List<ArticulationBody> fingerJoints;

    // Seturi de targete în grade pentru fiecare mod
    private readonly Dictionary<string, float[]> graspPresets = new Dictionary<string, float[]>
{
    { "Basic",   new float[] { 30,30,30,30, 30,30,30, 30,30,30,30 } },
    { "Pinch",   new float[] {  0, 0, 0, 0, 60,60,60,  0, 0, 0, 0 } },
    { "Wide",    new float[] { 15,15,15,15, 15,15,15, 15,15,15,15 } },
    { "Scissor", new float[] {  0,60,0,60,  0,60,0,  60,0,60,0 } }
};



    public void ApplyGrasp(string mode)
    {
        if (!graspPresets.ContainsKey(mode) || fingerJoints.Count != graspPresets[mode].Length)
        {
            Debug.LogWarning("❌ GripperSim: mod invalid sau număr articulații nepotrivit.");
            return;
        }

        float[] targets = graspPresets[mode];

        for (int i = 0; i < fingerJoints.Count; i++)
        {
            var drive = fingerJoints[i].xDrive;
            drive.target = targets[i];
            fingerJoints[i].xDrive = drive;
        }

        Debug.Log($"🤖 GripperSim: setat mod {mode}");
    }

    //[ContextMenu("TEST_CLOSE")]
    //public void TEST_CLOSE()
    //{
    //    Debug.Log("🧪 Executăm TEST_CLOSE pe toate articulațiile...");

    //    foreach (var joint in fingerJoints)
    //    {
    //        var drive = joint.xDrive;
    //        drive.target = 40f;
    //        joint.xDrive = drive;
    //    }
    //}

}
