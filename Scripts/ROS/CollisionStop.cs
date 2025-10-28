//using RosMessageTypes.Actionlib;
//using Unity.Robotics.ROSTCPConnector;
//using UnityEngine;

//public class CollisionStop : MonoBehaviour
//{
//    public string collisionTopic = "/scaled_pos_joint_traj_controller/follow_joint_trajectory/cancel";
//    public JointSubscriber sub;

//    private ROSConnection ros;
//    const string virtualTwinTag = "robot_sim";
//    const string realTwinTag = "robot_real";

//   // public CollisionDialog dialog;
//    public CollisionManager collisionManager;

//    void Start()
//    {
//        ros = ROSConnection.GetOrCreateInstance();
//        ros.RegisterPublisher<GoalIDMsg>(collisionTopic);
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        HandleCollision(other);
//    }

//    private void OnTriggerStay(Collider other)
//    {
//        HandleCollision(other);
//    }

//    private void HandleCollision(Collider other)
//    {
//        if (collisionManager.isCollisionEnabled == false)
//        {
//            //Debug.Log("Collision disabled!");
//            return;
//        }
//        if (other.gameObject.CompareTag(virtualTwinTag))
//        {
//            //Debug.Log("Collision with virtual twin detected. Ignoring.");
//            return;
//        }
//        if (other.gameObject.CompareTag(realTwinTag))
//        {
//            //Debug.Log("Collision with self detected. Ignoring.");
//            return;
//        }
//        CancelTrajectory();
//        Debug.Log("Collision detected with: " + other.gameObject.name);
//       // dialog.SpawnNeutralDialogFromCode();
//        collisionManager.isCollisionEnabled = false;
//    }
//    void CancelTrajectory()
//    {
//        GoalIDMsg cancelMsg = new();

//        ros.Publish(collisionTopic, cancelMsg);

//        Debug.Log("Published cancel message to stop the robot's trajectory.");
//    }
//}
