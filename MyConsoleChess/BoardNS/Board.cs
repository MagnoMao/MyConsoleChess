using System;
using MyConsoleChess.BoardNS.Enums;
using MyConsoleChess.ChessNS.Enums;

namespace MyConsoleChess.BoardNS
{
    class Board
    {
        public int Rows { get; private set; }
        public int Collums { get; private set; }
        public Piece[,] Pieces { get; private set; }

        public Board(int rows, int collums)
        {
            Rows = rows;
            Collums = collums;
            Pieces = new Piece[Rows, Collums];
        }
        /// <summary>
        /// Check if the coordinate is inside the bounds of the board
        /// </summary>
        /// <param name="row"></param>
        /// <param name="collum"></param>
        /// <returns></returns>
        public bool ValidPos(int row, int collum)
        {
            return row > 0 && row < Rows && collum > 0 && collum < Collums;
        }
        public void AddPiece(Piece p)
        {
            Pieces[p.Row, p.Collum] = p;
        }
        public void Clear()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Collums; j++)
                {
                    Pieces[i, j] = null;
                }
            }
        }
        /// <summary>
        /// Print the board 
        /// </summary>
        public void Print()
        {
            //Print the coord at the TOP of the board
            PrintBoardCoordChar();
            for (int i = 0; i < Rows; i++)
            {
                //Print the coord at the LEFT side of the board
                Console.Write(Rows - i + " ");
                for (int j = 0; j < Collums; j++)
                {
                    if (Pieces[i, j] == null) Console.Write("- ");
                    else
                    {
                        if(Pieces[i, j].Color != Color.White)
                        {
                            PrintPieceColor(Pieces[i, j], ConsoleColor.Yellow);
                        }
                        else Console.Write(Pieces[i, j] + " ");
                    }
                }
                //Print the coord at the RIGHT side of the board
                Console.Write(Rows - i + " ");
                Console.WriteLine();
            }
            //Print the cood at the BOTTOM of the board
            PrintBoardCoordChar();
        }
        public void PrintMovements(Moves[,] possibleMovements, Piece piece)
        {
            //Print the coord at the TOP of the board
            PrintBoardCoordChar();
            for (int i = 0; i < Rows; i++)
            {
                //Print the coord at the LEFT side of the board
                Console.Write(Rows - i + " ");
                for (int j = 0; j < Collums; j++)
                {
                    //The piece that is current selected
                    if(i == piece.Row && j == piece.Collum)
                    {
                        PrintPieceColor(Pieces[i, j], ConsoleColor.Green);
                        continue;
                    }
                    //Piece can move to this location
                    if (possibleMovements[i, j] == Moves.Move) {
                        Console.Write("X ");//(char)178
                        continue;
                    }
                    if (possibleMovements[i, j] == Moves.Capture)
                    {
                        PrintPieceColor(Pieces[i, j], ConsoleColor.Red);
                        continue;
                    }
                    if (possibleMovements[i, j] == Moves.None)
                    {
                        if (Pieces[i, j] == null) Console.Write("- ");
                        else if (Pieces[i, j].Color != Color.White)
                        {
                            PrintPieceColor(Pieces[i, j], ConsoleColor.Yellow);
                        }
                        else PrintPieceColor(Pieces[i, j], ConsoleColor.White);
                    }
                }
                //Print the coord at the RIGHT side of the board
                Console.Write(Rows - i + " ");
                Console.WriteLine();
            }
            //Print the cood at the BOTTOM of the board
            PrintBoardCoordChar();
        }
        /// <summary>
        /// Print a string with a different color
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="color"></param>
        private void PrintPieceColor(Piece piece, ConsoleColor color)
        {
            ConsoleColor c = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(piece + " ");
            Console.ForegroundColor = c;
        }
        private void PrintBoardCoordChar()
        {
            Console.Write("  ");
            for (int i = 0; i < Collums; i++)
            {
                char ch = (char)('A' + i);
                Console.Write(ch + " ");
            }
            Console.WriteLine();
        }
    }

}
