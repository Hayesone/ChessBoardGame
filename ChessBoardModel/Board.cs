using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
                    int rowMove = keyValuePair.Value.Item1;
                    int colMove = keyValuePair.Value.Item2;

                    try
                    {
                        Cell destination;

                        if (currentCell.Piece.Colour == "White")
                        {
                            destination = theGrid[cellRowRank + -rowMove, cellColFile + -colMove];
                        }
                        else
                        {
                            destination = theGrid[cellRowRank + rowMove, cellColFile + colMove];
                        }

                        if (destination.Piece is not null && destination.Piece.Colour == currentCell.Piece.Colour)
                        {
                            continue;
                        }
                        else 
                        {
                            destination.LegalNextMove = true;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        continue;
                    }

                    
                }
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
    
}
