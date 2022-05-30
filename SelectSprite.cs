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
            position = new Vector2(0, 725);
            scale = 2;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Left))
            {
                position = new Vector2(0, 725);
            }

            if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Right))
            {
                position = new Vector2(1150, 725);
            }
            
            if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Down))
            {
                position = new Vector2(630, 875);
            }
        }
    }
}
