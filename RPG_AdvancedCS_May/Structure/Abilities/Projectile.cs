﻿using System;
using RPG_AdvancedCS_May.GameEngine;
using RPG_AdvancedCS_May.Interfaces;

namespace RPG_AdvancedCS_May.Structure
{
    public abstract class Projectile : GameObject, IMoveable   //Make an interface?   //TO DO: 
    {
        //Description: Spells, Arrows, etc..
        protected Projectile(int sizeX, int sizeY, int x, int y) : base(sizeX, sizeY, x, y)
        {
        }

        public Direction Direction { get; set; }
        public int MovementSpeed { get; set; }
        public void Move()
        {
            throw new NotImplementedException();
        }
    }
}
