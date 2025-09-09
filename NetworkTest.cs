using UnityEngine;

public class NetworkTest : MonoBehaviour
{
    [SerializeField] private string _data;

    [ContextMenu("Send")]
    private void Send() {
        RoomInfo room = new RoomInfo(_data, "방 이름", "qlalfqjsgh");

        NetworkManager.SendBuffer(PacketType.MakeRoom, room);
    }

    [ContextMenu("Refresh")]
    private void Refresh() {
        NetworkManager.SendBuffer(PacketType.RefreshRoom, null);
    }

    [ContextMenu("Move")]
    private void Move() {
        Move move = new(MoveType.Put, new Coord(8, 8), TeamColor.Black);

        NetworkManager.SendBuffer(PacketType.Move, move.GetPacket());
    }

    [ContextMenu("MatchMaking Start")]
    private void MatchStart() {
        NetworkManager.SendBuffer(PacketType.MatchStart, null);
    }

    [ContextMenu("MatchMaking Cancel")]
    private void MatchCancel() {
        NetworkManager.SendBuffer(PacketType.MatchCancel, null);
    }
}
