﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_AdvancedCS_May.Interfaces
{
    public interface IUnit : IGameObject
    {
        int CurrentHP { get; set; }
        int MaxHP { get; set; }
        int AttackPoints { get; set; }
        int DefensePoints { get; set; }
        bool IsAlive { get; set; }
    }
}
