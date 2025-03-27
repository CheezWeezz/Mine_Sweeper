using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MineSeeperProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameLogic gmL = new GameLogic();
        MSBoard gameBoard;

        private readonly int c_Empty = 0;
        private readonly int c_Count = 1;
        private readonly int c_Flag = 2;
        private readonly int c_Bomb = 3;

        private const int CellSize = 35;
        private const int rowHeight = CellSize;
        private const int colWidth = CellSize;
        private bool[,] mineField;
        public static int mineFlag;
        int diffLvl = 0;

        public MainWindow()
        {
            mineField = new bool[8, 8];
            gameBoard = new MSBoard(mineField.GetLength(0), mineField.GetLength(1));
            mineFlag = 0;
            InitializeComponent();

            //Put the mines on the board
            gmL.BoardInit(mineField, diffLvl);

            //Make the grid
            gameBoard = BoardSetup(mineField.GetLength(0), mineField.GetLength(1));
            boardGrid.Children.Add(gameBoard);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            mineField = new bool[8, 8];
            mineFlag = 0;
            InitializeComponent();

            //Put the mines on the board
            gmL.BoardInit(mineField, diffLvl);

            //Make the grid
            boardGrid.Children.Add(BoardSetup(mineField.GetLength(0), mineField.GetLength(1)));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MSCell cell = sender as MSCell;

            if (cell != null)
            {
                int row = cell.row;
                int col = cell.col;

                if (cell.bombArroundCount == 0)
                {
                    CellWithZeros(cell.bombArroundCount, row, col, mineField);
                }
                RevealNumbersAround(mineField,row,col);
                gameBoard.DisableButton(row,col);
                gameBoard.EndGame(mineField, GameOverCheck(mineField, row, col));
                CheckForWin(mineField, diffLvl);
            }
        }

        public void Button_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                MSCell cell = sender as MSCell;
                if (cell != null)
                {
                    int row = cell.row;
                    int col = cell.col;

                    if (cell.currentContent == c_Flag)
                    {
                        if (gameBoard.IsArroundDisable(row,col))
                        {
                            cell.SwitchContent(c_Count);
                        }
                        else
                        {
                            cell.SwitchContent(c_Empty);
                        }
                        mineFlag--;
                    }
                    else
                    {
                        cell.SwitchContent(c_Flag);
                        mineFlag++;
                    }
                }
            }
            CheckForWin(mineField, diffLvl);
        }

        public MSBoard BoardSetup(int bRow, int bCol)
        {
            MSBoard bGrid = new MSBoard(bRow,bCol);
            int countForId = 0;
            for (int r = 0; r < bRow; r++)
            {
                for (int c = 0; c < bCol; c++)
                {
                    countForId++;
                    MSCell cell = new MSCell(countForId, r, c);
                    MSBoard.SetRow(cell, r);
                    MSBoard.SetColumn(cell, c);
                    cell.bombArroundCount = gmL.MineRadar(mineField,r,c);
                    cell.isBomb = mineField[r, c];
                    cell.Click += Button_Click;
                    cell.MouseDown += Button_MouseDown;
                    bGrid.Children.Add(cell);
                }
            }
            return bGrid;
        }

        public void RevealNumbersAround(bool[,] board, int row, int col)
        {
            for (int r = row - 1; r <= row + 1; r++)
            {
                for (int c = col - 1; c <= col + 1; c++)
                {
                    MSCell cell  = gameBoard.FindButton(r, c);
                    if (cell != null && !(cell.Content is Image))
                    {
                        cell.SwitchContent(c_Count);
                    }
                }
            }
        }

        /// <summary>
        /// Check Adjacent 0's
        /// </summary>
        /// <param name="count"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="board"></param>
        /// <param name="bGrid"></param>
        public void CellWithZeros(int count, int row, int col, bool[,] board)
        {

            for (int r = row - 1; r <= row + 1; r++)
            {
                for (int c = col - 1; c <= col + 1; c++)
                {
                    MSCell cell = gameBoard.FindButton(r, c);
                    if (cell != null)
                    {
                        if (cell.bombArroundCount == 0 && cell.IsEnabled == true)
                        {
                            cell.SwitchContent(c_Count);
                            RevealNumbersAround(mineField,r,c);
                            gameBoard.DisableButton(r, c);
                            CellWithZeros(count, r, c, board);
                        }
                    }
                }
            }
        }

        public bool GameOverCheck(bool[,] board, int row, int col)
        {
            MSCell cell = gameBoard.FindButton(row, col);

            if (cell.isBomb)
            {
                return true;
            }
            return false;
        }

        public void CheckForWin(bool[,] board, int diff)
        {
            int boardSize = gameBoard.RowCount * gameBoard.ColCount - gmL.lvl[diff];
            if (gameBoard.DisableCount(board) == boardSize && mineFlag == gmL.lvl[diff])
            {
                for (int row = 0; row < board.GetLength(0); row++)
                {
                    for (int col = 0; col < board.GetLength(1); col++)
                    {
                        gameBoard.DisableButton(row, col);
                    }
                }
            }
        }
    }
}