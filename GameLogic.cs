using System;
using System.Runtime.CompilerServices;
using System.Web;
using System.Windows.Media.Animation;

public class GameLogic
{
    public static bool m_MineCheck = false;
    private readonly int[] lvl = { 10, 40, 99 };

    /// <summary>
    /// Initializes the board with mines
    /// </summary>
    /// <param name="board"></param>
    /// <param name="difLevel"></param>
    public void BoardInit(bool[,] board, int difLevel)
	{
		Random rdm = new Random();
        int[] mines = new int[lvl[difLevel]];

        //Picking random locations for mines
        for (int i = 0; i < lvl[difLevel]; i++)
		{
			mines[i] = rdm.Next(board.GetLength(0)* board.GetLength(1));
		}

        //Placing mines on the board
        for (int row = 0; row < board.GetLength(0); row++)
		{
            for (int col = 0; col < board.GetLength(1); col++)
            {
                int currentTile = row * col;
                for (int i = 0; i < mines.Length; i++)
                {
                    if (currentTile == mines[i])
                    {
                        board[row, col] = true;
                    }
                }
            }
        }
	}

    /// <summary>
    /// Look for mine arround the Selected index
    /// </summary>
    /// <param name="board"></param>
    /// <param name="row"></param>
    /// <param name="col"></param>
    /// <returns></returns>
    public int MineRadar(bool[,] board, int row, int col)
    {
        int count = 0;
        for (int r = row - 1; r <= row + 1; r++)
        {
            for (int c = col - 1; c <= col + 1; c++)
            {
                if (r >= 0 && r < board.GetLength(0) && c >= 0 && c < board.GetLength(1))
                {
                    if (board[r, c])
                    {
                        count++;
                    }
                }
            }
        }
        return count;
    }
    /// <summary>
    /// Check if a mine as been clicked
    /// </summary>
    /// <param name="board"></param>
    public bool GameOverCheck(bool[,] board)
    {
        int row = InterfaceHandling.m_currentCellRow;
        int col = InterfaceHandling.m_currentCellCol;

        if (m_MineCheck == true)
        {
            if (board[row,col])
            {
                return true;
            }
            m_MineCheck = false;
        }
        return false;
    }
}
