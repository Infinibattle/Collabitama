﻿namespace OnitamaTestClient.Enums {
    public enum MessageType {
        //bot to game
        MovePiece,
        Pass,

        //game to bot
        GameInfo,
        NewGameState,
        InvalidMove
    }
}