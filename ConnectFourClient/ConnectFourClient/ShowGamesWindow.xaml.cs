﻿using ConnectFourClient.ConnectFourService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ConnectFourClient
{
    /// <summary>
    /// Interaction logic for ShowGamesWindow.xaml
    /// </summary>
    public partial class ShowGamesWindow : Window
    {
        public delegate GameDetails[] getGamesDelegate();
        public event getGamesDelegate getGames;
        public ShowGamesWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.getGames += mainWindow.getGames;
        }

        private void GamesDG_Loaded(object sender, RoutedEventArgs e)
        {
            GameDetails[] games = null;
            Thread t = new Thread(() => { games = getGames(); });
            t.Start();
            t.Join();
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                GamesDG.ItemsSource = from game in games
                                      select new
                                      {
                                          Player1 = game.Player1,
                                          Player2 = game.Player2,
                                          GameTime = game.gameTime,
                                          Winner = game.Winner
                                      };
            }));
        }
    }
}