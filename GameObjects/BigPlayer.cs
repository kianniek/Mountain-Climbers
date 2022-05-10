using BaseProject.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;


//Dion & Thimo
namespace BaseProject
{
    public class BigPlayer : HeadPlayer
    {
        readonly SmallPlayer smallPlayer;

        public Lives[] livesBig;
        public Lives[] noLives;
        public int livesPlayer;

        public bool holdingPlayer;
        public BigPlayer(Tile[,] worldTiles, SmallPlayer smallPlayer) : base("player2", worldTiles)
        {
            origin = new Vector2(Center.X, Center.Y / 4);
            this.smallPlayer = smallPlayer;

            livesPlayer = 2;
            noLives = new Lives[livesPlayer * 2];
            livesBig = new Lives[livesPlayer];
        }

        public override void Update(GameTime gameTime)
        {
            zPressed = false;

            if (stand && !left && !right)
            {
               hitClimbWall = CollisonWithRope() || CollisonWith(Tags.ClimebleWall);
            }
            

            if (CollisonWith(Tags.Lava))
            {
                position = LastSavedPos;
            }

            if (holdingPlayer)
            {
                GrabPlayer();
                if (smallPlayer.hitRightWall)
                {
                    right = false;
                }
                if (smallPlayer.hitLeftWall)
                {
                    left = false;
                }
            }
            else
            {
                smallPlayer.canMove = true;
                smallPlayer.beingHeld = false;
            }

            base.Update(gameTime);
            BreakeblePlatform breakebleplatform = CollisionWithBreakingPlatform();
            if (breakebleplatform != null)
            {
                breakebleplatform.isBreaking = true;
            }

            CollisonWithGround();
        }
        public void GrabPlayer()
        {
            smallPlayer.PickedUp(new Vector2(position.X, position.Y - smallPlayer.Height));

            if (smallPlayer.beingHeld)
            {
                if (left)
                {
                    smallPlayer.left = true;
                    smallPlayer.Mirror = true;
                }
                if (right)
                {
                    smallPlayer.right = true;
                    smallPlayer.Mirror = false;
                }
            }
        }
        public void CollisonWithGround()
        {
            for (var x = 0; x < WorldTiles.GetLength(0); x++)
            {
                for (var y = 0; y < WorldTiles.GetLength(1); y++)
                {
                    var tile = WorldTiles[x, y];
                    if (tile == null)
                        continue;

                    if (this.Position.X + this.Width / 2 > tile.Position.X &&
                        this.Position.X < tile.Position.X + tile.Width / 2 &&
                        this.Position.Y + this.Height > tile.Position.Y &&
                        this.Position.Y < tile.Position.Y + tile.Height)
                    {
                        var mx = (this.Position.X - tile.Position.X);
                        var my = (this.Position.Y - tile.Position.Y);

                        if (Math.Abs(mx) > Math.Abs(my))
                        {
                            if (mx > 0 && this.Velocity.X < 0)
                            {
                                this.velocity.X = 0;
                                this.position.X = tile.Position.X + tile.Width / 2;
                            }
                            else if (mx < 0 && this.Velocity.X > 0)
                            {
                                this.position.X = tile.Position.X - this.Width / 2;
                                this.velocity.X = 0;

                            }
                        }
                        else
                        {
                            if (my > 0 && this.velocity.Y < 0)
                            {
                                this.velocity.Y = 0;
                                this.position.Y = tile.Position.Y + tile.Height;
                            }
                            else if (my < 0 && this.velocity.Y > 0)
                            {
                                this.velocity.Y = 0;
                                this.position.Y = tile.Position.Y - this.Height;
                                this.stand = true;
                            }
                        }
                    }
                }
            }
        }
        public bool CollisonWithRope()
        {
            for (var x = 0; x < WorldTiles.GetLength(0); x++)
            {
                for (var y = 0; y < WorldTiles.GetLength(1); y++)
                {
                    var tile = WorldTiles[x, y];

                    if (tile == null || tile.Sprite.Sprite.Name != "RopeSegment")
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

        public override void Knockback()
        {
            base.Knockback();
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (inputHelper.IsKeyDown(Keys.LeftShift))
            {
                horizontalSpeed = sprintingSpeed;
            }
            else
            {
                horizontalSpeed = walkingSpeed;
            }

            //Player is climbing the wall by hitting a climbing wall or rope and pressing Z
            if (hitClimbWall && zPressed)
            {
                Climb();

                if (inputHelper.IsKeyDown(Keys.Q))
                {
                    velocity.Y = -100;
                }
                if (inputHelper.IsKeyDown(Keys.S))
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
                if (inputHelper.KeyPressed(Keys.W))
                {
                    stand = false;
                    jump = true;
                }
                if (inputHelper.IsKeyDown(Keys.Z))
                {
                    zPressed = true;
                }
            }

            if (!hitClimbWall)
            {
                if (inputHelper.IsKeyDown(Keys.A))
                {
                    left = true;
                    Mirror = true;
                }
                if (inputHelper.IsKeyDown(Keys.D))
                {
                    right = true;
                    Mirror = false;
                }
            }
            

            if (inputHelper.KeyPressed(Keys.E))
            {
                holdingPlayer = false;
                //smallPlayer.stand = false;
                if (smallPlayer.CollidesWith(this))
                {
                    holdingPlayer = true;
                }
            }
        }
        public BreakeblePlatform CollisionWithBreakingPlatform()
        {
            for (var x = 0; x < WorldTiles.GetLength(0); x++)
            {
                for (var y = 0; y < WorldTiles.GetLength(1); y++)
                {
                    var tile = WorldTiles[x, y];

                    if (tile == null || tile.Id != Tags.BreakeblePlatform.ToString())
                        continue;

                    if (this.Position.X + this.Width / 2f > tile.Position.X && this.Position.X < tile.Position.X + tile.Width / 2f
                        && this.Position.Y + this.Height > tile.Position.Y && this.Position.Y < tile.Position.Y + tile.Height)
                    {
                        return null;//(BreakeblePlatform)tile;
                    }
                }
            }
            return null;
        }
    }
}