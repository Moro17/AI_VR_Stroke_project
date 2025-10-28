//using System.Collections;
//using UnityEngine;

//public class AvatarPositionController : MonoBehaviour
//{
//    public Transform mainCamera;
//    public ZEDBodyTrackingManager zedBodyTrackingManager;

//    public string skeletonObjectNamePrefix = "Skeleton_ID_";

//    public float updateInterval = 5f; // Update interval in seconds

//    private Transform headJoint;
//    private Vector3 initialOffset;

//    // Start is called before the first frame update
//    void Start()
//    {
//        // Find the head joint after a brief delay to ensure the avatar is spawned
//        Invoke(nameof(FindHeadJoint), 2f);

//        // Store the initial offset
//        if (zedBodyTrackingManager != null)
//        {
//            if (zedBodyTrackingManager != null)
//            {
//                initialOffset = zedBodyTrackingManager.manualOffset;
//            }
//        }

//        // Start the coroutine to update the offset
//        StartCoroutine(UpdateOffsetCoroutine());
//    }

//    // Coroutine to update the offset at regular intervals
//    IEnumerator UpdateOffsetCoroutine()
//    {
//        while (true)
//        {
//            yield return new WaitForSeconds(updateInterval);

//            // Update the offset
//            UpdateOffset();
//        }
//    }

//    // Function to update the offset
//    void UpdateOffset()
//    {
//        if (headJoint == null)
//        {
//            FindHeadJoint();
//        }

//        // Check if both camera and head joint are available
//        if (mainCamera != null && headJoint != null)
//        {
//            // Extract the transform of the head joint
//            Transform jointTransform = headJoint.transform;

//            // Revert the previous offset applied to the joint
//            Vector3 revertedOffset = -zedBodyTrackingManager.manualOffset;
//            Vector3 newPosition = jointTransform.position + revertedOffset;

//            // Calculate the new offset required to move the joint onto the camera
//            Vector3 newOffset = mainCamera.position - newPosition;

//            // Apply offset to the ZED body tracked manager's manualOffset
//            if (zedBodyTrackingManager != null)
//            {
//                zedBodyTrackingManager.manualOffset = newOffset;
//            }
//        }
//    }

//    // Function to find the head joint dynamically
//    void FindHeadJoint()
//    {
//        // Find all GameObjects in the scene
//        GameObject[] rootObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();

//        // Iterate through each GameObject to find the head joint
//        foreach (GameObject rootObject in rootObjects)
//        {
//            // Check if the GameObject represents a skeleton
//            if (rootObject.name.StartsWith(skeletonObjectNamePrefix))
//            {
//                // Check if the skeleton GameObject contains a child named "5" (assuming it represents the head joint)
//                Transform joint = rootObject.transform.Find("5");
//                if (joint != null)
//                {
//                    headJoint = joint;
//                    break;
//                }
//            }
//        }
//    }
//}
