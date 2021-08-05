using System;
using MyConsoleChess.BoardNS.Enums;
using MyConsoleChess.ChessNS.Enums;

namespace MyConsoleChess.BoardNS
{
    class Board
    {
        public int Rows { get; private set; }
        public int Collums { get; private set; }
        public Piece[,] Pieces;

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
            return row >= 0 && row < Rows && collum >= 0 && collum < Collums;
        }
        /// <summary>
        /// Convert the coordinate from board to matrix and return null if the coordinate is out of bounds.
        /// </summary>
        /// <param name="boardCoord"></param>
        /// <returns></returns>
        public int[] PosConverter(string boardCoord)
        {
            //Receives a Board coord in string (ex: a2, b5) and convert it to the matrix coord
            //(ex: a2 - > (6,0); b5 -> (3,1))

            //Convert the char to a numeral collum and properly identify who is the Row and who is the collum
            int boardCollum = char.ToLower(boardCoord[0]) - 'a';
            int boardRow = int.Parse(boardCoord[1].ToString());

            //Convert the char 
            int matrixRow = Rows - boardRow;
            int matrixCollum = boardCollum;

            //Validate the coordinates according to board size.
            if (ValidPos(matrixRow, matrixCollum))
            {
                int[] pos = new int[] { matrixRow, matrixCollum };
                return pos;
            }
            else return null;

            
        }
        public void AddPiece(Piece p)
        {
            Pieces[p.Row, p.Collum] = p;
        }
        /// <summary>
        /// Get the coordinates from the user in Board system and convert it to matrix coord.
        /// It also test if the coordinate is inside the bounds of the board.
        /// Return null if no input was given.
        /// </summary>
        /// <returns></returns>
        public int[] GetCoordinates()
        {
            while (true)
            {
                string boardCoord = Console.ReadLine();
                if (boardCoord == "") return null;

                int[] coordinates = PosConverter(boardCoord);
                if (coordinates != null) return coordinates;
                else Console.WriteLine("Coordinate out of bounds");
            }
        }
        /// <summary>
        /// Ask for input and get the piece
        /// </summary>
        /// <returns></returns>
        public Piece GetPiece()
        {
            while (true)
            {
                int[] coordinates = GetCoordinates();
                if (coordinates == null) return null;
                int row = coordinates[0];
                int collum = coordinates[1];
                //Validate the input
                if (Pieces[row, collum] == null)
                {
                    Console.WriteLine("There is no piece in this location");
                }
                else return Pieces[row, collum];
            }
        }
        public Piece GetPiece(int row, int collum)
        {
            //Validate the input
            if (Pieces[row, collum] == null)    throw new BoardException("There is no piece in this location");
            else return Pieces[row, collum];
        }
        /// <summary>
        /// Ask for input and get a piece that current player owns
        /// </summary>
        /// <param name="currentPlayer"></param>
        /// <returns></returns>
        public Piece GetPieceToMove(Color currentPlayer)
        {
            while (true)
            {
                Piece piece = GetPiece();
                if (piece == null) return null;
                if (piece.Color != currentPlayer)
                {
                    Console.WriteLine("This piece isn't yours to move.");
                }
                else return piece;
            }
        }
        public void MovePiece(Piece piece, int row, int collum)
        {
            Pieces[row, collum] = piece;
            
            // Before update the coordinates inside the piece I'll use it to clear the 
            // tile where the piece was before moving
            Pieces[piece.Row, piece.Collum] = null;

            piece.Move(row, collum);
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
    }

}
