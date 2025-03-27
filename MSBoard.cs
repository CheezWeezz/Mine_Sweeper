using System;
using System.Windows.Controls;
using System.Windows;
using MineSeeperProject;

public class MSBoard : Grid
{
    public readonly int[] lvl = { 10, 40, 99 };

    private const int CellSize = 35;
    private const int rowHeight = CellSize;
    private const int colWidth = CellSize;

    public int RowCount {  get; }
    public int ColCount { get; }
    public int CurrentDiff { get; }

	public MSBoard(int row, int col, int diff)
	{
		this.RowCount = row;
		this.ColCount = col;
        this.CurrentDiff = diff;

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
        for (int r = row - 1; r <= row + 1; r++)
        {
            for (int c = col - 1; c <= col + 1; c++)
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
    public void BoardInit(int difLevel)
    {
        Random rdm = new Random();
        int[] mines = new int[lvl[difLevel]];

        //Picking random locations for mines
        for (int i = 0; i < lvl[difLevel]; i++)
        {
            int currentNum = rdm.Next(this.RowCount * this.ColCount);
            if (mines.Contains(currentNum))
            {
                i--;
            }
            else
            {
                mines[i] = currentNum;
            }
        }
        Array.Sort(mines);

        //Placing mines on the board
        for (int i = 0; i < mines.Length; i++)
        {
            int row = mines[i] / 8;
            int col = mines[i] - (row * 8);
            MSCell cell = FindButton(row, col);
            cell.isBomb = true;
        }
        AddBombCount();

    }

    public void BoardSetup(bool[,] mineField)
    {
        int countForId = 0;
        for (int r = 0; r < this.RowCount; r++)
        {
            for (int c = 0; c < this.RowCount; c++)
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
    public void RevealNumbersAround(int row, int col)
    {
        for (int r = row - 1; r <= row + 1; r++)
        {
            for (int c = col - 1; c <= col + 1; c++)
            {
                MSCell cell = FindButton(r, c);
                if (cell != null && !(cell.Content is Image))
                {
                    cell.SwitchContent(MainWindow.m_Count);
                }
            }
        }
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
                        RevealNumbersAround(r, c);
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
}
