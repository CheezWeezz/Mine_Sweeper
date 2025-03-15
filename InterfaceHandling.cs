using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using MineSeeperProject;
using System.Data.Common;

public class InterfaceHandling
{
    public static int m_currentCellRow = 0;
    public static int m_currentCellCol = 0;

    /// <summary>
    /// Modify the Content of a Button in the Grid
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

    /// <summary>
    /// Disable a Cell in the Grid
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <param name="newContent"></param>
    public void DisableButton(Grid bGrid, int row, int column)
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
                    button.IsEnabled = false;
                }
            }
        }
    }

    /// <summary>
    /// End the Game
    /// </summary>
    /// <param name="bGrid"></param>
    /// <param name="board"></param>
    /// <param name="mineCheck"></param>
    public void EndGame(Grid bGrid, bool[,] board, bool mineCheck)
    {
        if (mineCheck)
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    DisableButton(bGrid, row, col);
                }
            }
        }
    }
}
