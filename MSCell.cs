using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using MineSeeperProject;

public class MSCell : Button
{
    private const string flagIcon = "Images\\flag.png";
    private const string bombIcon = "Images\\bomb.png";

    public int id { get; }
    public int row { get; }
    public int col { get; }
    public bool isBomb { get; set; }
    public int bombArroundCount { get; set; }

	public int currentContent { get; set; }

    public MSCell(int id, int row, int col)
	{
        this.id = id;
		this.row = row;
		this.col = col;

    }

	public void SwitchContent(int Content)
	{
        Image img = new Image();

        switch (Content)
        {
            case MainWindow.m_Empty:
                this.Content = "";
                this.currentContent = MainWindow.m_Empty;
                break;
            case MainWindow.m_Count:
                this.Content = bombArroundCount;
                this.currentContent = MainWindow.m_Count;
                break;
            case MainWindow.m_Flag:
                img.Source = new BitmapImage(new Uri(flagIcon, UriKind.Relative));
                this.currentContent = MainWindow.m_Flag;
                this.Content = img;
                break;
            case MainWindow.m_Bomb:
                img.Source = new BitmapImage(new Uri(bombIcon, UriKind.Relative));
                this.currentContent = MainWindow.m_Bomb;
                this.Content = img;
                break;
            default:
                break;
        }
    }
}
