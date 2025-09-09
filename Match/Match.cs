using UnityEngine;

public class Match : MonoBehaviour
{
    private void Start() {
        NetworkManager.EventListener.Add(PacketType.MatchStart, (x) => {
            //매칭 시작
        });

        NetworkManager.EventListener.Add(PacketType.MatchCancel, (x) => {
            //매칭 취소
        });

        NetworkManager.EventListener.Add(PacketType.MatchSuccess, (x) => {
            (UserInfo, UserInfo) userInfos = Decoder.Decode<(UserInfo, UserInfo)>(x);

            //보여주기
        });
    }

    private void OnDestroy() {
        NetworkManager.EventListener.Remove(PacketType.MatchStart);
        NetworkManager.EventListener.Remove(PacketType.MatchCancel);
        NetworkManager.EventListener.Remove(PacketType.MatchSuccess);
    }
    
    public void MatchStart() {
        NetworkManager.SendBuffer(PacketType.MatchStart, true);
    }

    public void MatchCancel() {
        NetworkManager.SendBuffer(PacketType.MatchCancel, true);
    }
}
