using System;
using MyConsoleChess.BoardNS;
using MyConsoleChess.BoardNS.Enums;
using MyConsoleChess.ChessNS.Enums;

namespace MyConsoleChess.ChessNS
{
    class Pawn : Piece
    {
        private bool _moved  = false;
        public Pawn(int row,int collum, Color color, Board board) : base(row, collum, color, board)
        {
        }
        public Pawn(char collum, int row, Color color, Board board) : base(collum, row, color, board)
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

            //Takes into accout if the pawn is moving up (white piece) out moving down (black piece)
            int dir = 0;
            if (Color == Color.White) dir = - 1;  // Pawn is moving up
            else dir = 1;                      // Pawn is moving down


            //Move foward
            var forwardRow = Row + dir;
            if (Board.ValidPos(forwardRow, Collum) && pieces[forwardRow, Collum] == null)
            {
                possibleMovements[Row + dir, Collum] = Moves.Move;
                //Move foward 2 tiles if not moved
                if (!_moved)
                {
                    int twoFowardRow = forwardRow + dir;
                    if (Board.ValidPos(twoFowardRow, Collum) && pieces[twoFowardRow, Collum] == null) possibleMovements[twoFowardRow, Collum] = Moves.Move;
                }
            }

            //Capture diagonally
            int leftCollum = Collum - 1;
            if (Board.ValidPos(forwardRow, leftCollum)) {
                if (pieces[forwardRow, leftCollum] != null && pieces[forwardRow, leftCollum].Color != Color) 
                    possibleMovements[forwardRow, leftCollum] = Moves.Capture;
            }

            int rightCollum = Collum + 1;
            if (Board.ValidPos(forwardRow, rightCollum))
            {
                if (pieces[forwardRow, rightCollum] != null && pieces[forwardRow, rightCollum].Color != Color) 
                    possibleMovements[forwardRow, rightCollum] = Moves.Capture;
            }

            return possibleMovements;
        }
        //public override bool CheckMate(Piece[,] pieces)
        //{
        //    //Takes into accout if the pawn is moving up (white piece) out moving down (black piece)
        //    int dir = 0;
        //    if (Color == Color.White) dir = -1;  // Pawn is moving up
        //    else dir = 1;                      // Pawn is moving down

        //    var forwardRow = Row + dir;
        //    //Capture diagonally

        //    int leftCollum = Collum - 1;
        //    if (Board.ValidPos(forwardRow, leftCollum))
        //    {
        //        if (pieces[forwardRow, leftCollum] != null && pieces[forwardRow, leftCollum].Color != Color
        //            && pieces[forwardRow, leftCollum].IsInstanceOfType()) return true;
        //    }

        //    int rightCollum = Collum + 1;
        //    if (Board.ValidPos(forwardRow, rightCollum))
        //    {
        //        if (pieces[forwardRow, rightCollum] != null && pieces[forwardRow, rightCollum].Color != Color) return true;
        //    }
        //    return false;
        //}
        public override bool MoveToLocation(int row, int collum, Moves[,] possibleMovements)
        {
            bool flag = base.MoveToLocation(row, collum, possibleMovements);
            if (flag)
            {
                _moved = true;
                return true;
            }
            else return false;
        }
        public override string ToString()
        {
            return "P";
        }
    }
}
