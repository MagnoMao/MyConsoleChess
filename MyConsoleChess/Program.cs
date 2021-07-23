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
            bool matchFinished = false;
            chessMatch.InitBoard();
            while (!matchFinished)
            {
                board.AddPiece(new Knight(5, 0, Color.Black,board));
                board.AddPiece(new Knight(5, 1, Color.Black,board));
                board.AddPiece(new King(2,3, Color.White, board));
                board.Print();
                Console.WriteLine("\nCurrent player: " + chessMatch.CurrentPlayer);
                Piece pieceToMove = chessMatch.GetPieceToMove();
                Moves[,] possibleMovements = pieceToMove.PossibleMovements(board.Pieces, chessMatch.CurrentPlayer);
                
                Console.Clear();
                board.PrintMovements(possibleMovements, pieceToMove);

                Console.ReadLine();
            }
            //int[] coord = chessMatch.PosConverter(Console.ReadLine());
        }
    }
}
