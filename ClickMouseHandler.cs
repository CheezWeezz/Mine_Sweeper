using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using MineSeeperProject;

public class ClickMouseHandler
{
	public static void HandlePreviewMouseDown(object sender, MouseButtonEventArgs e)
	{
        if (sender is MSCell currentCell)
        {
            MSCell cell = sender as MSCell;
            MSBoard gBoard = cell.Parent as MSBoard;

            int row = cell.row;
            int col = cell.col;

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (cell != null)
                {
                    if (cell.bombArroundCount == 0)
                    {
                        gBoard.CellWithZeros(cell.bombArroundCount, row, col);
                    }
                    gBoard.DisableButton(row, col);
                    cell.SwitchContent(MainWindow.m_Count);
                    gBoard.EndGame(gBoard.GameOverCheck(row, col));
                }
            }
            if (e.RightButton == MouseButtonState.Pressed)
            {
                if (cell != null)
                {
                    if (cell.currentContent == MainWindow.m_Flag)
                    {
                        if (gBoard.IsArroundDisable(row, col))
                        {
                            cell.SwitchContent(MainWindow.m_Count);
                        }
                        else
                        {
                            cell.SwitchContent(MainWindow.m_Empty);
                        }
                        MainWindow.mineFlag--;
                    }
                    else
                    {
                        cell.SwitchContent(MainWindow.m_Flag);
                        MainWindow.mineFlag++;
                    }
                }
                gBoard.CheckForWin(gBoard.CurrentDiff);
            }
        }
    }
}
