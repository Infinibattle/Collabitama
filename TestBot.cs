using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OnitamaTestClient.Models;
using OnitamaTestClient.Models.Enums;
using OnitamaTestClient.Services;
using RemoteBotClient;

namespace OnitamaTestClient {
    class TestBot {
        private GameState gameState;
        private PlayerIdentityEnum identity;
        private readonly IBotInterface botInterface;

        public TestBot(IBotInterface botInterface) {
            this.botInterface = botInterface;
        }

        public void Run() {
            this.ReadGameInfo();

            while (true) {
                this.ReadGameState();
                var nextMove = this.GetNextMove();
                var asMessage = this.CreateMessage(nextMove);
                this.botInterface.WriteLine(Serialize(asMessage));
            }
        }
        
        private void ReadGameInfo() {   
            var data = this.botInterface.ReadLine();

            var message = Deserialize<Message>(data);
            if (message.Type == MessageType.GameInfo) {
                var gameInfo = Deserialize<GameInfo>(message.JsonPayload);
                this.identity = gameInfo.Identity;
            }
            else {
                throw new Exception($"Expected to read GameInfo message, instead received: {message.Type} - Seed: {Seed}");
            }
        }

        private void ReadGameState() {
            var data = this.botInterface.ReadLine();

            var message = JsonConvert.DeserializeObject<Message>(data);
            if (message.Type == MessageType.NewGameState) {
                var gs = Deserialize<GameState>(message.JsonPayload);
                this.gameState = gs;
            }
            else
            {
                throw new Exception($"Expected to read NewGameState message, instead received: {message.Type} - Seed: {Seed}");
            }
        }

        private const string Seed = "Jorik is een gekkie";
        private readonly Random r = new Random(Seed.GetHashCode(StringComparison.InvariantCulture));
        private Move GetNextMove() {
            var possibleMoves = this.GetPossibleMoves().ToList();
            if (!possibleMoves.Any()) {
                return new Move.Pass(this.gameState.MyHand.First().Type);
            }

            return possibleMoves[r.Next(0, possibleMoves.Count())];
        }

        private IEnumerable<Move> GetPossibleMoves() {
            foreach (var piece in this.gameState.MyPieces) {
                foreach (var card in this.gameState.MyHand) {
                    foreach (var target in card.Targets) {
                        var newPosition = piece.PositionOnBoard + target;
                        var potentialMove = new Move.Play(card.Type, piece.PositionOnBoard, newPosition);

                        if (this.IsPlayValid(potentialMove)) {
                            yield return potentialMove;
                        }
                    }
                }
            }
        }

        private bool IsPlayValid(Move.Play move) {
            //out of bounds
            if (move.To.X < 0 || move.To.X > 4 || move.To.Y < 0 || move.To.Y > 4) {
                return false;
            }

            //if there's a piece on the target position, only allow if piece is of the enemy
            var maybePiece = this.gameState.Pieces.Find(p => p.PositionOnBoard == move.To);
            if (maybePiece != null) {
                return maybePiece.Owner != this.gameState.CurrentlyPlaying;
            }

            // if is in bounds and noone is on the target position, go ahead.
            return true;
        }

        private Message CreateMessage(Move command) {
            var message = new Message();
            if (command is Move.Play) {
                message.Type = MessageType.MovePiece;
            }

            if (command is Move.Pass) {
                message.Type = MessageType.Pass;
            }

            message.JsonPayload = Serialize(command);
            return message;
        }

        private static T Deserialize<T>(string serialized) where T : new()
        {
            return JsonConvert.DeserializeObject<T>(serialized, new StringEnumConverter());
        }

        private static string Serialize(object toSeriliaze)
        {
            return JsonConvert.SerializeObject(toSeriliaze);
        }
    }
}
