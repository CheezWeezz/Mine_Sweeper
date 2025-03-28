using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MineSeeperProject;

public class MSCell : Button
{
    private const string flagIcon = "Images\\Flag.png";
    private const string bombIcon = "Images\\Mine.png";
    private const string emptyIcon = "Images\\Empty.png";
    private const string emptyDisIcon = "Images\\EmptyDis.png";
    private const string num1Icon = "Images\\Num1.png";
    private const string num2Icon = "Images\\Num2.png";
    private const string num3Icon = "Images\\Num3.png";
    private const string num4Icon = "Images\\Num4.png";
    private const string num5Icon = "Images\\Num5.png";
    private const string num6Icon = "Images\\Num6.png";
    private const string num7Icon = "Images\\Num7.png";
    private const string num8Icon = "Images\\Num8.png";

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
        this.Padding = new Thickness(0);
        this.IsEnabledChanged += IsDisable;

        Image img = new Image();
        img.Source = new BitmapImage(new Uri(emptyIcon, UriKind.Relative));
        this.currentContent = MainWindow.m_Empty;
        this.Content = img;
        currentContent = MainWindow.m_Empty;

    }

	public void SwitchContent(int Content)
	{
        Image img = new Image();

        switch (Content)
        {
            case MainWindow.m_Empty:
                img.Source = new BitmapImage(new Uri(emptyIcon, UriKind.Relative));
                this.currentContent = MainWindow.m_Empty;
                this.Content = img;
                break;
            case MainWindow.m_Count:
                this.Content = CorrectNumberImage();
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

    public Image CorrectNumberImage()
    {
        Image img = new Image();

        switch (this.bombArroundCount)
        {
            case 1:
                img.Source = new BitmapImage(new Uri(num1Icon, UriKind.Relative));
                return img;
            case 2:
                img.Source = new BitmapImage(new Uri(num2Icon, UriKind.Relative));
                return img;
            case 3:
                img.Source = new BitmapImage(new Uri(num3Icon, UriKind.Relative));
                return img;
            case 4:
                img.Source = new BitmapImage(new Uri(num4Icon, UriKind.Relative));
                return img;
            case 5:
                img.Source = new BitmapImage(new Uri(num5Icon, UriKind.Relative));
                return img;
            case 6:
                img.Source = new BitmapImage(new Uri(num6Icon, UriKind.Relative));
                return img;
            case 7:
                img.Source = new BitmapImage(new Uri(num7Icon, UriKind.Relative));
                return img;
            case 8:
                img.Source = new BitmapImage(new Uri(num8Icon, UriKind.Relative));
                return img;
            default:
                img.Source = new BitmapImage(new Uri(emptyDisIcon, UriKind.Relative));
                return img;
        }
    }
    private void IsDisable(object sender, DependencyPropertyChangedEventArgs e)
    {
        ChangleDisableAppearance();
    }
    private void ChangleDisableAppearance()
    {
        // Check if the button is disabled
        if (!this.IsEnabled)
        {
            this.BorderBrush = Brushes.Transparent;
            this.BorderThickness = new Thickness(0);
        }
    }
}
