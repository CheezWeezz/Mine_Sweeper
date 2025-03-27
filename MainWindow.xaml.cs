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
        private const int CellSize = 35;
        private const int rowHeight = CellSize;
        private const int colWidth = CellSize;

        public const int m_Empty = 0;
        public const int m_Count = 1;
        public const int m_Flag = 2;
        public const int m_Bomb = 3;

        public bool[,] mineField;
        public static int mineFlag;
        public int diffLvl = 0;

        GameLogic gmL = new GameLogic();
        MSBoard gameBoard;

        public MainWindow()
        {
            mineField = new bool[8, 8];
            gameBoard = new MSBoard(mineField.GetLength(0), mineField.GetLength(1), diffLvl);
            mineFlag = 0;
            InitializeComponent();

            //Put the mines on the board
            gameBoard.BoardSetup(mineField);
            gameBoard.BoardInit(diffLvl);

            //Make the grid
            boardGrid.Children.Add(gameBoard);
        }
    }
}