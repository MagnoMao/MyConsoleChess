using System;
using MyConsoleChess.BoardNS;
using MyConsoleChess.BoardNS.Enums;
using MyConsoleChess.ChessNS.Enums;

namespace MyConsoleChess.ChessNS
{
    class Knight : Piece
    {
        public Knight(int row,int collum, Color color, Board board) : base(row, collum, color, board)
        {
        }
        public Knight(char collum, int row, Color color, Board board) : base(collum, row, color, board)
        {
        }
        public override Moves[,] PossibleMovements(Piece[,] pieces)
        {
            //Mat init
            int rows = pieces.GetLength(0);
            int collums = pieces.GetLength(1);
            Moves[,] possibleMovements = new Moves[rows, collums];
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < collums; j++)
                {
                    possibleMovements[i, j] = Moves.None;
                }
            }

            for (double a = 0; a < 2 * Math.PI; a += Math.PI / 2.0)
            {
                //Since the knight moves in a L pattern the code will have two parts:
                //Here will be calculated the coordinate to move the knight two tiles forward in the "main direction"
                //of this iteration (starting from right, then up, left and down)
                int auxRow = Row - (int)Math.Round(Math.Sin(a))*2;
                int auxCollum = Collum + (int)Math.Round(Math.Cos(a))*2;

                //Now both "i" and "j" will calculate the actual coordinate with the "side step" to make the L pattern
                // by adding PI/2 to the angle of this iteration and then adding/subtracting it.
                
                //This is the first "side step"
                int i = auxRow + (int)Math.Round(Math.Sin(a + Math.PI / 2.0));
                int j = auxCollum + (int)Math.Round(Math.Cos(a + Math.PI / 2.0));
                if (Board.ValidPos(i, j)) { 
                    if(pieces[i, j] == null) possibleMovements[i, j] = Moves.Move;
                    else if (pieces[i, j].Color == Color.Black)
                    {
                        possibleMovements[i, j] = Moves.Capture;
                    }
                }

                //And this is the second side step
                i = auxRow - (int)Math.Round(Math.Sin(a + Math.PI / 2.0));
                j = auxCollum - (int)Math.Round(Math.Cos(a + Math.PI / 2.0));
                if (Board.ValidPos(i, j))
                {
                    if (pieces[i, j] == null) possibleMovements[i, j] = Moves.Move;
                    else if (pieces[i, j].Color == Color.Black)
                    {
                        possibleMovements[i, j] = Moves.Capture;
                    }
                }
            }

            return possibleMovements;
        }

        public override string ToString()
        {
            return "N";
        }
    }
}
