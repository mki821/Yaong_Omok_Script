using UnityEngine;
using TMPro;
using Newtonsoft.Json;
using DG.Tweening;

public class AccountUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nicknameInput;
    [SerializeField] private TMP_InputField _passwordInput;

    private void Start() {
        NetworkManager.EventListener.Add(PacketType.Login, CheckLogin);
        NetworkManager.EventListener.Add(PacketType.Register, CheckRegister);

        if(IngameManager.Instance.IsLogined) {
            gameObject.SetActive(false);
            TitleAny.isLogin = true;
        }
    }

    private void OnDestroy() {
        NetworkManager.EventListener.Remove(PacketType.Login);
        NetworkManager.EventListener.Remove(PacketType.Register);
    }

    public void Login() {
        UserInfo userInfo = new UserInfo(_nicknameInput.text,_passwordInput.text);

        NetworkManager.SendBuffer(PacketType.Login, userInfo);
    }

    private void CheckLogin(string data) {
        bool isCorrect = JsonConvert.DeserializeObject<bool>(data);
        Debug.Log(isCorrect ? "Success Login" : "Failed Login");

        if(isCorrect) {
            GetComponent<CanvasGroup>().DOFade(0f, 1f).OnComplete(() => {
                IngameManager.Instance.IsLogined = true;
                gameObject.SetActive(false);
                TitleAny.isLogin = true;

                IngameManager.Instance.Login();
            });
        }
    }

    public void Register() {
        UserInfo userInfo = new UserInfo(_nicknameInput.text,_passwordInput.text);

        NetworkManager.SendBuffer(PacketType.Register, userInfo);
    }

    private void CheckRegister(string data) {
        bool isCorrect = JsonConvert.DeserializeObject<bool>(data);
        Debug.Log(isCorrect ? "Success Register" : "Failed Register");
        
        if(isCorrect) {
            Login();
        }
    }
}
