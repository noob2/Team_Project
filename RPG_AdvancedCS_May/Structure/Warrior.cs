﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_AdvancedCS_May.Graphics;
using RPG_AdvancedCS_May.Interfaces;

using System.Windows.Forms;

namespace RPG_AdvancedCS_May.Structure
{
    public class Warrior : CharacterUnit, IMeleeAttacking
    {
        public Warrior(int sizeX, int sizeY, int x, int y, int currentHp, int maxHp, 
            int attackPoints, int defensePoints, int movementSpeed, SpriteType spriteType) 
            : base(sizeX, sizeY, x, y, currentHp, maxHp, attackPoints, defensePoints, movementSpeed, spriteType)
        {
        }

        public MeleeAbility MeleeAttack(int mouseX, int mouseY)
        {
            MessageBox.Show($"MeleeAttack: {mouseX}-{mouseY} \n {this.DetermineAbilityDirection(mouseX, mouseY).ToString()}");
            return new BasicAttack(5,5,5,5);
        }
    }
}
