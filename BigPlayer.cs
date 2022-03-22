﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

//Dion
namespace BaseProject
{
    class BigPlayer : SpriteGameObject
    {
        float gravity;
        bool left, right, jump, stand;
        public BigPlayer() : base("player2")
        {
            position.Y = 300;
            velocity.Y = 20;
            velocity.X = 500;
            gravity = 10f;
        }

        public override void Update(GameTime gameTime)
        {
            velocity.X = 0;

            if (jump)
            {
                jump = false;
                stand = false;
                velocity.Y = -400;
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

            if (inputHelper.IsKeyDown(Keys.A))
            {
                left = true;
            }
            if (inputHelper.IsKeyDown(Keys.D))
            {
                right = true;
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

        //Player is touches the ground
        //Deze methode kun je gebruiken voor elk object dat collision heeft met de player als die op platform staat.
        public void OnGround(float standPosition)
        {
            if (position.Y >= standPosition)
            {
                velocity.Y = 0;
                stand = true;
                position.Y = standPosition;
            }
        }

        //Deze kun je gebruiken bij een wall collision aan de linkerkant 
        public void hitWallLeft(float leftPosition)
        {
            if (position.X <= leftPosition)
            {
                position.X = leftPosition;
            }
        }

        //Deze kun je gebruiken bij een wall collision aan de rechterkant
        public void hitWallRight(float rightPosition)
        {
            if (position.X >= rightPosition)
            {
                position.X = rightPosition;
            }
        }
    }
}
