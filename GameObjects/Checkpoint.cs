using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects
{
    public class Checkpoint : SpriteGameObject
    {
        public Checkpoint() : base("CP")
        {
            position = new Vector2(800, 100);
        }

    }
}
