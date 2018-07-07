using System;
using System.Collections.Generic;
using System.Linq;
using Collabitama.Client.Enums;
using Collabitama.Client.Models;

namespace Collabitama.Client {
    public static class Strategy {
        public static Move GetNextMove(GameState gamestate, PlayerIdentityEnum identity) {
            var possibleMoves = GetPossibleMoves(gamestate).ToList();

            return possibleMoves.Any()
                ? possibleMoves.OrderBy(_ => Guid.NewGuid()).First()
                : new Move.Pass(gamestate.MyHand.First().Type);
        }

        private static IEnumerable<Move> GetPossibleMoves(GameState gamestate) {
            foreach (var piece in gamestate.MyPieces)
            foreach (var card in gamestate.MyHand)
            foreach (var target in card.Targets) {
                var newPosition = piece.PositionOnBoard + target;
                var potentialMove = new Move.Play(card.Type, piece.PositionOnBoard, newPosition);

                if (IsPlayValid(gamestate, potentialMove)) yield return potentialMove;
            }
        }

        private static bool IsPlayValid(GameState gamestate, Move.Play move) {
            //out of bounds
            if (move.To.X < 0 || move.To.X > 4 || move.To.Y < 0 || move.To.Y > 4) return false;

            //if there's a piece on the target position, only allow if piece is of the enemy
            var maybePiece = gamestate.Pieces.Find(p => p.PositionOnBoard == move.To);
            if (maybePiece != null) return maybePiece.Owner != gamestate.CurrentlyPlaying;

            // if is in bounds and noone is on the target position, go ahead.
            return true;
        }
    }
}