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
            board.AddPiece(new King('d', 4, Color.Black, board));
            while (!matchFinished)
            {
                Console.Clear();
                Print.PrintBoard(chessMatch);
                Console.WriteLine("Origin: ");
                Piece pieceToMove = board.GetPieceToMove(chessMatch.CurrentPlayer);
                if (pieceToMove == null) continue;
                Moves[,] possibleMovements = pieceToMove.PossibleMovements(board.Pieces);

                Console.Clear();
                Print.PrintBoard(chessMatch,pieceToMove, possibleMovements);
                bool moved = false;
                Console.Write("Target: ");
                while (!moved)
                {
                    int[] moveCoord = board.GetCoordinates();
                    if (moveCoord == null) break; // This is how you unselect a piece, simply enter a empty coordinate
                    if (possibleMovements[moveCoord[0], moveCoord[1]] != Moves.None) {
                        Piece pieceAtTargetLocation = null;
                        if (board.Pieces[moveCoord[0], moveCoord[1]] != null) pieceAtTargetLocation = board.Pieces[moveCoord[0], moveCoord[1]];
                        moved = chessMatch.MovePiece(pieceToMove, moveCoord[0], moveCoord[1], possibleMovements);
                        //add piece to chess.capturedPieceWhite or black
                        if (moved)
                        {
                            if (pieceAtTargetLocation != null)
                            {
                                if (pieceAtTargetLocation.Color == Color.White)
                                    chessMatch.WhitePiecesCaptured.Add(pieceAtTargetLocation);
                                else chessMatch.BlackPiecesCaptured.Add(pieceAtTargetLocation);
                            }
                            if (chessMatch.CurrentPlayer == Color.White) chessMatch.CurrentPlayer = Color.Black;
                            else chessMatch.CurrentPlayer = Color.White;
                        }
                    }
                    if (!moved) Console.WriteLine("Invalid position to move");
                }
            }
        }
    }
}
