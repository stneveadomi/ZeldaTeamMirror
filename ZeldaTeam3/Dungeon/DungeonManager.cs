﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace Zelda.Dungeon
{
    public class DungeonManager : IDrawable
    {
        public Scene Scene { get; private set; }
        public bool[][] EnabledRooms { get; private set; }
        public Point CurrentRoom { get; private set; } = Point.Zero;
        private ISprite _background;
        private IPlayer _player;
        private Scene[][] _scenes;
        private BackgroundId[][] _backgroundIds;

        private enum BackgroundId
        {
            Default = 0,
            Basement = 1,
            OldMan = 2
        }

        private void SetBackground(BackgroundId backgroundId)
        {
            switch (backgroundId)
            {
                case BackgroundId.Default:
                    _background = BackgroundSpriteFactory.Instance.CreateDungeonBackground();
                    break;
                case BackgroundId.Basement:
                    _background = BackgroundSpriteFactory.Instance.CreateBasementBackground();
                    break;
                case BackgroundId.OldMan:
                    _background = BackgroundSpriteFactory.Instance.CreateOldManBackground();
                    break;
                default:
                    _background = BackgroundSpriteFactory.Instance.CreateDungeonBackground();
                    break;
            }
        }

        public void LoadDungeonContent(ContentManager content, IPlayer player)
        {
            _player = player;
            var enabledRooms = content.Load<int[][]>("DungeonEnabledRooms");
            var enemies = content.Load<int[][]>("DungeonEnemies");
            var backgrounds = content.Load<int[][]>("DungeonRoomBackgrounds");
            var rows = enabledRooms.Length;
            _scenes = new Scene[rows][];
            _backgroundIds = new BackgroundId[rows][];
            EnabledRooms = new bool[rows][];

            for (var row = 0; row < rows; row++)
            {
                var cols = enabledRooms[row].Length;
                _scenes[row] = new Scene[cols];
                _backgroundIds[row] = new BackgroundId[cols];
                EnabledRooms[row] = new bool[cols];

                for (var col = 0; col < cols; col++)
                {
                    EnabledRooms[row][col] = enabledRooms[row][col] == 1;
                    if (enabledRooms[row][col] != 1) continue;

                    var enemyId = -1;
                    var backgroundId = BackgroundId.Default;

                    if (row < enemies.Length && col < enemies[row].Length)
                    {
                        enemyId = enemies[row][col];
                    }

                    if (row < backgrounds.Length && col < backgrounds[row].Length)
                    {
                        backgroundId = (BackgroundId) backgrounds[row][col];
                    }

                    var room = new Room(this,content.Load<int[][]>($"Rooms/{row}-{col}"), enemyId);
                    _scenes[row][col] = new Scene(room, player);
                    _backgroundIds[row][col] = backgroundId;
                }
            }
        }

        public void Reset()
        {
            for (var row = 0; row < _scenes.Length; row++)
            {
                for (var col = 0; col < _scenes[row].Length; col++)
                {
                    _scenes[row][col]?.Reset();
                }
            }
        }

        public void Transition(Direction roomDirection) 
        {
            int _newRoomRow = CurrentRoom.X;
            int _newRoomColumn = CurrentRoom.Y;
            if(roomDirection == Direction.Down) 
            {
                _newRoomRow++;
                _player.TeleportToEntrance(Direction.Up);
            }
            if(roomDirection == Direction.Up) 
            {
                _newRoomRow--;
                _player.TeleportToEntrance(Direction.Down);
            }
            if(roomDirection == Direction.Left) 
            {
                _newRoomColumn--;
                _player.TeleportToEntrance(Direction.Right);
            }
            if(roomDirection == Direction.Right) 
            {
                _newRoomColumn++;
                _player.TeleportToEntrance(Direction.Left);
            }
            CurrentRoom = new Point(_newRoomRow,_newRoomColumn);
            SetBackground(_backgroundIds[_newRoomRow][_newRoomColumn]);
            Scene = _scenes[_newRoomRow][_newRoomColumn];
            Scene.SpawnEnemies();
        }

        public void TransitionToRoom(int row, int column)
        {
            CurrentRoom = new Point(row, column);
            SetBackground(_backgroundIds[row][column]);
            bool _specialRoomTP = false;
            if(row == 0 && column == 1)
            {
                _player.TeleportToEntrance(Direction.UpFromBasement);
                _specialRoomTP = true;
            }
            if(row == 1 && column == 1)
            {
                _player.TeleportToEntrance(Direction.DownFromDungeon);
                _specialRoomTP = true;
            }
            if(_specialRoomTP == false)
            {
                _player.TeleportToEntrance(Direction.Down);
            }
            Scene = _scenes[row][column];
            Scene.SpawnEnemies();
        }

        public void Update()
        {
            Scene?.Update();
        }

        public void Draw()
        {
            _background?.Draw(Vector2.Zero);
            Scene?.Draw();
        }
    }
}