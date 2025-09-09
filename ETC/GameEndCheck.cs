using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameEndCheck : MonoBehaviour
{
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private TextMeshProUGUI _winText;

    private void Start() {
        NetworkManager.EventListener.Add(PacketType.EndGame, EndGame);
    }

    private void OnDestroy() {
        NetworkManager.EventListener.Remove(PacketType.EndGame);
    }

    private void EndGame(string data) {
        TeamColor winTeam = (TeamColor)Decoder.Decode<short>(data);

        _winText.text = (winTeam == TeamColor.Black ? "흑" : "백") + "돌이 승리했습니다!";
        _winPanel.SetActive(true);
    }

    public void GotoMain() {
        SceneManager.LoadScene(0);
    }
}
