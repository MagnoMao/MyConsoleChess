using MyConsoleChess.BoardNS;

namespace MyConsoleChess.ChessNS
{
    class Knight : Piece
    {
        public Knight(int row,int collum, Color color, Board board) : base(row, collum, color, board)
        {
        }

        public override string ToString()
        {
            return "N";
        }
    }
}
