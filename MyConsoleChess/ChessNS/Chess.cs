using System;
using System.Collections.Generic;
using MyConsoleChess.BoardNS;
using MyConsoleChess.BoardNS.Enums;
using MyConsoleChess.ChessNS.Enums;

namespace MyConsoleChess.ChessNS
{
    class Chess
    {
        public Board Board { get; private set; }
        public Color CurrentPlayer = Color.White;
        public List<Piece> WhitePiecesCaptured = new List<Piece>();
        public List<Piece> BlackPiecesCaptured = new List<Piece>();
        public bool WhiteInCheck { get; private set; } = false;
        public bool BlackInCheck { get; private set; } = false;

        public Chess(Board board)
        {
            Board = board;
        }
        public void InitBoard()
        {
            Board.Clear();
            //Black pieces
            for (int i = 0; i < Board.Collums; i++)
            {
                Board.AddPiece(new Pawn(1, i, Color.Black, Board));
            }
            Board.AddPiece(new Tower(0, 0, Color.Black, Board));
            Board.AddPiece(new Knight(0, 1, Color.Black, Board));
            Board.AddPiece(new Bishop(0, 2, Color.Black, Board));
            Board.AddPiece(new Queen(0, 3, Color.Black, Board));
            Board.AddPiece(new King(0, 4, Color.Black, Board));
            Board.AddPiece(new Bishop(0, 5, Color.Black, Board));
            Board.AddPiece(new Knight(0, 6, Color.Black, Board));
            Board.AddPiece(new Tower(0, 7, Color.Black, Board));

            //White pieces
            for (int i = 0; i < Board.Collums; i++)
            {
                Board.AddPiece(new Pawn(6, i, Color.White, Board));
            }
            Board.AddPiece(new Tower(7, 0, Color.White, Board));
            Board.AddPiece(new Knight(7, 1, Color.White, Board));
            Board.AddPiece(new Bishop(7, 2, Color.White, Board));
            Board.AddPiece(new Queen(7, 3, Color.White, Board));
            Board.AddPiece(new King(7, 4, Color.White, Board));
            Board.AddPiece(new Bishop(7, 5, Color.White, Board));
            Board.AddPiece(new Knight(7, 6, Color.White, Board));
            Board.AddPiece(new Tower(7, 7, Color.White, Board));
        }
        /// <summary>
        /// Check if the king from the informed color is in check
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        
        public void KingsCheck() {
            WhiteInCheck = KingCheck(Color.White);
            BlackInCheck = KingCheck(Color.Black);
        }
        public bool MovePiece(Piece piece, int row, int collum, Moves[,] possibleMovements)
        {
            if (Board.MovePiece(piece, row, collum, possibleMovements))
            {
                KingsCheck();
                return true;
            }
            return false;
        }
        private bool KingCheck(Color color)
        {
            Piece[,] pieces = Board.Pieces;

            for (int i = 0; i < Board.Rows; i++) {
                for (int j = 0; j < Board.Collums; j++)
                {
                    if (pieces[i, j] == null) continue;
                    Moves[,] possibleMovements = pieces[i, j].PossibleMovements(pieces);
                    for (int k = 0; k < Board.Rows; k++)
                    {
                        for (int l = 0; l < Board.Collums; l++)
                        {
                            if (possibleMovements[k, l] == Moves.Capture && pieces[k, l] is King 
                                && pieces[k, l].Color == color) return true;
                        }
                    }
                }
            }
            return false;
        }

    }
}