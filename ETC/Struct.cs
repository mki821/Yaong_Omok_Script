public class MovePacket {
    public ushort type;
    public Coord coord;
    public ushort direction;
    public short team;

    public MovePacket(Move move) {
        type = (ushort)move.type;
        coord = move.coord;
        direction = (ushort)move.direction;
        team = (short)move.team;
    }

    public MovePacket(MoveType type, Coord coord, TeamColor team) {
        this.type = (ushort)type;
        this.coord = coord;
        this.team = (short)team;
    }

    public MovePacket(MoveType type, Coord coord, Direction direction, TeamColor team) {
        this.type = (ushort)type;
        this.coord = coord;
        this.direction = (ushort)direction;
        this.team = (short)team;
    }

    public Move GetMove() {
        return new Move(this);
    }
}
