using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameObjects
{
    public class CuttebleRope : SpriteGameObject
    {
        public int x;
        public int y;
        public LevelGenerator levelGen;
        public bool isOut;

        public CuttebleRope(LevelGenerator levelGen, int x, int y) : base("RopeSegment")
        {
            origin = Center;
            this.y = y;
            this.x = x;
            this.levelGen = levelGen;
        }

    }
}
