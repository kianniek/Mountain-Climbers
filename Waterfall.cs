using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject
{
    //Dion
    class Waterfall : SpriteGameObject
    { 
        public Waterfall(string assetName, Vector2 waterPosition) : base(assetName)
        {
            position = waterPosition;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
