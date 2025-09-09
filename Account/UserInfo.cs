[System.Serializable]
public class UserInfo {
    public string Nickname { get; set; }
    public string Password { get; set; }
    public int Point { get; set; } = 0;
    public int MMR { get; set; } = 0;

    public UserInfo() { }

    public UserInfo(string nickname, string password) {
        Nickname = nickname;
        Password = password;
    }
}