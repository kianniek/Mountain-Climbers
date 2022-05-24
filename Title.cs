using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BaseProject
{
    class Title : TextGameObject
    {
        public Title() : base("GameFont")
        {
            color = Color.Black;
            Text = "Climbing Heroes";
        }
    }
}
