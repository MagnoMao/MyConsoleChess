using System;
using System.Collections.Generic;
using MyConsoleChess.BoardNS;
using MyConsoleChess.BoardNS.Enums;
using MyConsoleChess.ChessNS;
using MyConsoleChess.ChessNS.Enums;

namespace MyConsoleChess
{
    static class Print
    {
        public static void PrintBoard(Chess chessMatch)
        {
            int rows = chessMatch.Board.Rows;
            int collums = chessMatch.Board.Collums;
            Piece[,] pieces = chessMatch.Board.Pieces;
            //Print the coord at the TOP of the board
            PrintBoardCoordChar(collums);
            for (int i = 0; i < rows; i++)
            {
                //Print the coord at the LEFT side of the board
                Console.Write(rows - i + " ");
                for (int j = 0; j < collums; j++)
                {
                    if (pieces[i, j] == null) Console.Write("- ");
                    else
                    {
                        if (pieces[i, j].Color != Color.White)
                        {
                            PrintPieceColor(pieces[i, j], ConsoleColor.Yellow);
                        }
                        else Console.Write(pieces[i, j] + " ");
                    }
                }
                //Print the coord at the RIGHT side of the board
                Console.Write(rows - i + " ");
                Console.WriteLine();
            }
            //Print the cood at the BOTTOM of the board
            PrintBoardCoordChar(collums);

            if (chessMatch.WhiteInCheck) Console.WriteLine("\nWhite king is in check.");
            if (chessMatch.BlackInCheck)
            {
                StringColor("\nBlack", ConsoleColor.Yellow);
                Console.WriteLine(" king is in check.");
            }

            Console.WriteLine("\nCaptured:");
            PrintCaptured(chessMatch, Color.White);
            PrintCaptured(chessMatch, Color.Black);

            Console.Write("\nPlayer turn: ");
            if (chessMatch.CurrentPlayer == Color.Black) StringColor(chessMatch.CurrentPlayer + "\n", ConsoleColor.Yellow);
            else Console.WriteLine(chessMatch.CurrentPlayer);
        }
        /// <summary>
        /// Print the board with the possible movements of the selected piece
        /// </summary>
        /// <param name="chessMatch"></param>
        /// <param name="piece"></param>
        public static void PrintBoard(Chess chessMatch, Piece piece)
        {
            int rows = chessMatch.Board.Rows;
            int collums = chessMatch.Board.Collums;
            Piece[,] pieces = chessMatch.Board.Pieces;
            Moves[,] possibleMovements = piece.PossibleMovements();
            //Print the coord at the TOP of the board
            PrintBoardCoordChar(collums);
            for (int i = 0; i < rows; i++)
            {
                //Print the coord at the LEFT side of the board
                Console.Write(rows - i + " ");
                for (int j = 0; j < collums; j++)
                {
                    //The piece that is current selected
                    if (i == piece.Row && j == piece.Collum)
                    {
                        PrintPieceColor(pieces[i, j], ConsoleColor.Green);
                        continue;
                    }
                    //Piece can move to this location
                    if (possibleMovements[i, j] == Moves.Move || possibleMovements[i,j] == Moves.Castle)
                    {
                        Console.Write("X ");//(char)178
                        continue;
                    }
                    if (possibleMovements[i, j] == Moves.Capture)
                    {
                        if(pieces[i, j] == null) StringColor("X ", ConsoleColor.Red);
                        else PrintPieceColor(pieces[i, j], ConsoleColor.Red);
                        continue;
                    }
                    if (possibleMovements[i, j] == Moves.None)
                    {
                        if (pieces[i, j] == null) Console.Write("- ");
                        else if (pieces[i, j].Color != Color.White)
                        {
                            PrintPieceColor(pieces[i, j], ConsoleColor.Yellow);
                        }
                        else PrintPieceColor(pieces[i, j], ConsoleColor.White);
                    }
                }
                //Print the coord at the RIGHT side of the board
                Console.Write(rows - i + " ");
                Console.WriteLine();
            }
            //Print the cood at the BOTTOM of the board
            PrintBoardCoordChar(collums);

            if (chessMatch.WhiteInCheck) Console.WriteLine("\nWhite king is in check.");
            if (chessMatch.BlackInCheck)
            {
                StringColor("\nBlack", ConsoleColor.Yellow);
                Console.WriteLine(" king is in check.");
            }

            Console.WriteLine("\nCaptured:");
            PrintCaptured(chessMatch, Color.White);
            PrintCaptured(chessMatch, Color.Black);

            Console.Write("\nPlayer turn: ");
            if (chessMatch.CurrentPlayer == Color.Black) StringColor(chessMatch.CurrentPlayer + "\n", ConsoleColor.Yellow);
            else Console.WriteLine(chessMatch.CurrentPlayer);
        }
        private static void PrintCaptured(Chess chessMatch,Color color)
        {
            if(color == Color.White)
            {
                Console.Write("[");
                foreach(Piece piece in chessMatch.WhitePiecesCaptured)
                {
                    Console.Write(piece + " ");
                }
                Console.WriteLine("]");
            }
            else
            {
                Console.Write("[");
                foreach (Piece piece in chessMatch.BlackPiecesCaptured)
                {
                    StringColor(piece + " ", ConsoleColor.Yellow);
                }
                Console.WriteLine("]");
            }
        }
        /// <summary>
        /// Console.Write in the specified ConsoleColor
        /// </summary>
        /// <param name="str"></param>
        /// <param name="color"></param>
        public static void StringColor(string str, ConsoleColor color)
        {
            ConsoleColor c = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(str);
            Console.ForegroundColor = c;
        }
        private static void PrintPieceColor(Piece piece, ConsoleColor color)
        {
            ConsoleColor c = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(piece + " ");
            Console.ForegroundColor = c;
        }
        private static void PrintBoardCoordChar(int collums)
        {
            Console.Write("  ");
            for (int i = 0; i < collums; i++)
            {
                char ch = (char)('A' + i);
                Console.Write(ch + " ");
            }
            Console.WriteLine();
        }

    }
}
