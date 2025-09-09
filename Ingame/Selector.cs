using UnityEngine;

public class Selector : MonoBehaviour
{
    private void Start() {
        NetworkManager.EventListener.Add(PacketType.Move, (x) => {
            short[,] board = Decoder.Decode<short[,]>(x);
            
            RenderManager.Instance.RenderMove(new GameBoard(board));
        });
        
        NetworkManager.EventListener.Add(PacketType.MoveSuccess, (x) => {
            bool success = Decoder.Decode<bool>(x);

            if(success) {
                //진행
            }
            else {
                //다시
            }
        });
    }

    public void Move(Coord coord) {
        NetworkManager.SendBuffer(PacketType.Move, coord);
    }
}
