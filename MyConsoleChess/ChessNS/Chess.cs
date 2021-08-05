using System;
using System.Collections.Generic;
using MyConsoleChess.BoardNS;
using MyConsoleChess.BoardNS.Enums;
using MyConsoleChess.ChessNS.Enums;

namespace MyConsoleChess.ChessNS
{
    class Chess
    {
        public Piece PieceEnPassant = null;
        public Board Board { get; private set; }
        public Color CurrentPlayer { get; private set; } = Color.White;
        public bool Finished { get; private set; } = false;
        public List<Piece> WhitePiecesCaptured = new List<Piece>();
        public List<Piece> BlackPiecesCaptured = new List<Piece>();
        public bool WhiteInCheck { get; private set; } = false;
        public bool BlackInCheck { get; private set; } = false;

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
                Board.AddPiece(new Pawn(1, i, Color.Black, this));
            }
            Board.AddPiece(new Rook(0, 0, Color.Black, this));
            Board.AddPiece(new Knight(0, 1, Color.Black, this));
            Board.AddPiece(new Bishop(0, 2, Color.Black, this));
            Board.AddPiece(new Queen(0, 3, Color.Black, this));
            Board.AddPiece(new King(0, 4, Color.Black, this));
            Board.AddPiece(new Bishop(0, 5, Color.Black, this));
            Board.AddPiece(new Knight(0, 6, Color.Black, this));
            Board.AddPiece(new Rook(0, 7, Color.Black, this));

