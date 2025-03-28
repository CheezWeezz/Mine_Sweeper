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
        private const int CellSize = 32;
        private const int rowHeight = CellSize;
        private const int colWidth = CellSize;

        public const int m_Empty = 0;
        public const int m_Count = 1;
        public const int m_Flag = 2;
        public const int m_Bomb = 3;

        public static int mineFlag;
        public static int diffLvl = 0;

        MSBoard gBoard;
        MSMenuBar menuBar;
        StackPanel Panel;

        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        private void NewGame()
        {
            Panel = new StackPanel();

            gBoard = new MSBoard(diffLvl);
            menuBar = new MSMenuBar(this);
            mineFlag = 0;

            menuBar.NewGameClicked += NewGame;

            Panel.Children.Add(menuBar);
            Panel.Children.Add(gBoard);
            Panel.MaxWidth = CellSize * gBoard.ColCount;

            this.Content = Panel;
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.ResizeMode = ResizeMode.NoResize;
        }
    }
}