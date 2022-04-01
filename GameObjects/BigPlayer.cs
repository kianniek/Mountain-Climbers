using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

//Dion
namespace BaseProject
{
    class BigPlayer : SpriteGameObject
    {
        SmallPlayer smallPlayer;
        float gravity;
        public bool left, right, jump, stand, holdingPlayer;
        public BigPlayer(SmallPlayer smallPlayer) : base("player2")
        {
            position.Y = 300;
            velocity.Y = 20;
            velocity.X = 500;
            gravity = 10f;
            this.smallPlayer = smallPlayer;
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

            if (holdingPlayer)
            {
                grabPlayer();
            }
            else
            {
                smallPlayer.canMove = true;
            }



        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (inputHelper.IsKeyDown(Keys.A))
            {
                left = true;
                effective = SpriteEffects.FlipHorizontally;
            }
            if (inputHelper.IsKeyDown(Keys.D))
            {
                right = true;
                effective = SpriteEffects.None;
            }

            if (inputHelper.KeyPressed(Keys.E))
            {
                holdingPlayer = false;
                //smallPlayer.stand = false;
                if (smallPlayer.CollidesWith(this) && smallPlayer.stand)
                    {
                    holdingPlayer = true;
                    smallPlayer.stand = false;
                }
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
        public void OnGround(float standPosition)
        {
            if (position.Y >= standPosition)
            {
                gravity = 0;
                //velocity.Y = 0;
                stand = true;
                position.Y = standPosition;
            }
            else
            {
                gravity = 10f;
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
        public void grabPlayer()
        {
            smallPlayer.pickedUp(new Vector2(position.X, position.Y - 80));
            smallPlayer.stand = false;
            if (smallPlayer.beingHeld)
            {
                if (left)
                { 
                    smallPlayer.left = true;
                    //smallPlayer.effective = SpriteEffects.FlipHorizontally;
                }
                if(right)
                {
                    smallPlayer.right = true;
                    //smallPlayer.effective = SpriteEffects.None;
                }
            }
        }
    }
}
