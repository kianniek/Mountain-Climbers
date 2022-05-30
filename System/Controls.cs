using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BaseProject
{
    class Controls : SpriteGameObject
    {
        public Controls() : base("controls")
        {
            Position = new Vector2(950, 400);
            Origin = Center;
        }
        
    }
}
