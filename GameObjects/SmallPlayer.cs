﻿using System;
using BaseProject;
using BaseProject.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Collections.Generic;

namespace BaseProject
{
    public class SmallPlayer : HeadPlayer
    {
        public bool canMove, beingHeld, hitLeftWall, hitRightWall;

        public Lives[] livesSmall;
        public Lives[] noLives;
        public int livesPlayer;

        public Vector2 buttonIndicatorPos;

        public SmallPlayer(Tile[,] worldTiles) : base("Player", worldTiles)
        {

            origin = new Vector2(Center.X, Center.Y - Center.Y / 2);
            livesPlayer = 2;
            noLives = new Lives[livesPlayer * 2];
            livesSmall = new Lives[livesPlayer];
        }

        public override void Update(GameTime gameTime)
        {
            buttonIndicatorPos = position + new Vector2(0, Height / 2);
            mPressed = false;

            if (stand)
            {
                hitClimbWall = CollisonWithRope() || CollisonWith(Tags.ClimebleWall);
            }

            CollisonWithLevelObjecs();

            base.Update(gameTime);

            CollisonWithGround();
        }
        public void CollisonWithGround()
        {
            hitLeftWall = false;
            hitRightWall = false;
            for (var x = 0; x < WorldTiles.GetLength(0); x++)
            {
                for (var y = 0; y < WorldTiles.GetLength(1); y++)
                {
                    var tile = WorldTiles[x, y];
                    if (tile == null)
                        continue;

                    var tileType = tile.GetType();

                    if (this.Position.X + this.Width / 2 > tile.Position.X &&
                        this.Position.X < tile.Position.X + tile.Width / 2 &&
                        this.Position.Y + this.Height > tile.Position.Y &&
                        this.Position.Y < tile.Position.Y + tile.Height)
                    {
                        var mx = (this.Position.X - tile.Position.X);
                        var my = (this.Position.Y - tile.Position.Y);
                        if (Math.Abs(mx) > Math.Abs(my))
                        {
                            if (Math.Abs(mx) > Math.Abs(my))
                            {
                                if (mx > 0)
                                {
                                    this.velocity.X = 0;
                                    this.position.X = tile.Position.X + this.Width / 4;
                                }
                                if (mx < 0)
                                {
                                    this.position.X = tile.Position.X - this.Width / 2;
                                    this.velocity.X = 0;
                                }
                            }

                            if (beingHeld)
                            {
                                if (mx > 0)
                                {
                                    hitLeftWall = true;
                                }
                                else if (mx < 0)
                                {
                                    hitRightWall = true;
                                }
                            }
                        }

                        else if (!beingHeld)
                        {
                            if (my > 0)
                            {
                                this.velocity.Y = 0;
                                this.position.Y = tile.Position.Y + tile.Height;
                            }
                            if (my < 0)
                            {
                                this.velocity.Y = 0;
                                this.position.Y = tile.Position.Y - this.Height;
                                this.stand = true;
                            }

                        }

                    }

                    if (tileType == typeof(ClimbWall))
                    {

                    }

                }

            }
        }
        public bool CollisonWithRope()
        {
            for (int x = 0; x < levelManager.CurrentLevel().LevelObjects.Children.Count; x++)
            {
                var obj = (SpriteGameObject)levelManager.CurrentLevel().LevelObjects.Children[x];
                var tileType = obj.GetType();
                if (tileType == typeof(Rope))
                {
                    if (CollidesWith(obj))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void CollisonWithLevelObjecs()
        {
            for (int x = 0; x < levelManager.CurrentLevel().LevelObjects.Children.Count; x++)
            {
                var obj = (SpriteGameObject)levelManager.CurrentLevel().LevelObjects.Children[x];
                var tileType = obj.GetType();

                if (tileType == typeof(Rope))
                {
                    if (CollidesWith(obj))
                    {
                        hitRope = true;
                    }
                    else { hitRope = false; }
                }
                if (tileType == typeof(Lava))
                {
                    if (CollidesWith(obj))
                    {
                        isDead = true;
                    }
                }
            }
        }
        public bool CollisonWith(GameObject.Tags Tag)
        {
            string id = Tag.ToString();
            for (var x = 0; x < WorldTiles.GetLength(0); x++)
            {
                for (var y = 0; y < WorldTiles.GetLength(1); y++)
                {
                    var tile = WorldTiles[x, y];

                    if (tile == null || tile.Id != id)
                        continue;

                    if (this.Position.X + this.Width / 2 > tile.Position.X && this.Position.X < tile.Position.X + tile.Width / 2
                        && this.Position.Y + this.Height > tile.Position.Y && this.Position.Y < tile.Position.Y + tile.Height)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (inputHelper.IsKeyDown(ButtonManager.Sprint_SmallPlayer))
            {
                horizontalSpeed = sprintingSpeed;
            }
            else
            {
                horizontalSpeed = walkingSpeed;
            }

            //Small Player is climbing a wall
            if (hitClimbWall)
            {
                Climb();


                if (inputHelper.IsKeyDown(ButtonManager.Jump_SmallPlayer))
                {
                    velocity.Y = -100;
                }
                if (inputHelper.IsKeyDown(ButtonManager.Down_SmallPlayer))
                {
                    velocity.Y = 100;
                }
            }
            else
            {
                NotClimbing();
            }

            if (stand)
            {
                if (inputHelper.KeyPressed(ButtonManager.Jump_SmallPlayer))
                {
                    //stand = false;
                    jump = true;
                }
                if (inputHelper.IsKeyDown(Keys.M))
                {
                    mPressed = true;
                }
            }

            if (inputHelper.IsKeyDown(ButtonManager.Left_SmallPlayer))
            {
                left = true;
                Mirror = true;
            }
            if (inputHelper.IsKeyDown(ButtonManager.Right_SmallPlayer))
            {
                right = true;
                Mirror = false;
            }
        }
        internal void PickedUp(Vector2 grabPosition)
        {
            velocity = Vector2.Zero;
            position = grabPosition;
            canMove = false;
            beingHeld = true;
        }
    }
}
