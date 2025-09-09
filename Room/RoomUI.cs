using UnityEngine;
using TMPro;

public class RoomUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _user1name;
    [SerializeField] private TextMeshProUGUI _user1point;
    [SerializeField] private TextMeshProUGUI _user2name;
    [SerializeField] private TextMeshProUGUI _user2point;

    [SerializeField] private GameObject _worldCanvas;

    private void Start() {
        NetworkManager.EventListener.Add(PacketType.GetRoomInfo, SetRoomInfo);
        NetworkManager.EventListener.Add(PacketType.EnterRoom, OpenRoom);
        NetworkManager.EventListener.Add(PacketType.ExitRoom, CloseRoom);
        
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        NetworkManager.EventListener.Remove(PacketType.GetRoomInfo);
        NetworkManager.EventListener.Remove(PacketType.EnterRoom);
        NetworkManager.EventListener.Remove(PacketType.ExitRoom);
    }

    private void OpenRoom(string data) {
        bool flag = Decoder.Decode<bool>(data);

        if(flag) {
            gameObject.SetActive(true);
            NetworkManager.SendBuffer(PacketType.GetRoomInfo, true);
        }
    }

    private void CloseRoom(string data) {
        gameObject.SetActive(false);
    }

    private void SetRoomInfo(string data) {
        (UserInfo, UserInfo) userInfos = Decoder.Decode<(UserInfo ,UserInfo)>(data);

        _user1name.text = userInfos.Item1.Nickname;
        _user1point.text = "점수 : " + userInfos.Item1.Point.ToString();

        _user2name.text = userInfos.Item2.Nickname;
        _user2point.text = "점수 : " + userInfos.Item2.Point.ToString();
    }

    public void ChangeTeam() {
        NetworkManager.SendBuffer(PacketType.ChangeTeam, true);
    }

    public void GameStart() {
        NetworkManager.SendBuffer(PacketType.StartGame, true);
    }

    public void ExitRoom() {
        NetworkManager.SendBuffer(PacketType.ExitRoom, true);
        _worldCanvas.SetActive(true);
    }
}
