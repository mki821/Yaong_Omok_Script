using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameManager : MonoSingleton<IngameManager>
{
    public bool IsLogined { get; set; }
    public TeamColor Team { get; private set; }

    private void Start() {
        ScreenFixation();
        NetworkManager.EventListener.Add(PacketType.StartGame, StartGame);
    }

    public void Login() {
        NetworkManager.SendBuffer(PacketType.GetMyInfo, true);
    }

    private void StartGame(string data) {
        Team = (TeamColor)Decoder.Decode<short>(data);

        SceneManager.LoadScene(1);
    }

    private void ScreenFixation() => Screen.SetResolution(1920, 1080, true);
}
