using System;
using System.Windows.Controls;
using System.Windows;

public class AppLogic
{
    private const int CellSize = 35;
    private const int rowHeight = CellSize;
    private const int colWidth = CellSize;
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
                    Content = "M"
                };
                Grid.SetRow(btn, r);
                Grid.SetColumn(btn, c);
                bGrid.Children.Add(btn);
            }
        }
        return bGrid;
    }
}
