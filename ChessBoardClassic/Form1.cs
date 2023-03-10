using ChessBoardModel;
using System.Drawing.Design;
using System.Security.Cryptography.Xml;

namespace ChessBoardClassic
{
    public partial class Form1 : Form
    {
        // reference for the Board class, containing all the values for our myBoard object
        static Board myBoard = new Board();

        // 2D array of buttons whose values are determined by myBoard
        public Button[,] btnGrid = new Button[myBoard.Size, myBoard.Size];

        public Cell ?previousCell { get; set; }

        public Form1()
        {
            previousCell = null;
            InitializeComponent();
            populateGrid();
        }

        private void populateGrid()
        {
            int buttonSize = panel1.Width / myBoard.Size;

            panel1.Height = panel1.Width;

            // nested loop, create buttons and print them to the screen
            for (int i = 0; i < myBoard.Size; i++)
            {
                for (int j = 0; j < myBoard.Size; j++)
                {
                    btnGrid[i, j] = new Button();

                    btnGrid[i, j].Height = buttonSize;
                    btnGrid[i, j].Width = buttonSize;

                    // add a click event to each button.
                    btnGrid[i, j].Click += Grid_Button_Click;

                    // add the new buttons to the panel
                    panel1.Controls.Add(btnGrid[i, j]);

                    btnGrid[i, j].Location = new Point(i * buttonSize, j * buttonSize);

                    // Checks which cells have a Piece on and sets their Name according to Piece, else shows their Array Index.
                    if (myBoard.theGrid[i, j].Piece is not null)
                    {
                        btnGrid[i, j].Text = myBoard.theGrid[i, j].Piece.Name;
                    } 
                    else
                    {
                        btnGrid[i, j].Text = i + "|" + j;
                    }

                    
                    btnGrid[i, j].Tag = new Point(i, j);
                }
            }

        }

        private void Grid_Button_Click(object? sender, EventArgs e)
        {
            // get the row and col number of the button clicked.
            Button clickedButton = (Button)sender;
            Point location = (Point)clickedButton.Tag;

            int RowRank = location.X;
            int ColFile = location.Y;
            Cell currentCell = myBoard.theGrid[RowRank, ColFile];

            // If the button press has a cell with LegalNextMove == true; Move the piece to that location.
            if (currentCell.LegalNextMove)
            {
                myBoard.LegalMove(previousCell, currentCell);
                myBoard.ClearBoardOfPreviousMoveFluff();
                MarkupFormVisuals();
                previousCell = currentCell;
                return;
            }


            // Clearing board of previous legal moves
            myBoard.ClearBoardOfPreviousMoveFluff();
            
            // Mark the next legal moves cells with LegalMoves = true
            myBoard.MarkNextLegalMoves(currentCell);

            // Mark the Cells with colours assoicated with moves
            MarkupFormVisuals();

            // Set currentCell to previousCell to be used by the next button click.
            previousCell = currentCell;

        }

        private void MarkupFormVisuals()
        {
            for (int i = 0; i < myBoard.Size; i++)
            {
                for (int j = 0; j < myBoard.Size; j++)
                {
                    // Show the colour of the legal moves for that piece.
                    if (myBoard.theGrid[i, j].LegalNextMove == false)
                    {
                        btnGrid[i, j].BackColor = Color.FromArgb(255, 255, 255);
                    }
                    else
                    {
                        btnGrid[i, j].BackColor = Color.FromArgb(0, 255, 0);
                    }

                    // Update Piece text after move.
                    if (myBoard.theGrid[i, j].Piece is not null)
                    {
                        btnGrid[i, j].Text = myBoard.theGrid[i, j].Piece.Name;
                    }
                    else
                    {
                        btnGrid[i, j].Text = i + "|" + j;
                    }


                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}