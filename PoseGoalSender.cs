using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Geometry;

public class PoseGoalSender : MonoBehaviour
{
    [Header("Obiectul către care să se ducă robotul")]
    public GameObject targetObject;

    [Header(" Configurare ROS")]
    public string topicName = "/pose_goal";
    public Vector3 offset = new Vector3(0f, 0f, -0.1f); 

    private ROSConnection ros;

    void Start()
    {
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<PoseStampedMsg>(topicName);
    }

    public void SendPose()
    {
        if (targetObject == null)
        {
            Debug.LogWarning("Target object nu e setat!");
            return;
        }

        Vector3 targetPos = targetObject.transform.position + targetObject.transform.rotation * offset;
        Quaternion targetRot = targetObject.transform.rotation;

        PoseStampedMsg pose = new PoseStampedMsg
        {
            header = new RosMessageTypes.Std.HeaderMsg
            {
                frame_id = "base_link"  
            },
            pose = new PoseMsg
            {
                position = new PointMsg(targetPos.x, targetPos.y, targetPos.z),
                orientation = new QuaternionMsg(targetRot.x, targetRot.y, targetRot.z, targetRot.w)
            }
        };

        ros.Publish(topicName, pose);
        Debug.Log("📬 Pose goal trimis: " + targetPos.ToString("F3"));
    }
}
