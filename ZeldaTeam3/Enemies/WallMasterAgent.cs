﻿using System;
using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    public class WallMasterAgent
    {
        private ISprite _sprite;

        public bool Alive { get; private set; }

        public Point Location;

        private bool _isImmobile;
        private bool _isDying;

        private int _clockDelay;
        private int _health;
        private int _agentClock;

        private Direction _currentDirection;
        private AgentState _agentStatus;

        private const int ActionDelay = 16;
        private static Random rng = new Random();


        //Must be a factor of 16 (grid like movement)
        private const int Velocity = 1;

        public WallMasterAgent(Point location)
        {
            Location = location;
            Alive = false;
            _health = 0;
            _sprite = EnemySpriteFactory.Instance.CreateWallMaster();
            _sprite.Hide();
            _isImmobile = true;
            _isDying = false;

            _agentStatus = AgentState.Ready;
        }

        public void Spawn()
        {
            _sprite = EnemySpriteFactory.Instance.CreateSpawnExplosion();
            _isImmobile = true;
            _clockDelay = 30;
            _health = 2;
            Alive = true;
            _currentDirection = Direction.Down;
        }

        public void Kill()
        {
            if (!Alive)
            {
                return;
            }
            _sprite.Hide();
            _clockDelay = 32;
            _sprite = EnemySpriteFactory.Instance.CreateDeathSparkle();
            _isDying = true;
            Alive = false;
        }

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    MoveUp();
                    return;
                case Direction.Left:
                    MoveLeft();
                    return;
                case Direction.Right:
                    MoveRight();
                    return;
                case Direction.Down:
                    MoveDown();
                    return;
            }
        }

        private void MoveDown()
        {
            Location.Y += Velocity;
        }

        private void MoveLeft()
        {
            Location.X -= Velocity;
        }

        private void MoveRight()
        {
            Location.X += Velocity;
        }

        private void MoveUp()
        {
            Location.Y -= Velocity;
        }

        public void TakeDamage()
        {
            if (Alive)
            {
                _health--;
                _sprite.PaletteShift();
            }

            if (_health < 1)
            {
                Kill();
            }
        }

        public void Draw()
        {
            _sprite.Draw(Location.ToVector2());
        }

        public void Update()
        {

            if (_clockDelay > 0)
            {
                _clockDelay--;
                if (_clockDelay == 0)
                {
                    CheckFlags();
                }
            }
            else
            {
                if (Alive)
                    ExecuteAction();
            }

            _sprite.Update();

        }

        public void ExecuteAction()
        {
            if (_agentClock > 0)
            {
                _agentClock--;
            }

            switch (_agentStatus)
            {
                case AgentState.Ready:
                    UpdateAction();
                    break;
                case AgentState.Halted:
                    if (_agentClock == 0)
                    {
                        _agentStatus = AgentState.Ready;
                    }

                    break;
                case AgentState.Knocked:
                    if (_agentClock != 0)
                    {
                        Move(_currentDirection);
                    }
                    else
                    {
                        FlipDirection();
                        _agentStatus = AgentState.Ready;
                    }

                    break;
                case AgentState.Moving:
                    if (_agentClock != 0)
                    {
                        Move(_currentDirection);
                    }
                    else
                    {
                        _agentStatus = AgentState.Ready;
                    }

                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public void UpdateAction()
        {
            _agentStatus = (AgentState)(rng.Next(3));
            if (_agentStatus == AgentState.Moving)
            {
                _currentDirection = (Direction)(rng.Next(4));
            }
            _agentClock = ActionDelay;

        }

        private void CheckFlags()
        {
            if (_isImmobile)
            {
                _sprite = EnemySpriteFactory.Instance.CreateWallMaster();
                _isImmobile = false;
            }

            if (_isDying)
            {
                _sprite = EnemySpriteFactory.Instance.CreateWallMaster();
                _sprite.Hide();
                _isDying = false;
            }
        }

        public void Knockback()
        {
            _agentStatus = AgentState.Knocked;
            _agentClock = ActionDelay / 2;
            FlipDirection();
        }

        public void Halt()
        {
            _agentStatus = AgentState.Halted;
            _agentClock = ActionDelay;
            FlipDirection();
            Move(_currentDirection);
        }

        public void Stun()
        {
            throw new NotImplementedException();
        }

        private void FlipDirection()
        {
            switch (_currentDirection)
            {
                case Direction.Up:
                    _currentDirection = Direction.Down;
                    break;
                case Direction.Down:
                    _currentDirection = Direction.Up;
                    break;
                case Direction.Left:
                    _currentDirection = Direction.Right;
                    break;
                case Direction.Right:
                    _currentDirection = Direction.Left;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}