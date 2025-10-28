//using UnityEngine;

//public class MoveWorkspaceTest : MonoBehaviour
//{
//    void Start()
//    {
//        Debug.Log("🚀 Test: Mut Workspace la (2, 0, 2)");

//        Vector3 targetPosition = new Vector3(2, 0, 2);
//        Quaternion targetRotation = Quaternion.Euler(0, 90, 0);

//        Rigidbody rb = GetComponent<Rigidbody>();
//        if (rb != null && !rb.isKinematic)
//        {
//            rb.MovePosition(targetPosition);
//            rb.MoveRotation(targetRotation);
//            Debug.Log("📦 Mutat cu Rigidbody");
//        }
//        else
//        {
//            transform.position = targetPosition;
//            transform.rotation = targetRotation;
//            Debug.Log("📦 Mutat cu transform");
//        }
//    }
//}
