﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Zelda.Projectiles
{
    internal class Arrow : IProjectile
    {
        private const int FramesToDisappear = 140;

        private readonly ISprite _sprite;
        private readonly ArrowAndSwordBeamStateMachine _arrowStateMachine;
        public Rectangle Bounds => _arrowStateMachine.Bounds;

        private int _framesDelayed;

        public List<IProjectile> Projectiles { get; set; }

        public Arrow(Point location, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    _sprite = ProjectileSpriteFactory.Instance.CreateArrowUp();
                    break;
                case Direction.Down:
                    _sprite = ProjectileSpriteFactory.Instance.CreateArrowDown();
                    break;
                case Direction.Left:
                    _sprite = ProjectileSpriteFactory.Instance.CreateArrowLeft();
                    break;
                case Direction.Right:
                    _sprite = ProjectileSpriteFactory.Instance.CreateArrowRight();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _arrowStateMachine = new ArrowAndSwordBeamStateMachine(location, direction);
        }

        public bool CollidesWith(Rectangle rectangle)
        {
            return _arrowStateMachine.CollidesWith(rectangle);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            return Commands.NoOp.Instance;
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
            _sprite.Hide();
            _arrowStateMachine.ClearBounds();
            return new Commands.SpawnableDamage(enemy);
        }

        public ICommand ProjectileEffect(IHaltable projectile)
        {
            return Commands.NoOp.Instance;
        }

        public void Knockback()
        {

        }

        public void Halt()
        {

        }

        public void removeProjectile() {

        }

        public void AddProjectile()
        {

        }

        public void Update()
        {
            _arrowStateMachine.Update();
            if (_framesDelayed++ == FramesToDisappear)
            {
                _sprite.Hide();
            }
            _sprite.Update();
        }

        public void Draw()
        {
            _sprite.Draw(_arrowStateMachine.Location.ToVector2());
        }
    }
}
