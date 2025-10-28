using RosMessageTypes.UrDashboard;
using TMPro;
using Unity.Robotics.ROSTCPConnector;
using UnityEngine;

public class RobotMode : MonoBehaviour
{
    public enum SAFETY_MODE {
        NORMAL = 1,
        REDUCED = 2,
        PROTECTIVE_STOP = 3,
        RECOVERY = 4,
        SAFEGUARD_STOP = 5,
        SYSTEM_EMERGENCY_STOP = 6,
        ROBOT_EMERGENCY_STOP = 7,
        VIOLATION = 8,
        FAULT = 9,
        VALIDATE_JOINT_ID = 10,
        UNDEFINED_SAFETY_MODE = 11,
        AUTOMATIC_MODE_SAFEGUARD_STOP = 12,
        SYSTEM_THREE_POSITION_ENABLING_STOP = 13,
    };

    public enum ROBOT_MODE {
        NO_CONTROLLER = -1,
        DISCONNECTED = 0,
        CONFIRM_SAFETY = 1,
        BOOTING = 2,
        POWER_OFF = 3,
        POWER_ON = 4,
        IDLE = 5,
        BACKDRIVE = 6,
        RUNNING = 7,
        UPDATING_FIRMWARE = 8,
    };

    public TextMeshProUGUI robot;
    public TextMeshProUGUI safety;

    private string safety_mode;
    private string robot_mode;

    // Start is called before the first frame update
    void Start()
    {
        ROSConnection.GetOrCreateInstance().Subscribe<RobotModeMsg>("/ur_hardware_interface/robot_mode", RobotUpdate);
        ROSConnection.GetOrCreateInstance().Subscribe<SafetyModeMsg>("/ur_hardware_interface/safety_mode", SafetyUpdate);
    }

    void RobotUpdate(RobotModeMsg msg)
    {
        robot_mode = ((ROBOT_MODE)msg.mode).ToString();
        robot.text = robot_mode;
    }

    void SafetyUpdate(SafetyModeMsg msg)
    {
        safety_mode = ((SAFETY_MODE)msg.mode).ToString();
        safety.text = safety_mode;
    }
}
