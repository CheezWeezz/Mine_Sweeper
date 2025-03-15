using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

public class InterfaceHandling
{
    private const int CellSize = 35;
    private const int rowHeight = CellSize;
    private const int colWidth = CellSize;

    /// <summary>
    /// Create a grid with buttons for a game board
    /// </summary>
    /// <param name="bRow"></param>
    /// <param name="bCol"></param>
    /// <returns></returns>
    public Grid BoardSetup(int bRow, int bCol)
    {
        Grid bGrid = new Grid();
        for (int r = 0; r < bRow; r++)
        {
            bGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(rowHeight) });
            for (int c = 0; c < bCol; c++)
            {
                bGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(colWidth) });
                Button btn = new Button{
                    Content = $"Btn {r},{c}",
                    Name = $"btn_{r}_{c}"
                };
                Grid.SetRow(btn, r);
                Grid.SetColumn(btn, c);
                btn.Tag = new Tuple<int, int>(r, c);
                btn.Click += Button_Click;
                bGrid.Children.Add(btn);
            }
        }
        return bGrid;
    }

    /// <summary>
    /// Button click event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        Button clickedButton = sender as Button;
        var tag = clickedButton.Tag as Tuple<int, int>;
        {
            int row = tag.Item1;  // Row value
            int column = tag.Item2;  // Column value

            clickedButton.Background = new SolidColorBrush(Colors.Cyan);
        }
    }

    /// <summary>
    /// Modify the content of a button in the grid
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <param name="newContent"></param>
    public void ModifyButtonContent(Grid bGrid, int row, int column, string newContent)
    {
        foreach (var child in bGrid.Children)
        {
            if (child is Grid grid)
            {
                var button = grid.Children
                    .OfType<Button>()
                    .FirstOrDefault(b => Grid.GetRow(b) == row && Grid.GetColumn(b) == column);

                if (button != null)
                {
                    button.Content = newContent;
                }
            }
        }
    }
}
