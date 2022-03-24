using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject
{
    class Lives : SpriteGameObject
    {
        public Lives(string assetName, Vector2 startPosition) : base(assetName)
        {
            position = startPosition;
            velocity.Y = 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
