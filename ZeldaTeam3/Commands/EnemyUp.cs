﻿namespace Zelda.Commands
{
    class EnemyUp : ICommand
    {
        private readonly IEnemy[] _enemy;

        public EnemyUp(IEnemy[] Enemy)
        {
            _enemy = Enemy;
        }

        public void Execute()
        {
            foreach (IEnemy enemy in _enemy)
            {
                enemy.MoveUp();
            }
        }

        public override string ToString() => "Enemy: Face/move up";
    }
}
