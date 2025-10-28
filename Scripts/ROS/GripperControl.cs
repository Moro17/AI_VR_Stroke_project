using UnityEngine;

public class GripperControl : MonoBehaviour
{
    public GameObject RosManager;

    private JointPublisher gripperPublisher;

    private bool isGripperActivated;
    private byte graspingMode;
    private byte targetPosition;

    void Start()
    {
        // Get all JointPublisher components
        JointPublisher[] publishers = RosManager.GetComponents<JointPublisher>();

        // Find the one controlling the gripper
        foreach (JointPublisher publisher in publishers)
        {
            if (publisher.robotType == JointPublisher.Robot.Gripper)
            {
                gripperPublisher = publisher;
                break;
            }
        }

        if (gripperPublisher == null)
        {
            Debug.LogError("Gripper JointPublisher not found!");
        }
    }

    public void ActivateGripper()
    {
        isGripperActivated = true;
        UpdateGripperState();
    }

    public void DisableGripper()
    {
        isGripperActivated = false;
        UpdateGripperState();
    }

    public void SetTargetPosition(byte position)
    {
        targetPosition = position;
        UpdateGripperState();
    }

    public void SetGraspingMode(byte mode)
    {
        graspingMode = mode;
        UpdateGripperState();
    }

    private void UpdateGripperState()
    {
        if (gripperPublisher == null)
        {
            Debug.LogError("Publisher missing!");
        }

        // Default values for the message fields
        byte rACT = (byte)(isGripperActivated ? 0x1 : 0x0);
        byte rMOD = graspingMode;
        byte rGTO = 0x1; // Go to the requested position
        byte rATR = 0x0; // Stop automatic release
        byte rGLV = 0x0; // Glove mode off
        byte rICF = 0x0; // Normal mode for fingers
        byte rICS = 0x0; // Normal mode for scissor
        byte rPRA = targetPosition; // Target position for fingers
        byte rSPA = 0xFF; // Maximum speed
        byte rFRA = 0xFF; // Maximum force
        byte rPRB = 0x00; // Default position for finger B
        byte rSPB = 0x00; // Minimum speed for finger B
        byte rFRB = 0x00; // Minimum force for finger B
        byte rPRC = 0x00; // Default position for finger C
        byte rSPC = 0x00; // Minimum speed for finger C
        byte rFRC = 0x00; // Minimum force for finger C
        byte rPRS = 0x00; // Default position for scissor
        byte rSPS = 0x00; // Minimum speed for scissor
        byte rFRS = 0x00; // Minimum force for scissor

        // Form the message
        double[] pos = new double[] {
            rACT, rMOD, rGTO, rATR, rGLV, rICF, rICS,
            rPRA, rSPA, rFRA, rPRB, rSPB, rFRB, rPRC, rSPC, rFRC,
            rPRS, rSPS, rFRS
        };

        // Publish the updated state
        gripperPublisher.PublishMessage(pos, 0);
    }
}