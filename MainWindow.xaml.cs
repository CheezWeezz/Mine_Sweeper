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
        public MainWindow()
        {
            GameLogic gmL = new GameLogic();
            AppLogic appL = new AppLogic();
            mineField = new bool[8, 8];
            InitializeComponent();
            gmL.BoardInit(mineField, 0);
            boardGrid.Children.Add(appL.BoardSetup(8, 8))
            ;
        }
    }
}