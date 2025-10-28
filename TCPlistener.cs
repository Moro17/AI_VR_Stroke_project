using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using Newtonsoft.Json;

public class TcpCommandListener : MonoBehaviour
{
    public DigitalTwinControl twinControl;
    public GripperControl gripperControl;
    public GhostToRealMirror ghostToRealMirror; // trebuie setat în Inspector
    public GripperControllerSim gripperSim;

    private TcpListener listener;
    private Thread listenerThread;

    void Start()
    {
        listener = new TcpListener(IPAddress.Any, 5005);
        listener.Start();
        listenerThread = new Thread(ListenForCommands);
        listenerThread.IsBackground = true;
        listenerThread.Start();
        Debug.Log("✅ TCP Listener started on port 5005");
    }

    void OnApplicationQuit()
    {
        listener.Stop();
        listenerThread?.Abort();
    }

    void ListenForCommands()
    {
        while (true)
        {
            try
            {
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream stream = client.GetStream();

                byte[] buffer = new byte[4096];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string json = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                Debug.Log("📨 Mesaj primit:\n" + json);
                ApplyCommand(json);

                stream.Close();
                client.Close();
            }
            catch (System.Exception ex)
            {
                Debug.LogError("❌ TCP Listener Error: " + ex.Message);
            }
        }
    }

    void ApplyCommand(string json)
    {
        try
        {
            // caz special: doar {"sync":true}
            if (json.Contains("\"sync\":true") && ghostToRealMirror != null)
            {
                MainThreadInvoker.Enqueue(() =>
                {
                    ghostToRealMirror?.SyncFromGhost();
                });

                Debug.Log("🔁 [SYNC] Robotul real a copiat poziția din fantomă.");
                return;
            }

            RobotCommand cmd = JsonConvert.DeserializeObject<RobotCommand>(json);

            if (cmd.joints != null && cmd.joints.Length == 6 && twinControl != null)
            {
                Debug.Log("🔧 Setăm articulații...");
                twinControl.SetExternalJointTargets(cmd.joints);
            }


            if (cmd.gripper != null)
            {
                gripperControl?.SetGraspingMode(cmd.gripper.mode);
                gripperControl?.SetTargetPosition((byte)cmd.gripper.position);
                gripperControl?.ActivateGripper();

                // Simulare locală fără ROS:
                string[] modes = { "Basic", "Pinch", "Wide", "Scissor" };
                string grasp = cmd.gripper.mode >= 0 && cmd.gripper.mode < modes.Length ? modes[cmd.gripper.mode] : "Basic";
                gripperSim?.ApplyGrasp(grasp);
            }


        }
        catch (System.Exception ex)
        {
            Debug.LogError("❌ Failed to parse or apply command: " + ex.Message);
        }
    }


    class RobotCommand
    {
        public float[] joints;
        public GripperData gripper;
    }

    class GripperData
    {
        public byte mode;
        public int position;
    }
}
