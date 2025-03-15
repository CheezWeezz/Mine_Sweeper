using System;
using System.Runtime.CompilerServices;
using System.Web;
using System.Windows.Media.Animation;

public class GameLogic
{
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
}
