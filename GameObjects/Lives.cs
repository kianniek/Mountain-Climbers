using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject
{
    public class Lives : SpriteGameObject
    {
        public Lives(string assetName, Vector2 startPosition) : base(assetName)
        {
            position = startPosition;
            velocity.Y = 0;
        }
    }
}
