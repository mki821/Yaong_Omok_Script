public enum PacketType : ushort {
    None = 0,
    Error,

    Register,
    Login,

    GetMyInfo,

    MatchStart,
    MatchCancel,
    MatchSuccess,

    MakeRoom,
    MakeRoomSuccess,
    RefreshRoom,
    EnterRoom,
    ExitRoom,

    GetRoomInfo,
    ChangeTeam,

    StartGame,
    Move,
    MoveSuccess,
    EndGame,
}

public enum ErrorType : ushort {
    None = 0,
    
    MissingRoom,
    MakeRoomFailure
}
