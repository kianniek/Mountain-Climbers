using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject
{
    class CreditsButton : SpriteGameObject
    {
        public CreditsButton(string assetName, Vector2 newPosition) : base(assetName)
        {
            position = newPosition;
        }
    }
}
