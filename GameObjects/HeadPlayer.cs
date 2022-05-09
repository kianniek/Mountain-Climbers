﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject
{
    public class HeadPlayer : SpriteGameObject
    {
        public bool isDead;
        public float gravity;
        public bool left, right, jump, stand, hitClimbWall, zPressed, mPressed, noLeft, noRight;

        public static float JumpForce = 460;
        public float horizontalSpeed = 175;
        public static float walkingSpeed = 175;
        public static float sprintingSpeed = 200;

        public Vector2 LastSavedPos;
        public int savePosTimer;

        public bool knockback;
        public int knockbackForce = 100;

        protected Tile[,] WorldTiles { get; private set; }

        public HeadPlayer(string assetName, Tile[,] worldTiles) : base(assetName)
        {
            position.Y = GameEnvironment.Screen.Y / 1.4f;
            position.X = 10;
            gravity = 10f;
            noLeft = false;
            noRight = false;
            this.WorldTiles = worldTiles;
        }

        public override void Update(GameTime gameTime)
        {
            if (knockback)
            {
                velocity.X = -1 * knockbackForce;
                Console.WriteLine(Velocity.X);
                jump = true;
                knockback = false;
            }
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
            if (stand)
            {
                velocity.X = 0;
            }
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

            //     if (inputHelper.IsKeyDown(Keys.Left))
            //   {
            //     left = true;
            //   Mirror = true;
            //}


            // if (inputHelper.IsKeyDown(Keys.Right))
            //{
            //  right = true;
            //Mirror = false;
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
        
        public void GoToNewLevel(Tile[,] tiles, Vector2 pos)
        {
            WorldTiles = tiles;
            position = pos;
            velocity = Vector2.Zero;
        }
    }
}
