using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardModel
{
    public class Board
    {
        // the size of the board 8x8
        public int Size = 8;

        public Cell[,] theGrid { get; set; }

        // constructor
        public Board()
        {
            // create a new 2D array of the type Cell
            theGrid = new Cell[Size, Size];

            // fill the Cell 2D array with the Cell obj
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    theGrid[i, j] = new Cell(i, j);
                }
            }

            SetUpBoard(theGrid);
        }

        private void SetUpBoard(Cell[,] b)
        {
            // Pawns
            for (int i = 0; i < Size; i++)
            {
                theGrid[i, 6].Piece = new Pawn { Colour = "White" };

                theGrid[i, 1].Piece = new Pawn { Colour = "Black" };
            }
        }

        public void MarkNextLegalMoves(Cell currentCell)
        {
            // Check current cell is has a Piece
            if (currentCell.Piece is not null)
            {
                int cellRowRank = currentCell.RowRank;
                int cellColFile = currentCell.ColumnFile;

                var movesDict = currentCell.Piece.GetMoves();

                foreach (KeyValuePair<string, Tuple<int, int>> keyValuePair in movesDict)
                {
                    
                    string moveName = keyValuePair.Key;
                    int rowMove = keyValuePair.Value.Item1;
                    int colMove = keyValuePair.Value.Item2;

                    try
                    {
                        Cell destination;

                        if (currentCell.Piece.Colour == "White")
                        {
                            destination = theGrid[cellRowRank + -rowMove, cellColFile + -colMove];
                            validateMove(destination, currentCell, moveName);

                        }
                        else
                        {
                            destination = theGrid[cellRowRank + rowMove, cellColFile + colMove];
                            validateMove(destination, currentCell, moveName);
                        }

                    }
                    catch (IndexOutOfRangeException)
                    {
                        continue;
                    }
                    catch (InvalidMoveException)
                    {
                        continue;
                    }

                    
                }
            }
        }

        public void validateMove(Cell destination, Cell currentCell, string moveName)
        {

            switch (currentCell.Piece)
            {
                case Pawn:
                    // Diagonal moves
                    // TODO: Enable En-passants (enpassant can only happen on ranks 5 and 4.)
                    if (moveName == "captureLeft" || moveName == "captureRight")
                    {
                        // Is there an opposing piece?
                        if (destination.Piece is not null && destination.Piece.Colour != currentCell.Piece.Colour)
                        {
                            destination.LegalNextMove = true;
                            return;
                        }
                        else
                        {
                            throw new InvalidMoveException();
                        }
                    }

                    if (moveName == "forwardOne")
                    {
                        if (destination.Piece is not null)
                        {
                            throw new InvalidMoveException();
                        } else
                        {
                            destination.LegalNextMove = true;
                            return;
                        }
                    }

                    if (moveName == "forwardTwo")
                    {
                        int RowRank = destination.RowRank;
                        int ColFile = destination.ColumnFile;
                        int moveColVector = 1;
                        Cell jumpedOverCell;

                        if (currentCell.Piece.Colour == "White")
                        {
                            jumpedOverCell = theGrid[currentCell.RowRank, currentCell.ColumnFile + -moveColVector];

                        }
                        else
                        {
                            jumpedOverCell = theGrid[currentCell.RowRank, currentCell.ColumnFile + moveColVector];
                        }

                        if (jumpedOverCell.Piece is not null)
                        {
                            throw new InvalidMoveException();
                        } else
                        {
                            destination.LegalNextMove = true;
                            return;
                        }

                    }

                    return;

                case Knight:
                    return;

                case Bishop:
                    return;

                case Rook:
                    return;

                case Queen:
                    return;

                case King:
                    return;


            }

        }

        public void LegalMove(Cell previousCell, Cell currentCell)
        {
            if (!currentCell.LegalNextMove)
            {
                return;
            } else
            {
                // TODO: If Piece is taken, show on the side of the game the Piece taken.
                currentCell.Piece = previousCell.Piece;
                previousCell.Piece = null;

                currentCell.Piece.HasMoved = true;
            }
        }



        public void ClearBoardOfPreviousMoveFluff()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    theGrid[i, j].LegalNextMove = false;
                }
            }
        }
    }

    [Serializable]
    internal class InvalidMoveException : Exception
    {
        public InvalidMoveException()
        {
        }

        public InvalidMoveException(string? message) : base(message)
        {
        }

        public InvalidMoveException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidMoveException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
