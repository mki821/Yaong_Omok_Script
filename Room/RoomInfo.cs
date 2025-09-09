public class RoomInfo {
    public string ID { get; private set; }
    public string Name { get; private set; }
    public string Password { get; private set; }

    public RoomInfo() { }

    public RoomInfo(string id, string name, string password) {
        ID = id;
        Name = name;
        Password = password;
    }
}
