using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardModel
{
    public class Cell
    {
        // Row = Rank
        public int RowRank { get; set; }
        // Col = File
        public int ColumnFile { get; set; }

        public Piece? Piece { get; set; }

        public bool LegalNextMove { get; set; }

        public bool TakeableNextMove { get; set; }

        public bool KingUnderAttack { get; set; }

        public Cell(int rank, int file)
        {
            RowRank = rank;
            ColumnFile = file;
        }


    }
}
