using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Control;
using RosMessageTypes.Trajectory;
using RosMessageTypes.Robotiq3fGripperArticulated;

public class JointPublisher : MonoBehaviour
{
    public enum Robot
    {
        UR5e,
        Gripper
    }

    public Robot robotType;
    string k_Topic;

    ROSConnection m_Ros;

    // Start is called before the first frame update
    void Start()
    {
        m_Ros = ROSConnection.GetOrCreateInstance();
        if(robotType == Robot.UR5e)
        {
            k_Topic = "/scaled_pos_joint_traj_controller/follow_joint_trajectory/goal";
            m_Ros.RegisterPublisher<FollowJointTrajectoryActionGoalMsg>(k_Topic);
        }
        else
        {
            k_Topic = "/Robotiq3FGripperRobotOutput";
            m_Ros.RegisterPublisher<Robotiq3FGripperRobotOutputMsg>(k_Topic);
        }
    }

    public void PublishMessage(double[] pos, int time)
    {
        if (robotType == Robot.UR5e)
        {
            m_Ros.Publish(k_Topic, InitializeMessageUR(pos, time));
        }
        else
        {
            m_Ros.Publish(k_Topic, InitializeMessageGripper(pos));
        }
    }

    private FollowJointTrajectoryActionGoalMsg InitializeMessageUR(double[] pos, int time)
    {
        return 
            new FollowJointTrajectoryActionGoalMsg
            {
                goal = new FollowJointTrajectoryGoalMsg()
                {
                    trajectory = new JointTrajectoryMsg()
                    {
                        header = new RosMessageTypes.Std.HeaderMsg(),
                        joint_names = new string[6]
                        {
                            "shoulder_pan_joint",
                            "shoulder_lift_joint",
                            "elbow_joint",
                            "wrist_1_joint",
                            "wrist_2_joint",
                            "wrist_3_joint"
                        },
                        points = new JointTrajectoryPointMsg[1]
                        {
                            new()
                            {
                                positions = pos,
                                velocities = new double[6]{ 0,0,0,0,0,0 },
                                accelerations = new double[0],
                                effort = new double[0],
                                time_from_start = new RosMessageTypes.BuiltinInterfaces.DurationMsg(){sec=time,nanosec=0}
                            }
                        }
                    },
                    path_tolerance = new JointToleranceMsg[1]
                    {
                        new()
                        {
                            name = new string(""),
                            position = 0,
                            velocity = 0,
                            acceleration = 0
                        }
                    },
                    goal_tolerance = new JointToleranceMsg[1]
                    {
                        new()
                        {
                            name = new string(""),
                            position = 0,
                            velocity = 0,
                            acceleration = 0
                        }
                    },
                    goal_time_tolerance = new RosMessageTypes.BuiltinInterfaces.DurationMsg() { sec=0, nanosec=0},
                }
            };
    }

    private Robotiq3FGripperRobotOutputMsg InitializeMessageGripper(double[] pos)
    {
        return new Robotiq3FGripperRobotOutputMsg
        {
            rACT = (byte)pos[0],  // Activation bit
            rMOD = (byte)pos[1],  // Grasping mode
            rGTO = (byte)pos[2],  // Go To action
            rATR = (byte)pos[3],  // Automatic release
            rGLV = (byte)pos[4],  // Glove mode
            rICF = (byte)pos[5],  // Individual Control of Fingers mode
            rICS = (byte)pos[6],  // Individual Control of Scissor mode
            rPRA = (byte)pos[7],  // Target position of finger A
            rSPA = (byte)pos[8],  // Speed of finger A
            rFRA = (byte)pos[9],  // Force of finger A
            rPRB = (byte)pos[10], // Target position of finger B
            rSPB = (byte)pos[11], // Speed of finger B
            rFRB = (byte)pos[12], // Force of finger B
            rPRC = (byte)pos[13], // Target position of finger C
            rSPC = (byte)pos[14], // Speed of finger C
            rFRC = (byte)pos[15], // Force of finger C
            rPRS = (byte)pos[16], // Scissor axis position
            rSPS = (byte)pos[17], // Scissor axis speed
            rFRS = (byte)pos[18]  // Scissor axis force
        };
    }
}
