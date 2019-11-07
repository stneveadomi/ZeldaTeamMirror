﻿using Microsoft.Xna.Framework;
using Zelda.GameOver;
using Zelda.GameWin;
using Zelda.HUD;
using Zelda.Music;

namespace Zelda.GameState
{
    internal class GameWinWorld : GameWorld
    {
        private const string WinMessage = "YOU WIN!";
        private const string PlayAgainMessage = "Press Enter to Play Again";
        private static readonly Point WinMessageLocation = new Point((HUDSpriteFactory.ScreenWidth - DrawnText.Width(WinMessage)) / 2, 0);
        private static readonly Point PlayAgainMessageLocation = new Point((HUDSpriteFactory.ScreenWidth - DrawnText.Width(PlayAgainMessage)) / 2, HUDSpriteFactory.ScreenHeight+64);


        private IUpdatable[] _updatables;
        public override IUpdatable[] Updatables => _updatables;

        private readonly GameWinMenu _screen;
        private readonly FrameDelay _menuDelay = new FrameDelay(300);
        private readonly GameWinControllerKeyboard _controllerKeyboard;

        public override IDrawable[] ScaledDrawables { get; }
        public GameWinWorld(GameStateAgent agent) : base(agent)
        {
            MusicManager.Instance.PlayWinMusic();
            ScaledDrawables = new IDrawable[]
            {
                new DrawnText { Location =  WinMessageLocation, Text = WinMessage },

                new DrawnText {Location = PlayAgainMessageLocation, Text = PlayAgainMessage}
            };

            _screen = new GameWinMenu(agent);
            _controllerKeyboard = new GameWinControllerKeyboard(agent, _screen);
            _updatables = new IUpdatable[]
            {
                new GameWinControllerKeyboard(agent, _screen),
                
            };
    
        }
    }
}
