using LitJson;

public class Packet {
    public ushort type;
    public object data;

    public Packet(PacketType type, object data) {
        this.type = (ushort)type;
        this.data = data;
    }

    public string ToJson() => JsonMapper.ToJson(this);
}
