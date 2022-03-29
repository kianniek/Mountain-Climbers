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
        }

        public override void Update(GameTime gameTime)
        {
            //Console.WriteLine(velocity.Y);
            base.Update(gameTime);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

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

        //Player is touching the ground
        //Deze methode kun je gebruiken voor elk object dat collision heeft met de player als die op platform staat.
        public override void OnGround(float standPosition)
        {
            base.OnGround(standPosition);
        }

        //Deze kun je gebruiken bij een wall collision aan de linkerkant 
        public override void hitWallLeft(float leftPosition)
        {
            /*if (position.X <= leftPosition)
            {
                position.X = leftPosition;
            }*/
            base.hitWallLeft(leftPosition);
        }

        //Deze kun je gebruiken bij een wall collision aan de rechterkant
        public override void hitWallRight(float rightPosition)
        {
            base.hitWallRight(rightPosition);
        }
    }
}
