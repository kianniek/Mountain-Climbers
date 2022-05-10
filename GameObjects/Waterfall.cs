using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject
{
    //Dion
    class Waterfall : SpriteGameObject
    { 
        public Waterfall(Vector2 waterPosition) : base("Waterfall200")
        {
            position = waterPosition;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
