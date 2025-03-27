using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

public class MSCell : Button
{
	private const int c_Empty = 0;
    private const int c_Count = 1;
    private const int c_Flag = 2;
    private const int c_Bomb = 3;

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
            case c_Empty:
                this.Content = "";
                this.currentContent = c_Empty;
                break;
            case c_Count:
                this.Content = bombArroundCount;
                this.currentContent = c_Count;
                break;
            case c_Flag:
                img.Source = new BitmapImage(new Uri(flagIcon, UriKind.Relative));
                this.currentContent = c_Flag;
                this.Content = img;
                break;
            case c_Bomb:
                img.Source = new BitmapImage(new Uri(bombIcon, UriKind.Relative));
                this.currentContent = c_Bomb;
                this.Content = img;
                break;
            default:
                break;
        }
    }
}
