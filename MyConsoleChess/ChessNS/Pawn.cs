using System;
using MyConsoleChess.BoardNS;
using MyConsoleChess.BoardNS.Enums;
using MyConsoleChess.ChessNS.Enums;

namespace MyConsoleChess.ChessNS
{
    class Pawn : Piece
    {
        private bool _moved  = false;
        
        public Pawn(int row,int collum, Color color, Chess chess) : base(row, collum, color, chess)
        {
        }
        public Pawn(char collum, int row, Color color, Chess chess) : base(collum, row, color, chess)
        {
        }
        public override void Move(int row, int collum)
        {
            if (Math.Abs(Row - row) == 2)
            {
                Chess.PieceEnPassant = this;
            }
            base.Move(row, collum);
            _moved = true;
            
            //Promotion
            if (row == 0 || row == 7)
            {
                Console.WriteLine("Which piece would you like your pawn to be promoted? ");
                Console.WriteLine("0 - Rook\n1 - Knight\n2 - Bishop\n3 - Queen");
                int promote = GetPromote();
                switch (promote){
                    case 0: Chess.Board.Pieces[Row, Collum] = new Rook(Row,Collum,Color,Chess); break;
                    case 1: Chess.Board.Pieces[Row, Collum] = new Knight(Row,Collum,Color,Chess); break;
                    case 2: Chess.Board.Pieces[Row, Collum] = new Bishop(Row,Collum,Color,Chess); break;
                    case 3: Chess.Board.Pieces[Row, Collum] = new Queen(Row,Collum,Color,Chess); break;
                }
            }
        }
        private int GetPromote()
        {
            int promote;
            while (true) {
                promote = int.Parse(Console.ReadLine());
                if(promote < 0 || promote > 3)
                {
                    Console.WriteLine("Invalid");
                    continue;
                }
                break;
            }
            return promote;
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

            //Takes into accout if the pawn is moving up (white piece) out moving down (black piece)
            int dir = 0;
            if (Color == Color.White) dir = - 1;  // Pawn is moving up
            else dir = 1;                      // Pawn is moving down


            //Move foward
            var forwardRow = Row + dir;
            if (Chess.Board.ValidPos(forwardRow, Collum) && pieces[forwardRow, Collum] == null)
            {
                possibleMovements[Row + dir, Collum] = Moves.Move;
                //Move foward 2 tiles if not moved
                if (!_moved)
                {
                    int twoFowardRow = forwardRow + dir;
                    if (Chess.Board.ValidPos(twoFowardRow, Collum) && pieces[twoFowardRow, Collum] == null) possibleMovements[twoFowardRow, Collum] = Moves.Move;
                }
            }

            //Capture diagonally
            int leftCollum = Collum - 1;
            if (Chess.Board.ValidPos(forwardRow, leftCollum)) {
                if (pieces[forwardRow, leftCollum] != null && pieces[forwardRow, leftCollum].Color != Color) 
                    possibleMovements[forwardRow, leftCollum] = Moves.Capture;
            }

            int rightCollum = Collum + 1;
            if (Chess.Board.ValidPos(forwardRow, rightCollum))
            {
                if (pieces[forwardRow, rightCollum] != null && pieces[forwardRow, rightCollum].Color != Color) 
                    possibleMovements[forwardRow, rightCollum] = Moves.Capture;
            }

            //En Passant
            if (Color == Color.White && Row == 3)
            {
                if (Chess.Board.ValidPos(Row, leftCollum) && pieces[Row, leftCollum] == Chess.PieceEnPassant)
                {
                    possibleMovements[Row - 1, leftCollum] = Moves.Move;
                }
                if(Chess.Board.ValidPos(Row, rightCollum) && pieces[Row, rightCollum] == Chess.PieceEnPassant)
                {
                    possibleMovements[Row - 1, rightCollum] = Moves.Move;
                }
            }else if(Color == Color.Black && Row == 4)
            {

            }

            if(Chess.PieceEnPassant != null && (Row == 3 || Row == 4))
            {
                if (Chess.Board.ValidPos(Row, leftCollum) && pieces[Row, leftCollum] == Chess.PieceEnPassant
                    && Chess.PieceEnPassant.Color != Color)
                {
                    possibleMovements[forwardRow, leftCollum] = Moves.Capture;
                }
                if (Chess.Board.ValidPos(Row, rightCollum) && pieces[Row, rightCollum] == Chess.PieceEnPassant
                    && Chess.PieceEnPassant.Color != Color)
                {
                    possibleMovements[forwardRow, rightCollum] = Moves.Capture;
                }
            }

            return possibleMovements;
        }
        
        public override string ToString()
        {
            return "P";
        }
    }
}
