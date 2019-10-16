﻿
using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Items
{
    internal class Rupee5 : ICollideable, IDrawable
    {
        private readonly ISprite _sprite = ItemSpriteFactory.Instance.Create1Rupee();
        private readonly Vector2 _drawLocation;
        public Rectangle Bounds { get; private set; }

        public Rupee5(Point location)
        {
            int x = location.X;
            int y = location.Y;
            Bounds = new Rectangle(x + 8, y, 8, 16);
            _drawLocation = new Vector2(x+4, y);
        }

        public bool CollidesWith(Rectangle rect)
        {
            return Bounds.Intersects(rect);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            _sprite.Hide();
            Bounds = new Rectangle(0, 0, 0, 0);
            return new Add5Rupee(player);
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
            return NoOp.Instance;
        }

        public ICommand ProjectileEffect(IHaltable projectile)
        {
            return NoOp.Instance;
        }

        public void Update()
        {
            _sprite.Update();
        }

        public void Draw()
        {
            _sprite.Draw(_drawLocation);
        }
    }
}
