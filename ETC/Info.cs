using UnityEngine;
using TMPro;

public class Info : MonoBehaviour
{
    [SerializeField] private TextMeshPro _nameText;
    [SerializeField] private TextMeshPro _pointText;
    
    private void Start() {
        NetworkManager.EventListener.Add(PacketType.GetMyInfo, SetInfo);

        NetworkManager.SendBuffer(PacketType.GetMyInfo, true);
    }

    private void OnDestroy() {
        NetworkManager.EventListener.Remove(PacketType.GetMyInfo);
    }

    private void SetInfo(string data) {
        UserInfo userInfo = Decoder.Decode<UserInfo>(data);

        _nameText.text = userInfo.Nickname;
        _pointText.text = userInfo.Point.ToString();
    }
}
