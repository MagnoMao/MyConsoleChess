using MyConsoleChess.BoardNS;
using MyConsoleChess.BoardNS.Enums;
using MyConsoleChess.ChessNS.Enums;
using System;

namespace MyConsoleChess.ChessNS
{
    class King : Piece
    {
        private bool _moved = false;
        public King(int row,int collum, Color color, Chess chess) : base(row, collum, color, chess)
        {
        }

        public King(char collum, int row, Color color, Chess chess) : base(collum, row, color, chess)
        {
        }
        public override void Move(int row, int collum)
        {
            base.Move(row, collum);
            _moved = true;
        }
        public override Moves[,] PossibleMovements()
        {
            Piece[,] pieces = Chess.Board.Pieces;

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
            
            //Check every Tile near the king
            for (double a = 0; a < 2 * Math.PI; a += Math.PI / 4.0)
            {
                int i = Row - (int)Math.Round(Math.Sin(a));
                int j = Collum + (int)Math.Round(Math.Cos(a));
                if (!Chess.Board.ValidPos(i, j)) continue;
                if (pieces[i, j] == null) possibleMovements[i, j] = Moves.Move;
                else if(pieces[i,j].Color != Color) possibleMovements[i, j] = Moves.Capture;
            }

            //---Castle
            
            //King wasn't moved
            if (_moved) return possibleMovements;
            //king isn't in check
            if ((Color == Color.White && Chess.WhiteInCheck)
                || (Color == Color.Black && Chess.BlackInCheck)) return possibleMovements;
            
            //Castle - Right side
            do // this do is so that I wont need a bunch of nested ifs
            {
                //Check if there is no tower at the right side of the board
                Piece piece = pieces[Row, Collum + 3];
                if (!(pieces[Row, Collum + 3] is Rook)) break;

                Rook tower = pieces[Row, Collum + 3] as Rook;
                if (tower.Color == Color && !tower.Moved)
                
                //Empty space between tower and king
                if (pieces[Row, Collum + 1] != null || pieces[Row, Collum + 2] != null) break;

                //Test if the king will pass through check or be in check
                pieces[Row, Collum + 1] = this;
                pieces[Row, Collum] = null;                
                    
                bool willBeInCheck = Chess.KingCheck(Color);
                
                //Undo de movement
                pieces[Row, Collum] = this;
                pieces[Row, Collum + 2] = null;

                if (willBeInCheck) break;
                
                pieces[Row, Collum + 2] = this;
                pieces[Row, Collum + 1] = null;

                willBeInCheck = Chess.KingCheck(Color);
                
                //Undo de movement
                pieces[Row, Collum] = this;
                pieces[Row, Collum + 2] = null;
                if (willBeInCheck) break;

                possibleMovements[Row, Collum + 2] = Moves.Castle;
            } while (false);

            //Castle - Left side
            do // this do is so that I wont need a bunch of nested ifs
            {
                //Check if there is no tower at the left side of the board
                Piece piece = pieces[Row, Collum - 4];
                if (!(pieces[Row, Collum - 4] is Rook)) break;

                Rook tower = pieces[Row, Collum - 4] as Rook;
                if (tower.Color == Color && !tower.Moved)

                    //Empty space between tower and king
                    if (   pieces[Row, Collum - 1] != null 
                        || pieces[Row, Collum - 2] != null
                        || pieces[Row, Collum - 3] != null) break;

                //Test if the king will pass through check or be in check
                pieces[Row, Collum - 1] = this;
                pieces[Row, Collum] = null;

                bool willBeInCheck = Chess.KingCheck(Color);
                //Undo de movement
                pieces[Row, Collum] = this;
                pieces[Row, Collum - 1] = null;
                if (willBeInCheck) break;

                pieces[Row, Collum - 2] = this;
                pieces[Row, Collum - 1] = null;

                willBeInCheck = Chess.KingCheck(Color);
                //Undo de movement
                pieces[Row, Collum] = this;
                pieces[Row, Collum - 2] = null;
                if (willBeInCheck) break;

                possibleMovements[Row, Collum - 2] = Moves.Castle;
            } while (false);
            return possibleMovements;
        }
        private bool InCheck()
        {

            return false;
        }
        public override string ToString()
        {
            return "K";
        }
    }
}