            //White pieces
            for (int i = 0; i < Board.Collums; i++)
            {
                Board.AddPiece(new Pawn(6, i, Color.White, this));
            }
            Board.AddPiece(new Rook(7, 0, Color.White, this));
            Board.AddPiece(new Knight(7, 1, Color.White, this));
            Board.AddPiece(new Bishop(7, 2, Color.White, this));
            Board.AddPiece(new Queen(7, 3, Color.White, this));
            Board.AddPiece(new King(7, 4, Color.White, this));
            Board.AddPiece(new Bishop(7, 5, Color.White, this));
            Board.AddPiece(new Knight(7, 6, Color.White, this));
            Board.AddPiece(new Rook(7, 7, Color.White, this));
        }      
        public Piece GetPieceToMove()
        {
            while (true)
            {
                int[] coordinates = Board.GetCoordinates();
                if (coordinates == null) continue;
                int row = coordinates[0];
                int collum = coordinates[1];
                Piece piece = Board.Pieces[row, collum];
                if (piece == null)
                {
                    Console.WriteLine("There is no piece in this location");
                    continue;
                }
                else if (piece.Color != CurrentPlayer)
                {
                    Console.WriteLine("This piece isn't yours to move.");
                }
                else return piece;
            }
        }
        public void MovePiece(Piece piece)
        {
            while (true)
            {
                int[] coordinates = Board.GetCoordinates();
                if (coordinates == null) return; // This way the player can select another piece if he entered an empty input
                int row = coordinates[0];
                int collum = coordinates[1];

                Moves[,] possibleMovements = piece.PossibleMovements();
                if (possibleMovements[row, collum] == Moves.None)
                {
                    Console.WriteLine("This piece can't move to this location");
                    continue;

                }

                //Jogada especial Castle
                if (possibleMovements[row, collum] == Moves.Castle)
                {
                    if (collum == 6)
                    {
                        //Right Castle
                        Board.MovePiece(piece, row, collum);
                        Board.MovePiece(Board.Pieces[row, 7], row, 5);
                    }
                    else
                    {
                        //Left Castle
                        Board.MovePiece(piece, row, collum);
                        Board.MovePiece(Board.Pieces[row, 0], row, 3);
                    }

                    if (CurrentPlayer == Color.White)
                    {
                        BlackInCheck = KingCheck(Color.Black);
                        CurrentPlayer = Color.Black;
                    }
                    else
                    {
                        WhiteInCheck = KingCheck(Color.White);
                        CurrentPlayer = Color.White;
                    }
                    return;
                }


                //Test if the player will put/leave his/her own king in check
                if (WillBeInCheck(piece, row, collum))
                {
                    Console.WriteLine("Your king will be/stay in check");
                    continue;
                }

                if (possibleMovements[row, collum] == Moves.Capture)
                {
                    if (Board.Pieces[row, collum] != null) // Board.Pieces[row, collum] == null means it's a Pawn En Passant move
                    {
                        Piece pieceAtTargetLocation = Board.Pieces[row, collum];
                        if (pieceAtTargetLocation.Color == Color.White)
                            WhitePiecesCaptured.Add(pieceAtTargetLocation);
                        else BlackPiecesCaptured.Add(pieceAtTargetLocation);
                    }
                }

                //Jogada especial En Passant
                if (PieceEnPassant != null)
                {
                    if (row == 2 && Board.Pieces[row + 1, collum] == PieceEnPassant)
                    {
                        BlackPiecesCaptured.Add(PieceEnPassant);
                        Board.Pieces[PieceEnPassant.Row, PieceEnPassant.Collum] = null;
                    }
                    else if (row == 5 && Board.Pieces[row - 1, collum] == PieceEnPassant)
                    {
                        WhitePiecesCaptured.Add(PieceEnPassant);
                        Board.Pieces[PieceEnPassant.Row, PieceEnPassant.Collum] = null;
                    }

                    PieceEnPassant = null;
                }
                 
                Board.MovePiece(piece, row, collum);

                WhiteInCheck = KingCheck(Color.White);
                BlackInCheck = KingCheck(Color.Black);

                if (WhiteInCheck && CheckMate(Color.White))
                {
                    Console.Clear();
                    Print.PrintBoard(this);
                    FinishMatch(Color.Black);
                }
                else if (BlackInCheck && CheckMate(Color.Black))
                {
                    Console.Clear();
                    Print.PrintBoard(this);
                    FinishMatch(Color.White);
                }

                //Pass the turn
                if (CurrentPlayer == Color.White) CurrentPlayer = Color.Black;
                else CurrentPlayer = Color.White;

                return;
            }
        }
        /// <summary>
        /// Check if the king from the informed color is in check
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        ///
        public bool KingCheck(Color color)
        {
            foreach (Piece piece in Board.Pieces)
            {
                if (piece == null || piece.Color == color) continue;
                Moves[,] possibleMovements = piece.PossibleMovements();
                for (int i = 0; i < Board.Rows; i++)
                {
                    for (int j = 0; j < Board.Collums; j++)
                    {
                        if (possibleMovements[i, j] == Moves.Capture && Board.Pieces[i, j] is King
                            && Board.Pieces[i, j].Color == color) return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Test if moving this piece will put the king from the same color in check
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="row"></param>
        /// <param name="collum"></param>
        /// <returns></returns>
        public bool WillBeInCheck(Piece piece, int row, int collum)
        {
            Piece capturedPiece = Board.Pieces[row, collum]; // Can be null, no problem
            Board.Pieces[row, collum] = piece;
            Board.Pieces[piece.Row, piece.Collum] = null;

            bool isInCheck = KingCheck(piece.Color);

            //Return the pieces to the original position
            Board.Pieces[piece.Row, piece.Collum] = piece;
            Board.Pieces[row, collum] = capturedPiece;

            return isInCheck;
        }
        /// <summary>
        /// Check if any piece from the informed color can move to undo the check
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        private bool CheckMate(Color color)
        {
            foreach (Piece piece in Board.Pieces)
            {
                if (piece == null || piece.Color != color) continue;
                Moves[,] possibleMovements = piece.PossibleMovements();
                for (int i = 0; i < Board.Rows; i++)
                {
                    for (int j = 0; j < Board.Collums; j++)
                    {
                        if (possibleMovements[i, j] != Moves.None)
                        {
                            //Save the captured piece then move
                            Piece capturedPiece = Board.Pieces[i, j]; // Can be null, no problem
                            Board.Pieces[i, j] = piece;
                            Board.Pieces[piece.Row, piece.Collum] = null;

                            bool isInCheck = KingCheck(piece.Color);

                            //Return the pieces to the original position
                            Board.Pieces[piece.Row, piece.Collum] = piece;
                            Board.Pieces[i, j] = capturedPiece;

                            if (!isInCheck) return false;


                        }
                    }
                }
            }
            return true;
        }
        private void FinishMatch(Color Winner)
        {
            Finished = true;
            Console.Write("\n\nTHE WINNER IS ");
            if (Winner == Color.White) Console.WriteLine("WHITE");
            else
            {
                Print.StringColor(Winner.ToString().ToUpper(), ConsoleColor.Yellow);
            }
        }
    }
}