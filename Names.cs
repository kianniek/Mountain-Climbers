using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject
{
    class Names : TextGameObject
    {
        public Names(Vector2 newPosition, string name) : base("GameFont")
        {
            color = Color.Black;
            position = newPosition;
            text = name;
        }
    }
}
