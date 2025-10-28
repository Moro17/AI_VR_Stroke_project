using System.Collections.Generic;
using UnityEngine;

public class GhostToRealMirror : MonoBehaviour
{
    public DigitalTwinControl ghost;
    public List<ArticulationBody> realJoints;

    [Header("Sync Motion Settings")]
    public float jointSpeed = 180f;
    public float maxStep = 2f;

    private float[] jointTargets;
    private bool synced = false;

    public void SyncFromGhost()
    {
        if (ghost == null || realJoints.Count != ghost.GetJointCount()) return;

        jointTargets = ghost.GetCurrentJointTargets();
        synced = true;

        Debug.Log("✅ Robotul real a primit targetele din fantomă (cu Joint Speed).");
    }

    void FixedUpdate()
    {
        if (!synced || jointTargets == null || jointTargets.Length != realJoints.Count)
            return;

        for (int i = 0; i < realJoints.Count; i++)
        {
            var joint = realJoints[i];
            var drive = joint.xDrive;

            float current = drive.target;
            float desired = jointTargets[i];
            float diff = desired - current;

            if (Mathf.Abs(diff) > maxStep)
                current += Mathf.Sign(diff) * maxStep;
            else
                current = desired;

            drive.target = current;
            joint.xDrive = drive;
        }
    }
}
