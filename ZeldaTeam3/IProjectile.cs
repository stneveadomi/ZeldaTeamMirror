﻿namespace Zelda
{
    public interface IProjectile:  ICollideable, IHaltable, IDrawable
    {
        bool Halted { get; }

        void Reflect(Direction direction);
    }
}
