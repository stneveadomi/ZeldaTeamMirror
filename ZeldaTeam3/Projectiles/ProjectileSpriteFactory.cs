﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Zelda.Projectiles
{
    public class ProjectileSpriteFactory
    {
        private Texture2D _fieldWeaponsSpriteSheet;
        private Texture2D _bombExplosionSpriteSheet;
        private static ProjectileSpriteFactory _instance = new ProjectileSpriteFactory();
        public static ProjectileSpriteFactory Instance => _instance;

        private ProjectileSpriteFactory() { }

        public void LoadAllTextures(ContentManager content)
        {
            _fieldWeaponsSpriteSheet = content.Load<Texture2D>("FieldWeapons");
            _bombExplosionSpriteSheet = content.Load<Texture2D>("SpawnExplosion");

        }

        public ISprite CreateArrowUp()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 16, 16, 1, new Point(0, 64));
        }
        public ISprite CreateArrowRight()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 16, 16, 1, new Point(16, 64));
        }
        public ISprite CreateArrowLeft()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 16, 16, 1, new Point(32, 64));
        }
        public ISprite CreateArrowDown()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 16, 16, 1, new Point(48, 64));
        }

        public ISprite CreateFireball()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 16, 16, 4, new Point(0, 96));
        }

        public ISprite CreateThrownBoomerang()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 8, 8, 8, new Point(0, 128));
        }

        public ISprite CreateBombExplosion()
        {
            return new Sprite(_bombExplosionSpriteSheet, 16, 16, 3, new Point(0, 0));
        }

        public ISprite CreateWoodSwordBeamUp()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 16, 16, 1, new Point(0, 0));
        }
        public ISprite CreateWoodSwordBeamRight()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 16, 16, 1, new Point(0, 16));
        }
        public ISprite CreateWoodSwordBeamLeft()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 16, 16, 1, new Point(0, 32));
        }
        public ISprite CreateWoodSwordBeamDown()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 16, 16, 1, new Point(0, 48));
        }
        public ISprite CreateWhiteSwordBeamUp()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 16, 16, 1, new Point(32, 0));
        }
        public ISprite CreateWhiteSwordBeamRight()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 16, 16, 1, new Point(32, 16));
        }
        public ISprite CreateWhiteSwordBeamLeft()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 16, 16, 1, new Point(32, 32));
        }
        public ISprite CreateWhiteSwordBeamDown()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 16, 16, 1, new Point(32, 48));
        }
        public ISprite CreateMagicSwordBeamUp()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 16, 16, 1, new Point(48, 0));
        }
        public ISprite CreateMagicSwordBeamRight()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 16, 16, 1, new Point(48, 16));
        }
        public ISprite CreateMagicSwordBeamLeft()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 16, 16, 1, new Point(48, 32));
        }
        public ISprite CreateMagicSwordBeamDown()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 16, 16, 1, new Point(48, 48));
        }
    }
}