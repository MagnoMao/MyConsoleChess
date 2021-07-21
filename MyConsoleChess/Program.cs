using System;
using MyConsoleChess.BoardNS;
using MyConsoleChess.ChessNS;

namespace MyConsoleChess
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);
            Chess chessMatch = new Chess(board);
            bool matchFinished = false;
            chessMatch.InitBoard();
            while (!matchFinished)
            {
                board.AddPiece(new Knight(5, 0, Color.Black));
                board.AddPiece(new Knight(5, 1, Color.Black));
                board.Print();
                Console.WriteLine("\nCurrent player: " + chessMatch.CurrentPlayer);
                Piece pieceToMove = chessMatch.GetPieceToMove();
                bool[,] possibleMovements = pieceToMove.PossibleMovements(board.Pieces, chessMatch.CurrentPlayer);
                
                Console.Clear();
                board.PrintMovements(possibleMovements, pieceToMove);

                Console.ReadLine();
            }
            //int[] coord = chessMatch.PosConverter(Console.ReadLine());
        }
    }
}
