using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

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
                // Knights

                theGrid[i, 3].Piece = new Knight { Colour = "White" };
            }

            
        }

        public void MarkNextLegalMoves(Cell currentCell)
        {
            // Check current cell is has a Piece
            if (currentCell.Piece is not null)
            {
                var movesDict = currentCell.Piece.GetMoves();

                // TODO: Need looping version for Queen, Rook, Bishop.
                foreach (KeyValuePair<string, Tuple<int, int>> keyValuePair in movesDict)
                {
                    string moveName = keyValuePair.Key;
                    int rowMove = keyValuePair.Value.Item1;
                    int colMove = keyValuePair.Value.Item2;

                    try
                    {
                        Cell destination = getCellFromCurrent(currentCell, (rowMove, colMove));
                        validateMove(destination, currentCell, moveName);
                        destination.LegalNextMove = true;

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
                    // TODO: Enable Promotion (can only happen on the first and last ranks.)
                    if (moveName == "captureLeft" || moveName == "captureRight")
                    {
                        // Is there an opposing piece?
                        if (destination.Piece is not null && destination.Piece.Colour != currentCell.Piece.Colour)
                        {
                            return;
                        }
                        else
                        {
                            // No piece to capture and move to.
                            throw new InvalidMoveException();
                        }
                    }

                    if (moveName == "forwardOne")
                    {
                        if (destination.Piece is not null)
                        {
                            // Piece is blocking move.
                            throw new InvalidMoveException();
                        } else
                        {
                            return;
                        }
                    }

                    if (moveName == "forwardTwo")
                    {
                        // Get the cell infront of the current Pawn.
                        Cell jumpedOverCell = getCellFromCurrent(currentCell, (0, 1));

                        if (jumpedOverCell.Piece is not null || destination.Piece is not null)
                        {
                            // Pawns cannot jump forward over another piece or onto another piece.
                            throw new InvalidMoveException();
                        } else
                        {
                            return;
                        }

                    }

                    return;

                case Knight:
                    // Throws exception if the Piece is on its own team.
                    if (destination.Piece is not null && destination.Piece.Colour == currentCell.Piece.Colour)
                    {
                        throw new InvalidMoveException();
                    }
                    else
                    {

                        return;
                    }

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

        private Cell getCellFromCurrent(Cell currentCell, (int, int) move)
        {
            // Gets the cell from the current using the translation vector given.
            // Inverts the direction on so that the origin is always with the current Colour at the bottom of the screen.
            // White gets -vector, and Black gets vector
            int moveRankVector = move.Item1;
            int moveColVector = move.Item2;
            Cell destinationCell;

            if (currentCell.Piece.Colour == "White")
            {
                destinationCell = theGrid[currentCell.RowRank + -moveRankVector, currentCell.ColumnFile + -moveColVector];

            }
            else
            {
                destinationCell = theGrid[currentCell.RowRank + moveRankVector, currentCell.ColumnFile + moveColVector];
            }

            return destinationCell;
            
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
