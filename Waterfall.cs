using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject
{
    //Dion
    class Waterfall : SpriteGameObject
    { 
        public Waterfall(string assetName) : base(assetName)
        {
            position.X = 600;
            position.Y = 250;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
