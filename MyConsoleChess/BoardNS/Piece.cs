namespace MyConsoleChess.BoardNS
{
    abstract class Piece
    {
        public int Collum { get; private set; }
        public int Row { get; private set; }
        public Color Color { get; private set; }
        public Board Board { get; private set; }

        public Piece( int row, int collum, Color color, Board board)
        {
            Collum = collum;
            Row = row;
            Color = color;
            Board = board;
        }
        /// <summary>
        /// This methold will be abstract, but for testing purpose is virtual for now
        /// </summary>
        /// <param name="pieces"></param>
        /// <param name="currentPlayer"></param>
        /// <returns></returns>
        public virtual bool[,] PossibleMovements(Piece[,] pieces, Color currentPlayer)
        {
            bool[,] mat =  new bool[2,2];
            return mat;
        }
    }
}
