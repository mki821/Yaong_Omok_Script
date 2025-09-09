using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using Newtonsoft.Json;
using LitJson;

public class NetworkManager : MonoSingleton<NetworkManager>
{
    public static Dictionary<PacketType, UnityAction<string>> EventListener = new();
    
    [SerializeField] private string IP = "172.31.3.146";
    [SerializeField] private int port = 5555;

    private Queue<System.Action> _commandQueue = new();

    private TcpClient _client;
    private static NetworkStream _stream;

    [SerializeField] private bool _connectServer = true;

    private void Start() {
        if(!_connectServer) return;

        try {
            _client = new TcpClient(IP, port);
        }
        catch(System.Exception ex) {
            Debug.LogError("Failed to Connect to Server!");
            Debug.LogError(ex.Message);
            return;
        }

        _stream = _client.GetStream();

        Thread thread = new(ReceiveBuffer);
        thread.Start();
    }

    private void Update() {
        if(!_connectServer) return;

        if(_commandQueue.Count > 0) {
            _commandQueue.Dequeue()?.Invoke();
        }
    }

    private void OnDisable() {
        if(_client != null) {
            if(!_connectServer) return;
            
            _stream.Close();
            _client.Close();
            Debug.Log("Close");
        }
    }

    private void ReceiveBuffer() {
        StringBuilder plus = new();

        while(true) {
            byte[] buffer = new byte[1024];
            StringBuilder output = new();

            if (plus.ToString().Length > 0) {
                output.Append(plus.ToString());
                plus.Clear();
            }

            while(!output.ToString().Contains("||")) {
                if(_stream.CanRead && _stream.DataAvailable) {
                    int nbytes = _stream.Read(buffer);
                    output.Append(Encoding.UTF8.GetString(buffer));
                }
            }

            string[] datas = output.ToString().Replace("\0", "").Split("||");

            for(int i = 0; i < datas.Length; ++i) {
                string message = datas[i];

                if(message.Length > 0) {
                    if (i == (datas.Length - 1)) plus.Append(message);
                    else {
                        JsonData decode = JsonMapper.ToObject(message);
                        Packet packet = JsonConvert.DeserializeObject<Packet>(message);

                        PacketType type = (PacketType)packet.type;
                        
                        if (EventListener.TryGetValue(type, out UnityAction<string> CallBack)) {
                            _commandQueue.Enqueue(() => CallBack?.Invoke(decode["Data"].ToJson()));
                        }
                        else if(type == PacketType.Error) {
                            Debug.LogError($"Error Code: {(ErrorType)packet.data}");
                        }
                        else Debug.LogError($"{(PacketType)packet.type} Trigger는 찾을 수 없습니다.");
                    }
                }
            }
        }
    }

    public static void SendBuffer(PacketType type, object data) {
        Packet packet = new(type, data);

        byte[] buffer = Encoding.UTF8.GetBytes(packet.ToJson() + "||");

        _stream.Write(buffer);
    }
}
