using System;
using Collabitama.Client.Enums;
using Collabitama.Client.Helpers;
using Collabitama.Client.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Collabitama.Client {
    internal static class Program {
        private const string ApiKey = "4d27b833-dc09-4371-8198-bcd1dd68c72f";

        private static void Main() {
            var botInterface = new AsyncBotInterface(ApiKey, 2000);
            var identity = PlayerIdentityEnum.Unknown;

            while (true) {
                try {
                    var data = botInterface.ReadLine();
                    var message = Deserialize<Message>(data);

                    switch (message.Type) {
                        case MessageType.GameInfo:
                            identity = Deserialize<GameInfo>(message.JsonPayload).Identity;
                            break;
                        case MessageType.NewGameState:
                            var gamestate = Deserialize<GameState>(message.JsonPayload);
                            var move = Strategy.GetNextMove(gamestate, identity);

                            botInterface.WriteLine(CreateMessage(move));
                            break;
                    }
                }
                catch (TimeoutException) {
                    Environment.Exit(0);
                }
            }
        }

        private static string CreateMessage(Move command) {
            var message = new Message {
                JsonPayload = Serialize(command)
            };

            switch (command) {
                case Move.Play _:
                    message.Type = MessageType.MovePiece;
                    break;
                case Move.Pass _:
                    message.Type = MessageType.Pass;
                    break;
            }

            return Serialize(message);
        }

        private static T Deserialize<T>(string serialized) where T : new() {
            return JsonConvert.DeserializeObject<T>(serialized, new StringEnumConverter());
        }

        private static string Serialize(object toSeriliaze) {
            return JsonConvert.SerializeObject(toSeriliaze);
        }
    }
}