﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Test_WPF
{
    /// <summary>
    /// Logique d'interaction pour NewChallenge.xaml
    /// </summary>
    public partial class NewChallenge : UserControl
    {
        private IEnumerable<Datas.Relationship> listeFriends;
        private int idUser, idGame, idScore;

        public NewChallenge(int idUser, int idGame, int idScore)
        {
            InitializeComponent();
            this.idUser = idUser;
            this.idGame = idGame;
            this.idScore = idScore;
            this.listeFriends = from i in Bdd.DbAccess.Relationships where i.userId1 == idUser select i;
            if (this.listeFriends.Count() == 0)
            {
                this.endOfNewChallenge();
            }
            else
            {
                this.displayFriends();
            }

        }

        private void displayFriends()
        {
            foreach (Datas.Relationship item in this.listeFriends)
            {
                string username = (from i in Bdd.DbAccess.Users where i.ID == item.userId2 select i.username).FirstOrDefault();
                Button bt = new Button() { Content = username, Tag = item.userId2, FontSize = 20, Padding = new Thickness(20), Margin = new Thickness(10) };
                bt.Click += new RoutedEventHandler(bt_Click);
                this.stackPanelFriends.Children.Add(bt);
            }
        }

        void bt_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            Datas.Dual d = new Datas.Dual();
            d.idChallenger = this.idUser;
            d.idChallenged = (int)b.Tag;
            d.idGame = this.idGame;
            d.idScoreChallenger = this.idScore;
            d.date = DateTime.Now;
            Bdd.DbAccess.AddToDuals(d);
            Bdd.DbAccess.SaveChanges();
            this.endOfNewChallenge();
        }

        private void endOfNewChallenge()
        {
            App.mainWindow.gotoHome();
        }
    }
}
