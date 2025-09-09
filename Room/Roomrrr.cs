using UnityEngine;

public class Roomrrr : MonoBehaviour
{
    public RoomInfo roomInfo;

    public void OpenRoom() {
        NetworkManager.SendBuffer(PacketType.EnterRoom, roomInfo);
    }
}
