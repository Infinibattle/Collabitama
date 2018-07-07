using System;
using Collabitama.Client.Enums;
using Collabitama.Client.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RemoteBotClient;

namespace Collabitama.Client {
    internal static class Program {
        private const string ApiKey = "APIKEY_HERE";

        private static void Main(string[] args) {
            var botInterface = RemoteBotClientInitializer.Init(ApiKey, false);
            var identity = GetIdentity(botInterface);

            while (true) {
                var gamestate = ReadGameState(botInterface);
                var nextMove = Strategy.GetNextMove(gamestate, identity);
                var asMessage = CreateMessage(nextMove);

                botInterface.WriteLine(Serialize(asMessage));
            }
        }

        private static PlayerIdentityEnum GetIdentity(IBotInterface botInterface) {
            var data = botInterface.ReadLine();
            var message = Deserialize<Message>(data);

            if (message.Type == MessageType.GameInfo) {
                return Deserialize<GameInfo>(message.JsonPayload).Identity;
            }

            throw new Exception($"Expected to read GameInfo message, instead received: {message.Type}");
        }

        private static GameState ReadGameState(IBotInterface botInterface) {
            var data = botInterface.ReadLine();
            var message = JsonConvert.DeserializeObject<Message>(data);

            if (message.Type == MessageType.NewGameState) {
                return Deserialize<GameState>(message.JsonPayload);
            }

            throw new Exception($"Expected to read NewGameState message, instead received: {message.Type}");
        }

        private static Message CreateMessage(Move command) {
            var message = new Message {JsonPayload = Serialize(command)};

            switch (command) {
                case Move.Play _:
                    message.Type = MessageType.MovePiece;
                    break;
                case Move.Pass _:
                    message.Type = MessageType.Pass;
                    break;
            }

            return message;
        }

        private static T Deserialize<T>(string serialized) where T : new() {
            return JsonConvert.DeserializeObject<T>(serialized, new StringEnumConverter());
        }

        private static string Serialize(object toSeriliaze) {
            return JsonConvert.SerializeObject(toSeriliaze);
        }
    }
}