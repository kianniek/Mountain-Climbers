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
        public bool isDead;
        public float gravity;
        public bool left, right, jump, stand, hitClimbWall, zPressed, mPressed;

        public static float JumpForce = 460;
        public float horizontalSpeed = 175;
        public static float walkingSpeed = 175;
        public static float sprintingSpeed = 500;

        public HeadPlayer(string assetName) : base(assetName)
        {
            position.Y = GameEnvironment.Screen.Y / 1.4f;
            position.X = 10;
            gravity = 10f;
        }

        public override void Update(GameTime gameTime)
        {
            velocity.X = 0;

            if (jump)
            {
                jump = false;
                stand = false;
                velocity.Y = -JumpForce;
            }

            if (left)
            {
                velocity.X = -horizontalSpeed;
                left = false;
            }
            if (right)
            {
                velocity.X = horizontalSpeed;
                right = false;
            }

            if (position.Y > 1080)
            {
                isDead = true;
            }

            base.Update(gameTime);
            velocity.Y += gravity;
        }

        //Roep deze functie aan als de speler normaal springt en de waterval raakt,
        //maar zodra je de pickup gebruikt, roep deze niet aan.
        public virtual void HitWaterfall()
        {
            velocity.Y = 520;
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

        public virtual void Climb()
        {
            left = false;
            right = false;
            stand = true;
            gravity = 0;
            velocity.Y = 0;
            velocity.X = 0;
        }

        public virtual void NotClimbing()
        {
            gravity = 10f;
        }

        ////Player is touching the ground
        ////Deze methode kun je gebruiken voor elk object dat collision heeft met de player als die op platform staat.
        //public virtual void OnGround(float standPosition)
        //{
        //    if (position.Y > standPosition)
        //    {
        //        stand = true;
        //        position.Y = standPosition;
        //        velocity.Y = 0;
        //    }
        //}

        ////Deze kun je gebruiken bij een wall collision aan de linkerkant 
        //public virtual void hitWallLeft(float leftPosition)
        //{
        //    if (position.X <= leftPosition)
        //    {
        //        position.X = leftPosition;
        //    }
        //}

        ////Deze kun je gebruiken bij een wall collision aan de rechterkant
        //public virtual void hitWallRight(float rightPosition)
        //{
        //    if (position.X >= rightPosition)
        //    {
        //        position.X = rightPosition;
        //    }
        //}
    }
}
