﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_AdvancedCS_May.Interfaces
{
    interface IPaintInterface
    {
        void AddObject(IRenderable rendObject);

        void RemoveObject(IRenderable rendObject);

        void RedrawObject(IRenderable rendObject);
    }
}
