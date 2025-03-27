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

        public static int mineFlag;
        public static int diffLvl = 0;

        MSBoard gBoard;
        MSMenuBar menuBar;
        Canvas canvas;

        public MainWindow()
        {
            NewGame();

            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
        }

        private void NewGame()
        {
            canvas = new Canvas();
            gBoard = new MSBoard(diffLvl);
            menuBar = new MSMenuBar(this);
            mineFlag = 0;
            menuBar.NewGameClicked += NewGame;
            canvas.Children.Add(menuBar);
            Canvas.SetTop(gBoard, 30);
            canvas.Children.Add(gBoard);
            this.Content = canvas;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Force a layout update to ensure canvas has measured its content
            this.UpdateLayout();

            // Now that the layout is updated, use ActualWidth and ActualHeight
            double contentWidth = canvas.ActualWidth;
            double contentHeight = canvas.ActualHeight;

            // Adjust the window size based on canvas size, with added padding
            this.Width = contentWidth + 20;  // Optional padding
            this.Height = contentHeight + 50;  // Optional padding

            // You can also set MinWidth and MinHeight to avoid the window shrinking too small
            this.MinWidth = contentWidth + 20;
            this.MinHeight = contentHeight + 50;
        }
    }
}