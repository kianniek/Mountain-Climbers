using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameObjects
{
    public class Rope : SpriteGameObject
    {
        public Rope() : base("RopeSegment")
        {
            origin = Center;
        }
    }
}
