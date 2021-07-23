using System;
using MyConsoleChess.BoardNS;
using MyConsoleChess.BoardNS.Enums;

namespace MyConsoleChess.ChessNS
{
    class Chess
    {
        Board Board;
        public Color CurrentPlayer { get; private set; } = Color.White;

        public Chess(Board board)
        {
            Board = board;
        }
        public void InitBoard()
        {
            Board.Clear();
            //Black pieces
            for (int i = 0; i < Board.Collums; i++)
            {
                Board.AddPiece(new Pawn(1, i, Color.Black,Board));
            }
            Board.AddPiece(new Tower(0, 0, Color.Black, Board));
            Board.AddPiece(new Knight(0, 1, Color.Black, Board));
            Board.AddPiece(new Bishop(0, 2, Color.Black, Board));
            Board.AddPiece(new Queen(0, 3, Color.Black, Board));
            Board.AddPiece(new King(0, 4, Color.Black, Board));
            Board.AddPiece(new Bishop(0, 5, Color.Black, Board));
            Board.AddPiece(new Knight(0, 6, Color.Black, Board));
            Board.AddPiece(new Tower(0, 7, Color.Black, Board));

            //White pieces
            for (int i = 0; i < Board.Collums; i++)
            {
                Board.AddPiece(new Pawn(6, i, Color.White, Board));
            }
            Board.AddPiece(new Tower(7, 0, Color.White, Board));
            Board.AddPiece(new Knight(7, 1, Color.White, Board));
            Board.AddPiece(new Bishop(7, 2, Color.White, Board));
            Board.AddPiece(new Queen(7, 3, Color.White, Board));
            Board.AddPiece(new King(7, 4, Color.White, Board));
            Board.AddPiece(new Bishop(7, 5, Color.White, Board));
            Board.AddPiece(new Knight(7, 6, Color.White, Board));
            Board.AddPiece(new Tower(7, 7, Color.White, Board));
        }
        public int[] PosConverter(string chessCoord)
        {
            //Receives a Chess coord in string (ex: a2, b5) and convert it to the matrix coord
            //(ex: a2 - > (6,0); b5 -> (3,1))

            //Convert the char to a numeral collum and properly identify who is the Row and who is the collum
            int chessCollum = char.ToLower(chessCoord[0]) - 'a';
            int chessRow = int.Parse(chessCoord[1].ToString());

            //Convert the char 
            int matrixRow = Board.Rows - chessRow;
            int matrixCollum = chessCollum;

            //Validate the coordinates according to board size.
            if (matrixRow < 0 || matrixRow > Board.Rows
                || matrixCollum < 0 || matrixCollum > Board.Collums)
            {
                DisplayMessage("Position out of bounds");
                return null;
            }
            
            int[] pos = new int[] {matrixRow, matrixCollum};
            return pos;
        }
        private int[] getCoordinates()
        {
            while (true)
            {                
                string chessCoord = Console.ReadLine();
                int[] coordinates = PosConverter(chessCoord);
                if (coordinates != null) return coordinates;
            }
        }
        private void DisplayMessage(string s)
        {
            Console.Clear();
            Board.Print();
            Console.Write("\nPlayer turn: " + CurrentPlayer + "\n");
            Console.WriteLine(s);
        }
        
        public Piece GetPieceToMove()
        {
            while (true)
            {
                int[] coordinates = getCoordinates();                
                int row = coordinates[0];
                int collum = coordinates[1];
                //Validate the input according to chess rules
                if (Board.Pieces[row, collum] == null)
                {
                    DisplayMessage("There is no piece to move in this location");
                }
                else if (Board.Pieces[row, collum].Color != CurrentPlayer)
                {
                    DisplayMessage("This piece isn't yours to move.");
                }
                else return Board.Pieces[row, collum];
            }
        }
    }
}