using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects
{
    public class Checkpoint : SpriteGameObject
    {
        public Checkpoint(Vector2 pos) : base("CP")
        {
            position = pos;
            origin = new Vector2(Center.X,Center.Y + Center.Y/2);
        }

    }
}
