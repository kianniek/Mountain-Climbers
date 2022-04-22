using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject
{
    class ClimbWall : SpriteGameObject
    {
        public ClimbWall(string assetName, Vector2 climbPosition) : base(assetName)
        {
            position = climbPosition;
        }
    }
}
