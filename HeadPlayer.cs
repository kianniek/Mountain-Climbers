using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject
{
    //Dion
    public class HeadPlayer : SpriteGameObject
    {
        public float gravity;
        public bool left, right, jump, stand, hitClimbWall, zPressed, mPressed;

        protected Tile[,] worldTiles;
        
        public HeadPlayer(string assetName, Tile[,] worldTiles) : base(assetName)
        {
            position.Y = GameEnvironment.Screen.Y / 1.5f;
            gravity = 10f;

            this.worldTiles = worldTiles;
        }

        public override void Update(GameTime gameTime)
        {
            velocity.X = 0;

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

        //Roep deze functie aan als de speler normaal springt en de waterval raakt,
        //maar zodra je de pickup gebruikt, roep deze niet aan.
        public virtual void hitWaterfall()
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

        public virtual void notClimbing()
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

        public void GoToNewLevel(Tile[,] tiles, Vector2 pos)
        {
            worldTiles = tiles;
            position = pos;
            velocity = Vector2.Zero;
        }
    }
}
