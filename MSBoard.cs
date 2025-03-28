using System;
using System.Windows.Controls;
using System.Windows;
using MineSeeperProject;
using System.Diagnostics;

public class MSBoard : Grid
{
    public readonly int[] lvl = { 10, 40, 99 };

    private const int CellSize = 32;
    private const int rowHeight = CellSize;
    private const int colWidth = CellSize;

    public int RowCount { get; set; }
    public int ColCount { get; set; }
    public int CurrentDiff { get; }

	public MSBoard(int diff)
	{
        this.CurrentDiff = diff;
        FieldSizeForDiff();

        for (int r = 0; r < RowCount; r++)
        {
            this.RowDefinitions.Add(new RowDefinition { Height = new GridLength(rowHeight) });
            for (int c = 0; c < ColCount; c++)
            {
                this.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(colWidth) });
            }
        }

        AddingCell();
        AddingBomb();
    }

    public void DisableButton(int row, int col)
    {
        MSCell cell = FindButton(row, col);
        if (cell != null)
        {
            cell.IsEnabled = false;
        }
    }
    public MSCell FindButton(int row, int col)
    {
        foreach (var child in this.Children)
        {
            if (child is MSCell cell)
            {
                int r = MSBoard.GetRow(cell);
                int c = MSBoard.GetColumn(cell);

                if (r == row && c == col)
                {
                    return cell;
                }
            }
        }
        return null;
    }
    public int DisableCount()
    {
        int count = 0;
        for (int r = 0; r < this.RowCount; r++)
        {
            for (int c = 0; c < this.ColCount; c++)
            {
                MSCell cell = FindButton(r, c);

                if (cell != null && !cell.IsEnabled)
                {
                    count++;
                }

            }
        }
        return count;
    }
    public void EndGame(bool mineCheck)
    {
        if (mineCheck)
        {
            for (int row = 0; row < this.RowCount; row++)
            {
                for (int col = 0; col < this.ColCount; col++)
                {
                    MSCell cell = FindButton(row, col);
                    if (cell.isBomb)
                    {
                        cell.SwitchContent(MainWindow.m_Bomb);
                    }
                    DisableButton(row, col);
                }
            }
        }
    }
    public void AddingBomb()
    {
        Random rdm = new Random();

        for (int i = 0; i < lvl[CurrentDiff]; i++)
        {
            int row = rdm.Next(this.RowCount);
            int col = rdm.Next(this.ColCount);
            MSCell cell = FindButton(row, col);
            if (cell.isBomb == true)
            {
                i--;
            }
            else
            {
                cell.isBomb = true;
            }
        }
        AddBombCount();

    }

    public void AddingCell()
    {
        int countForId = 0;
        for (int r = 0; r < this.RowCount; r++)
        {
            for (int c = 0; c < this.ColCount; c++)
            {
                countForId++;
                MSCell cell = new MSCell(countForId, r, c);
                MSBoard.SetRow(cell, r);
                MSBoard.SetColumn(cell, c);
                cell.PreviewMouseDown += ClickMouseHandler.HandlePreviewMouseDown;
                this.Children.Add(cell);
            }
        }
    }

    public void AddBombCount()
    {
        int count = 0;
        for (int r = 0; r < this.RowCount; r++)
        {
            for (int c = 0; c < this.ColCount; c++)
            {
                MSCell cell = FindButton(r, c);

                if (cell != null)
                {
                    cell.bombArroundCount = MineRadar(r,c);
                }

            }
        }
    }
    public int MineRadar(int row, int col)
    {
        int count = 0;
        for (int r = row - 1; r <= row + 1; r++)
        {
            for (int c = col - 1; c <= col + 1; c++)
            {
                MSCell cell = FindButton(r, c);
                if (r >= 0 && r < this.RowCount && c >= 0 && c < this.ColCount)
                {
                    if (cell.isBomb == true)
                    {
                        count++;
                    }
                }
            }
        }
        return count;
    }
    public void CellWithZeros(int count, int row, int col)
    {

        for (int r = row - 1; r <= row + 1; r++)
        {
            for (int c = col - 1; c <= col + 1; c++)
            {
                MSCell cell = FindButton(r, c);
                if (cell != null)
                {
                    if (cell.bombArroundCount == 0 && cell.IsEnabled == true)
                    {
                        cell.SwitchContent(MainWindow.m_Count);
                        DisableButton(r, c);
                        CellWithZeros(count, r, c);
                    }
                }
            }
        }
    }
    public bool GameOverCheck(int row, int col)
    {
        MSCell cell = FindButton(row, col);

        if (cell.isBomb)
        {
            return true;
        }
        return false;
    }
    public void CheckForWin(int diff)
    {
        int boardSize = this.RowCount * this.ColCount - this.lvl[diff];
        if (DisableCount() == boardSize && MainWindow.mineFlag == this.lvl[diff])
        {
            for (int row = 0; row < this.RowCount; row++)
            {
                for (int col = 0; col < this.ColCount; col++)
                {
                    this.DisableButton(row, col);
                }
            }
        }
    }

    private void FieldSizeForDiff()
    {
        switch (CurrentDiff)
        {
            case 0:
                RowCount = 8;
                ColCount = 8;
                break;
            case 1:
                RowCount = 16;
                ColCount = 16;
                break;
            case 2:
                RowCount = 16;
                ColCount = 30;
                break;
        }
    }
}
