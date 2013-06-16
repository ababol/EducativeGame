﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test_WPF.Games
{
    public delegate void DelegateEndOfGame(int idUser, int idGame, int idDefi, int points);

    public interface IGame
    {
        event DelegateEndOfGame EndOfGameEvent;

        void EndOfGame();
    }
}
