////using MixedReality.Toolkit.UX;
////using MixedReality.Toolkit.UX;
//using UnityEngine;

//public class ToggleCollectionHandler : MonoBehaviour
//{
//    [Tooltip("The ToggleCollection for open/close actions.")]
//    [SerializeField]
//    private ToggleCollection openCloseToggleCollection;

//    [Tooltip("The ToggleCollection for grasping modes.")]
//    [SerializeField]
//    private ToggleCollection graspingModeToggleCollection;

//    [Tooltip("The GripperControl script to update.")]
//    [SerializeField]
//    private GripperControl gripperControl;

//    private void Start()
//    {
//        if (gripperControl == null)
//        {
//            Debug.LogError("GripperControl reference is not set!");
//            return;
//        }

//        if (openCloseToggleCollection != null)
//        {
//            openCloseToggleCollection.OnToggleSelected.AddListener(OnOpenCloseToggleChanged);
//        }

//        if (graspingModeToggleCollection != null)
//        {
//            graspingModeToggleCollection.OnToggleSelected.AddListener(OnGraspingModeToggleChanged);
//        }
//    }

//    private void OnOpenCloseToggleChanged(int toggleSelectedIndex)
//    {
//        if (toggleSelectedIndex == 0)
//        {
//            // Open
//            gripperControl.SetTargetPosition(0); // Minimum position (open)
//        }
//        else if (toggleSelectedIndex == 1)
//        {
//            // Close
//            gripperControl.SetTargetPosition(255); // Maximum position (close)
//        }
//    }

//    private void OnGraspingModeToggleChanged(int toggleSelectedIndex)
//    {
//        gripperControl.SetGraspingMode((byte)toggleSelectedIndex);
//    }
//}
