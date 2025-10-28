using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutoAxisDetector : MonoBehaviour
{
    [System.Serializable]
    public class JointData
    {
        public ArticulationBody joint;
        public Vector3 detectedAxis;
    }

    public List<JointData> joints = new List<JointData>();
    public float testAngle = 10f;
    public float waitTime = 0.1f;

    [ContextMenu("Detect All Joint Axes")]
    public void StartDetection()
    {
        StartCoroutine(DetectAxesRoutine());
    }

    IEnumerator DetectAxesRoutine()
    {
        foreach (var data in joints)
        {
            if (data.joint == null) continue;

            var joint = data.joint;
            var drive = joint.xDrive;
            float originalTarget = drive.target;

            Vector3 before = joint.transform.localEulerAngles;

            drive.target += testAngle;
            joint.xDrive = drive;

            yield return new WaitForSeconds(waitTime);

            Vector3 after = joint.transform.localEulerAngles;
            Vector3 delta = after - before;

            Vector3 axis = Vector3.zero;
            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y) && Mathf.Abs(delta.x) > Mathf.Abs(delta.z)) axis = Vector3.right;
            else if (Mathf.Abs(delta.y) > Mathf.Abs(delta.z)) axis = Vector3.up;
            else axis = Vector3.forward;

            data.detectedAxis = axis;
            Debug.Log($"{joint.name} → Axis: {axis}");

            // Reset
            drive.target = originalTarget;
            joint.xDrive = drive;

            yield return new WaitForSeconds(waitTime);
        }
    }
}
