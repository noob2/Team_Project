﻿using System;
using System.Collections.Generic;
using System.Linq;
using RPG_AdvancedCS_May.GameEngine;
using RPG_AdvancedCS_May.Graphics;
using RPG_AdvancedCS_May.Interfaces;

namespace RPG_AdvancedCS_May.Structure
{
    public abstract class Unit : GameObject, IUnit, IMoveable, IRenderable
    {
        private int _currentHP;

        public int MaxHP { get; set; }
        public int AttackPoints { get; set; }
        public int DefensePoints { get; set; }
        public bool IsAlive { get; set; }

        public Direction Direction { get; set; }

        public int MovementSpeed { get; set; }

        public SpriteType SpriteType { get; set; }

        public int CurrentHP
        {
            get
            {
                return _currentHP;
            }
            set
            {
                if (value <= 0)
                {
                    this._currentHP = 0;
                    this.IsAlive = false;
                }
                else
                {
                    this._currentHP = value;
                }
            }
        }


        protected Unit(int x, int y, int sizeX, int sizeY, int currentHp, int maxHp, int attackPoints, int defensePoints, int movementSpeed, SpriteType spriteType) : base(x, y, sizeX, sizeY)
        {
            this._currentHP = currentHp;
            this.MaxHP = maxHp;
            this.AttackPoints = attackPoints;
            this.DefensePoints = defensePoints;
            this.MovementSpeed = movementSpeed;
            this.SpriteType = spriteType;
            this.IsAlive = true;
        }

        public void Move()
        {
            this.X += this.MovementSpeed * this.Direction.DirX;
            this.Y += this.MovementSpeed * this.Direction.DirY;
        }

        public virtual ReactionTypeEnum ReactToAbility(AbilityEffectEnum abilityEffect)
        {
            switch (abilityEffect)
            {
                case AbilityEffectEnum.DamagingAbility:
                    return ReactionTypeEnum.TakeDamage;
                case AbilityEffectEnum.HealingAbility:
                    return ReactionTypeEnum.TakeHeal;
                case AbilityEffectEnum.BuffingAbility:
                    return ReactionTypeEnum.TakeBuff;
                case AbilityEffectEnum.DebuffingAbility:
                    return ReactionTypeEnum.TakeDebuff;
                case AbilityEffectEnum.DisplacingAbility:
                    throw new NotImplementedException("Displacing Ability");
                default:
                    throw new ArgumentOutOfRangeException(nameof(abilityEffect), abilityEffect, null);
            }
        }
    }
}
