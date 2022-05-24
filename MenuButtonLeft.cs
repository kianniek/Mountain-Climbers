using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BaseProject
{
    class MenuButton : SpriteGameObject
    {
        public MenuButton(string assetName, Vector2 newPosition) : base(assetName)
        {
            position = newPosition; 
        }
    }
}
