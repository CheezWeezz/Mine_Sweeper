using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
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
        private bool[,] mineField;
        GameLogic gmL = new GameLogic();
        InterfaceHandling appL = new InterfaceHandling();

        public MainWindow()
        {
            mineField = new bool[8, 8];
            InitializeComponent();

            //Make the grid
            boardGrid.Children.Add(appL.BoardSetup(mineField.GetLength(0), mineField.GetLength(1)));
            //Put the mines on the board
            gmL.BoardInit(mineField, 0);
            //She the numbers on the board
            AddNumbersToBoard(mineField);
        }

        private void AddNumbersToBoard(bool[,] board)
        {
            for (int r = 0; r < board.GetLength(0); r++)
            {
                for (int c = 0; c < board.GetLength(1); c++)
                {
                    appL.ModifyButtonContent(boardGrid,r,c,gmL.MineRadar(board,r,c).ToString());
                }
            }
        }
    }
}