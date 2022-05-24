using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BaseProject
{
    class SelectSprite : SpriteGameObject
    {
        public SelectSprite() : base("Player")
        {
            position = new Vector2(-400, 1250);
            scale = 2;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Left))
            {
                position = new Vector2(-400, 1250);
            }

            if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Right))
            {
                position = new Vector2(700, 1250);
            }
        }
    }
}
