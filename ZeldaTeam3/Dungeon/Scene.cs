﻿using System.Collections.Generic;
using System.Linq;

namespace Zelda.Dungeon
{
    public class Scene : IDrawable
    {
        private const int ThrottleFrameDuration = 50;
        private readonly Room _room;
        private readonly IPlayer _player;
        private readonly Dictionary<IEnemy, int> _enemiesAttackThrottle = new Dictionary<IEnemy, int>();
        private int _enemyCount = -1;
        //put an IProjectile list here? Check if it's halted here

        public Scene(Room room, IPlayer player)
        {
            _room = room;
            _player = player;
            _player.Projectiles = new List<IProjectile>();
            //_room.Enemies
        }


        public void Reset()
        {
            _enemyCount = _room.Enemies.Count;
        }

        public void SpawnEnemies()
        {
            if (_enemyCount == -1) _enemyCount = _room.Enemies.Count;
            for (var i = 0; i < _enemyCount; i++)
            {
                _room.Enemies[i].Spawn();
            }
        }

        public void Update()
        {
            foreach (var roomDrawable in _room.Drawables)
            {
                roomDrawable.Update();
            }

            List<IProjectile> projectileCollisions = new List<IProjectile>();

            foreach (var roomEnemy in _room.Enemies)
            {

                roomEnemy.Update();
                //For each roomEnemy.projectiles
                //How to remove item if you're iterating over them
                //projectile.Update();

                foreach(var projectile in roomEnemy.Projectiles)
                {
                    projectile.Update();
                }

                int k = 0;
                while (k < roomEnemy.Projectiles.Count)
                {
                    if (roomEnemy.Projectiles.ElementAt(k).Halted)
                    {
                        roomEnemy.Projectiles.RemoveAt(k);
                    }
                    else
                    {
                        projectileCollisions.Add(roomEnemy.Projectiles.ElementAt(k));
                        //if the projectile was still valid, then we'll need to check it's collisions
                    }
                    k++;
                }
                //Above loop checks all of the enemy projectiles, for each enemy

              
                //Concatenate the remaining arrays -> Then check all collisions at once

                //Do collision detection of each enemy projectile
                //Projectile.CollidesWith(IEnemy)
                //Projectile.CollidesWith(Player)
                
                    ///SCENE Handles removal, link/enemy do not care after they throw it out there

                //Check Player for valid projectiles

                foreach (var roomCollidable in _room.Collidables)
                {
                    if (!roomCollidable.CollidesWith(roomEnemy.Bounds)) continue;

                    roomCollidable.EnemyEffect(roomEnemy).Execute();
                }

                if (_player.Alive && _player.UsingPrimaryItem && !_enemiesAttackThrottle.ContainsKey(roomEnemy) && _player.SwordCollision.CollidesWith(roomEnemy.Bounds))
                {
                    _player.SwordCollision.EnemyEffect(roomEnemy).Execute();
                    if (!roomEnemy.Alive) _enemyCount--;
                    _enemiesAttackThrottle[roomEnemy] = ThrottleFrameDuration;
                }

                if (roomEnemy.Alive && roomEnemy.CollidesWith(_player.BodyCollision.Bounds))
                {
                    roomEnemy.PlayerEffect(_player).Execute();
                }
            }

            foreach (var roomCollidable in _room.Collidables)
            {
                if (roomCollidable.CollidesWith(_player.BodyCollision.Bounds))
                    roomCollidable.PlayerEffect(_player).Execute();

                int j = 0;
                while (j < _player.Projectiles.Count)
                {
                    if (_player.Projectiles.ElementAt(j).Halted)
                    {
                        _player.Projectiles.RemoveAt(j);
                    }
                    else
                    {
                        projectileCollisions.Add(_player.Projectiles.ElementAt(j));
                    }
                    j++;
                }
                //Above loop checks player for projectiles, and then determines if any are invalid

            }

 
            foreach (var projectile in projectileCollisions)
            {
                //check collisions against enemies
                foreach(var enemy in _room.Enemies)
                {
                    projectile.EnemyEffect(enemy).Execute();
                    //projectile hit enemy
                }

                foreach(var collidable in _room.Collidables)
                {
                    collidable.ProjectileEffect(projectile).Execute();
                    //projectile hit object

                }
                

            }
                //Run the valid projectiles (that is, the ones that have not hit an enemy or player, or were Halted in the last call) and check collisions

            

            // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator (LINQ is slow here)
            foreach (var key in _enemiesAttackThrottle.Keys.ToList())
            {
                if (_enemiesAttackThrottle[key]-- == 0) _enemiesAttackThrottle.Remove(key);
            }
        }

        public void Draw()
        {
            foreach (var roomDrawable in _room.Drawables)
            {
                roomDrawable.Draw();
            }

            foreach (var roomEnemy in _room.Enemies)
            {
                roomEnemy.Draw();
                //foreach Projectile in Enemy
                //draw
            }
        }
    }
}
