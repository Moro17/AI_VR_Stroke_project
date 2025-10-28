using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleIKChain : MonoBehaviour
{
    [Header("Articulatii & Axele lor")]
    public List<ArticulationBody> joints;
    public List<Vector3> localAxes;

    [Header("Urmarire tinta")]
    public Transform handToFollow;         // transformul mainii
    public Transform target;               // buffer care va urmari mana

    [Header("Control")]
    public float stepSize = 1f;
    public float stopDistance = 0.01f;
    public int maxIterations = 30;
    public float ikStartDelay = 2.0f;
    public Transform endEffector;

    [Header("Optional: Ghost visual root")]
    public GameObject ghostVisualRoot;

    private bool follow = false;
   // private bool lockedOn = false; // nou
    void Start()
    {
        StartCoroutine(DelayedIKStart());
    }

    IEnumerator DelayedIKStart()
    {
        yield return new WaitForSeconds(ikStartDelay);

        target.position = handToFollow.position;
        target.rotation = handToFollow.rotation;
        follow = true;

        if (ghostVisualRoot != null)
        {
            ghostVisualRoot.SetActive(false);
            Debug.Log("👻 Ghost robot vizual a fost ascuns.");
        }
    }


    void FixedUpdate()
    {
        Debug.DrawLine(GetEndEffectorPosition(), target.position, Color.green);

        if (!follow || joints == null || joints.Count == 0 || target == null || localAxes.Count != joints.Count)
            return;

        // Update pozitie tinta in fiecare frame
        target.position = handToFollow.position;
        target.rotation = handToFollow.rotation;
        //if (lockedOn)
        //    return;
        for (int iter = 0; iter < maxIterations; iter++)
        {
            for (int i = joints.Count - 1; i >= 0; i--)
            {
                ArticulationBody joint = joints[i];
                Transform jt = joint.transform;

                Vector3 toEnd = GetEndEffectorPosition() - jt.position;
                Vector3 toTarget = target.position - jt.position;

                Vector3 axis = jt.TransformDirection(localAxes[i]);

                Vector3 projectedToEnd = Vector3.ProjectOnPlane(toEnd, axis);
                Vector3 projectedToTarget = Vector3.ProjectOnPlane(toTarget, axis);

                float angle = Vector3.SignedAngle(projectedToEnd, projectedToTarget, axis);
                float delta = Mathf.Clamp(angle, -stepSize, stepSize);

                if (Mathf.Abs(delta) > 0.01f)
                {
                    var drive = joint.xDrive;
                    drive.target += delta;
                    drive.target = Mathf.Clamp(drive.target, drive.lowerLimit, drive.upperLimit);
                    joint.xDrive = drive;
                }

                if (Vector3.Distance(GetEndEffectorPosition(), target.position) < stopDistance)
                {
                    Debug.Log("🔒 Robotul s-a fixat pe mână.");
                 //   lockedOn = true; // NU mai continuăm IK
                    return;
                }
            }
        }
    }

    Vector3 GetEndEffectorPosition()
    {
        return endEffector != null ? endEffector.position : joints[joints.Count - 1].transform.position;
    }

}
