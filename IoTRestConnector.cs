//using UnityEngine;
//using UnityEngine.Networking;
//using System.Text;
//using System.Threading.Tasks;

//public class IoTRestConnector : MonoBehaviour
//{
//    [Header("Azure IoT Hub Settings")]
//    public string hostName = "moro.azure-devices.net";
//    public string deviceId = "1WMHHB639N2097";
//    [TextArea]
//    public string sasToken; // SAS token goes here

//    async void Start()
//    {
//        var url = $"https://{hostName}/devices/{deviceId}/messages/events?api-version=2020-09-30";
//        var payload = "{\"temperature\":23}"; // example JSON
//        var uwr = new UnityWebRequest(url, "POST");
//        byte[] bodyRaw = Encoding.UTF8.GetBytes(payload);
//        uwr.uploadHandler = new UploadHandlerRaw(bodyRaw);
//        uwr.downloadHandler = new DownloadHandlerBuffer();
//        uwr.SetRequestHeader("Content-Type", "application/json");
//        uwr.SetRequestHeader("Authorization", sasToken);

//        var operation = uwr.SendWebRequest();
//        while (!operation.isDone) await Task.Yield();

//        if (uwr.result == UnityWebRequest.Result.Success)
//            Debug.Log("✅ HTTP message sent!");
//        else
//            Debug.LogError($"❌ HTTP error: {uwr.error}\n{uwr.downloadHandler.text}");
//    }
//}
