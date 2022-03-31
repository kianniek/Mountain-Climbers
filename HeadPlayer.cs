using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject
{
    //Dion
    class HeadPlayer : SpriteGameObject
    {
        public float gravity;
        public bool left, right, jump, stand;
        public HeadPlayer(string assetName) : base(assetName)
        {
            position.Y = 0;
            gravity = 10f;
            origin = Center;
        }

        public override void Update(GameTime gameTime)
        {
            velocity.X = 0;
            gravity = 10f;

            if (jump)
            {
                jump = false;
                stand = false;
                velocity.Y = -460;
            }

            if (left)
            {
                velocity.X = -175;
                left = false;
            }
            if (right)
            {
                velocity.X = 175;
                right = false;
            }

            base.Update(gameTime);
            velocity.Y += gravity;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (inputHelper.IsKeyDown(Keys.Left))
            {
                left = true;
                Mirror = true;
            }
            if (inputHelper.IsKeyDown(Keys.Right))
            {
                right = true;
                Mirror = false;
            }

            if (stand)
            {
                if (inputHelper.KeyPressed(Keys.Up))
                {
                    stand = false;
                    jump = true;
                }
            }
        }

        //Player is touching the ground
        //Deze methode kun je gebruiken voor elk object dat collision heeft met de player als die op platform staat.
        public virtual void OnGround(float standPosition)
        {
            if (position.Y > standPosition)
            {
                stand = true;
                position.Y = standPosition;
                velocity.Y = 0;
            }
        }

        //Deze kun je gebruiken bij een wall collision aan de linkerkant 
        public virtual void hitWallLeft(float leftPosition)
        {
            if (position.X <= leftPosition)
            {
                position.X = leftPosition;
            }
        }

        //Deze kun je gebruiken bij een wall collision aan de rechterkant
        public virtual void hitWallRight(float rightPosition)
        {
            if (position.X >= rightPosition)
            {
                position.X = rightPosition;
            }
        }
    }
}
