using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;
using MineSeeperProject;

public class MSMenuBar : Menu
{
    public event Action? NewGameClicked;
    public MSMenuBar(Window mainWindow)
	{
        MenuItem game = new MenuItem() { Header = "Game" };
        MenuItem newGame = new MenuItem() { Header = "New Game" };
        newGame.Click += (sender, e) => NewGameClicked?.Invoke();

        var easy = new MenuItem() { Header = "Easy", IsCheckable = true, Tag = 0 };
        var medium = new MenuItem() { Header = "Medium", IsCheckable = true, Tag = 1 };
        var hard = new MenuItem() { Header = "Hard", IsCheckable = true, Tag = 2 };

        easy.Checked += (sender, args) => HandleOptionSelected(easy);
        medium.Checked += (sender, args) => HandleOptionSelected(medium);
        hard.Checked += (sender, args) => HandleOptionSelected(hard);

        game.Items.Add(newGame);
        game.Items.Add(easy);
        game.Items.Add(medium);
        game.Items.Add(hard);

        MenuItem exit = new MenuItem() { Header = "Exit" };
        exit.Click += (sender, args) => {
            mainWindow.Close();
        };

        this.Items.Add(game);
        this.Items.Add(exit); 
    }
    private void HandleOptionSelected(MenuItem selectedOption)
    {
        var parent = selectedOption.Parent as MenuItem;

        foreach (var item in parent.Items)
        {
            if (item is MenuItem menuItem && menuItem != selectedOption)
            {
                menuItem.IsChecked = false;
            }
            else
            {
                MainWindow.diffLvl = (int)selectedOption.Tag;
            }
        }
    }
}
