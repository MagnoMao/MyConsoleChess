using System;
using MyConsoleChess.BoardNS;
using MyConsoleChess.BoardNS.Enums;
using MyConsoleChess.ChessNS;
using MyConsoleChess.ChessNS.Enums;


namespace MyConsoleChess
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);
            Chess chessMatch = new Chess(board);
            chessMatch.InitBoard();

            while (!chessMatch.Finished)
            {
                Console.Clear();
                Print.PrintBoard(chessMatch);
                Console.WriteLine("Origin: ");
                Piece pieceToMove = chessMatch.GetPieceToMove(); 

                Console.Clear();
                Print.PrintBoard(chessMatch,pieceToMove);
                bool moved = false;
                Console.Write("Target: ");
                chessMatch.MovePiece(pieceToMove);
                if (!moved) continue;
            }
        }
    }
}
