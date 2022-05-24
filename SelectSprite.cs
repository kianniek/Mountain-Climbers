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
            position = new Vector2(-700, 530);
            scale = 2;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Z))
            {
                position = new Vector2(-700, 530);
            }

            if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.X))
            {
                position = new Vector2(380, 530);
            }
        }
    }
}
