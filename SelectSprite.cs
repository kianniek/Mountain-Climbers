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

            if (inputHelper.KeyPressed(ButtonManager.Left_BigPlayer) || inputHelper.KeyPressed(ButtonManager.Left_SmallPlayer))
            {
                position = new Vector2(0, 725);
            }

            if (inputHelper.KeyPressed(ButtonManager.Right_BigPlayer) || inputHelper.KeyPressed(ButtonManager.Right_SmallPlayer))
            {
                position = new Vector2(1150, 725);
            }
            
            if (inputHelper.KeyPressed(ButtonManager.Down_BigPlayer) || inputHelper.KeyPressed(ButtonManager.Down_SmallPlayer))
            {
                position = new Vector2(630, 875);
            }
        }
    }
}
