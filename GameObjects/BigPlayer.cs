using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

//Dion & Thimo
namespace BaseProject
{
    class BigPlayer : HeadPlayer
    {

        public BigPlayer() : base("player2")
        {
            origin = new Vector2(Center.X, Center.Y + sprite.Height/2);
        }

        public override void Update(GameTime gameTime)
        {
            //Console.WriteLine(velocity.Y);
            base.Update(gameTime);
        }

        public override void hitWaterfall()
        {
           base.hitWaterfall();
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.IsKeyDown(Keys.A))
            {
                left = true;
                //effective = SpriteEffects.FlipHorizontally;
                Mirror = true;
            }
            if (inputHelper.IsKeyDown(Keys.D))
            {
                right = true;
                //effective = SpriteEffects.None;
                Mirror = false;
            }

            if (stand)
            {
                if (inputHelper.KeyPressed(Keys.W))
                {
                    stand = false;
                    jump = true;
                }
            }
        }
    }
}
