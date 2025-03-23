using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using MineSeeperProject;
using System.Data.Common;
using System.Windows.Navigation;

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

    /// <summary>
    /// Return a Button Object
    /// </summary>
    /// <param name="bGrid"></param>
    /// <param name="Row"></param>
    /// <param name="Column"></param>
    /// <returns>Button</returns>
    public Button FindButton(Grid bGrid, int row, int column)
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
                    return button;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// Check for disable cells arround
    /// </summary>
    /// <param name="bGrid"></param>
    /// <param name="row"></param>
    /// <param name="col"></param>
    /// <returns>bool</returns>
    public bool IsArroundDisable(Grid bGrid, int row, int col)
    {
        for (int r = row-1; r < row+1; r++)
        {
            for (int c = col - 1; c < col + 1; c++)
            {
                Button btn = FindButton(bGrid, r, c);

                if (btn != null && !btn.IsEnabled && r != row && c != col)
                {
                    return true;
                }

            }
        }
        return false;
    }
}
