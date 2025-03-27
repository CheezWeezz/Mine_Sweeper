using System;
using System.Windows.Controls;
using System.Windows;

public class MSBoard : Grid
{
    private const int CellSize = 35;
    private const int rowHeight = CellSize;
    private const int colWidth = CellSize;

    public int RowCount {  get; }
    public int ColCount { get; }

	public MSBoard(int row, int col)
	{
		this.RowCount = row;
		this.ColCount = col;

        for (int r = 0; r < row; r++)
        {
            this.RowDefinitions.Add(new RowDefinition { Height = new GridLength(rowHeight) });
            for (int c = 0; c < col; c++)
            {
                this.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(colWidth) });
            }
        }
    }

    public void DisableButton(int row, int col)
    {
        foreach (var child in this.Children)
        {
            var cell = this.Children
                    .OfType<MSCell>()
                    .FirstOrDefault(b => Grid.GetRow(b) == row && Grid.GetColumn(b) == col);

            if (cell != null)
            {
                cell.IsEnabled = false;
            }
        }
    }
    public MSCell FindButton(int row, int col)
    {
        foreach (var child in this.Children)
        {
            var cell = this.Children
                    .OfType<MSCell>()
                    .FirstOrDefault(b => Grid.GetRow(b) == row && Grid.GetColumn(b) == col);

            if (cell != null)
            {
                return cell;
            }
        }
        return null;
    }

    public bool IsArroundDisable(int row, int col)
    {
        MSCell centerCell = FindButton(row, col);
        for (int r = row - 1; r < row + 1; r++)
        {
            for (int c = col - 1; c < col + 1; c++)
            {
                MSCell cell = FindButton(r, c);

                if (cell != null && !cell.IsEnabled && centerCell.id != cell.id)
                {
                    return true;
                }

            }
        }
        return false;
    }

    public int DisableCount(bool[,] board)
    {
        int count = 0;
        for (int r = 0; r < board.GetLength(0); r++)
        {
            for (int c = 0; c < board.GetLength(1); c++)
            {
                MSCell btn = FindButton(r, c);

                if (btn != null && !btn.IsEnabled)
                {
                    count++;
                }

            }
        }
        return count;
    }
    public void EndGame(bool[,] board, bool mineCheck)
    {
        if (mineCheck)
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    DisableButton(row, col);
                }
            }
        }
    }
}
