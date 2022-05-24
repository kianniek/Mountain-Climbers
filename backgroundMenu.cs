using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BaseProject
{
    class backgroundMenu : SpriteGameObject
    {
        public backgroundMenu() : base("mountains_juiste")
        {
            position = new Vector2(-800, -180);   
        }
    }
}
