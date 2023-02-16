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

        public Form1()
        {
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


            // Check if button Cell clicked is a Legal Move
            if (currentCell.LegalNextMove)
                // If it is then move the Piece to that Cell












            // Check if button press is a legal move
            myBoard.LegalMove(currentCell);

            // Clearing board of previous legal moves
            ClearBoardOfPreviousMoveFluff();
            
            // Mark the next legal moves cells with LegalMoves = true
            myBoard.MarkNextLegalMoves(currentCell);

            // Mark the Cells with colours assoicated with moves
            MarkMovesWithColours();

        }

        private void ClearBoardOfPreviousMoveFluff()
        {
            for (int i = 0; i < myBoard.Size; i++)
            {
                for (int j = 0; j < myBoard.Size; j++)
                {
                    myBoard.theGrid[i, j].LegalNextMove = false;
                }
            }
        }

        private void MarkMovesWithColours()
        {
            for (int i = 0; i < myBoard.Size; i++)
            {
                for (int j = 0; j < myBoard.Size; j++)
                {
                    if (myBoard.theGrid[i, j].LegalNextMove == false)
                    {
                        btnGrid[i, j].BackColor = Color.FromArgb(255, 255, 255);
                        
                        continue;
                    }
                    else
                    {
                        btnGrid[i, j].BackColor = Color.FromArgb(0, 255, 0);
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