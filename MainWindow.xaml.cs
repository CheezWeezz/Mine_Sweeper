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
        private const string flagIcon = "Images\\flag.png";

        GameLogic gmL = new GameLogic();
        InterfaceHandling appL = new InterfaceHandling();

        private const int CellSize = 35;
        private const int rowHeight = CellSize;
        private const int colWidth = CellSize;
        private bool[,] mineField;
        public static int mineCountDisc;

        public MainWindow()
        {
            mineField = new bool[8, 8];
            mineCountDisc = 0;
            InitializeComponent();

            //Make the grid
            boardGrid.Children.Add(BoardSetup(mineField.GetLength(0), mineField.GetLength(1)));
            //Put the mines on the board
            gmL.BoardInit(mineField, 0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            var tag = clickedButton.Tag as Tuple<int, int>;
            if (tag != null)
            {
                int row = tag.Item1;
                int col = tag.Item2;
                //updating my world variable
                InterfaceHandling.m_currentCellRow = tag.Item1;  // Row value
                InterfaceHandling.m_currentCellCol = tag.Item2;  // Column value
                GameLogic.m_MineCheck = true;
                appL.EndGame(boardGrid, mineField, gmL.GameOverCheck(mineField));

                if (gmL.MineRadar(mineField, row, col) == 0)
                {
                    CellWithZeros(gmL.MineRadar(mineField, row, col), row, col, mineField, boardGrid);
                }
                RevealNumbersAround(boardGrid,mineField,row,col);
                appL.DisableButton(boardGrid,row,col);
            }
        }

        public void Button_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                Button clickedButton = sender as Button;
                var tag = clickedButton.Tag as Tuple<int, int>;
                if (tag != null)
                {
                    int row = tag.Item1;
                    int col = tag.Item2;

                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri(flagIcon, UriKind.Relative));

                    if (clickedButton.Content is Image)
                    {
                        if (appL.IsArroundDisable(boardGrid,row,col))
                        {
                            clickedButton.Content = gmL.MineRadar(mineField, row, col);
                        }
                        else
                        {
                            clickedButton.Content = "";
                        }
                        mineCountDisc--;
                    }
                    else
                    {
                        clickedButton.Content = img;
                        mineCountDisc++;
                    }
                }
            }
        }

        /// <summary>
        /// Create a grid with buttons for a game board
        /// </summary>
        /// <param name="bRow"></param>
        /// <param name="bCol"></param>
        /// <returns>Grid</returns>
        public Grid BoardSetup(int bRow, int bCol)
        {
            Grid bGrid = new Grid();
            for (int r = 0; r < bRow; r++)
            {
                bGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(rowHeight) });
                for (int c = 0; c < bCol; c++)
                {
                    bGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(colWidth) });
                    Button btn = new Button
                    {
                        Name = $"btn_{r}_{c}"
                    };
                    Grid.SetRow(btn, r);
                    Grid.SetColumn(btn, c);
                    btn.Tag = new Tuple<int, int>(r, c);
                    btn.Click += Button_Click;
                    btn.MouseDown += Button_MouseDown;
                    bGrid.Children.Add(btn);
                }
            }
            return bGrid;
        }

        /// <summary>
        /// Look for mine arround the Selected index
        /// </summary>
        /// <param name="board"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns>Int</returns>
        public void RevealNumbersAround(Grid bGrid, bool[,] board, int row, int col)
        {
            for (int r = row - 1; r <= row + 1; r++)
            {
                for (int c = col - 1; c <= col + 1; c++)
                {
                    Button btn  = appL.FindButton(bGrid, r, c);
                    if (btn != null && !(btn.Content is Image))
                    {
                        btn.Content = gmL.MineRadar(board, r, c).ToString();
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
        public void CellWithZeros(int count, int row, int col, bool[,] board, Grid bGrid)
        {

            for (int r = row - 1; r <= row + 1; r++)
            {
                for (int c = col - 1; c <= col + 1; c++)
                {
                    Button currentbtn = appL.FindButton(bGrid, r, c);
                    if (currentbtn != null)
                    {
                        if (gmL.MineRadar(board, r, c) == 0 && currentbtn.IsEnabled == true)
                        {
                            currentbtn.Content = gmL.MineRadar(board, r, c).ToString();
                            RevealNumbersAround(boardGrid,mineField,r,c);
                            appL.DisableButton(bGrid, r, c);
                            CellWithZeros(count, r, c, board, bGrid);
                        }
                    }
                }
            }
        }
    }
}