﻿namespace RPG_AdvancedCS_May.Structure.BoostItems
{
    using Graphics;

    public class Pineapple : Item
    {
        private const int Pineapple_DEFAULT_DAMAGE_BOOST = 0;
        private const int Pineapple_DEFAULT_HEALTH_BOOST = 50;
        private const int Pineapple_DEFAULT_DEFENCE_BOOST = 0;
        private const int PineappleDefaultSizeX = 35;
        private const int PineappleDefaultSizeY = 32;
        private const SpriteType PineappleDefaultSprite = SpriteType.Pineapple;


        public Pineapple(int x, int y) : base(PineappleDefaultSizeX, PineappleDefaultSizeY, x, y, PineappleDefaultSprite, Pineapple_DEFAULT_HEALTH_BOOST, Pineapple_DEFAULT_DAMAGE_BOOST, Pineapple_DEFAULT_DEFENCE_BOOST)
        {
            this.SizeX = PineappleDefaultSizeX;
            this.SizeY = PineappleDefaultSizeY;
        }
    }
}
