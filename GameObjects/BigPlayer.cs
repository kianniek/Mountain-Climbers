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
        SmallPlayer smallPlayer;

        public bool holdingPlayer;
        public BigPlayer(Tile[,] worldTiles, SmallPlayer smallPlayer) : base("player2", worldTiles)
        {
            origin = new Vector2(Center.X, Center.Y / 4);
            this.smallPlayer = smallPlayer;
        }

        public override void Update(GameTime gameTime)
        {
            //Console.WriteLine(zPressed);
            zPressed = false;

            base.Update(gameTime);

            CollisonWithGround();

            if (holdingPlayer)
            {
                grabPlayer();
            }
            else
            {
                smallPlayer.canMove = true;
            }
        }
        public void CollisonWithGround()
        {
            for (var x = 0; x < worldTiles.GetLength(0); x++)
            {
                for (var y = 0; y < worldTiles.GetLength(1); y++)
                {
                    var tile = worldTiles[x, y];
                    if (tile == null)
                        continue;

                    if (this.Position.X + this.Width > tile.Position.X && this.Position.X < tile.Position.X + tile.Width
                        && this.Position.Y + this.Height > tile.Position.Y && this.Position.Y < tile.Position.Y + tile.Height)
                    {
                        var mx = (this.Position.X - tile.Position.X);
                        var my = (this.Position.Y - tile.Position.Y);

                        if (Math.Abs(mx) > Math.Abs(my))
                        {
                            if (mx > 0 && this.Velocity.X < 0)
                            {
                                this.velocity.X = 0;
                                this.position.X = tile.Position.X + tile.Width;
                            }
                            else if (mx < 0 && this.Velocity.X > 0)
                            {
                                this.position.X = tile.Position.X - this.Width;
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
        public override void HandleInput(InputHelper inputHelper)
        {
            //if ((!hitClimbWall) && (!zPressed))
            //{
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

                if (inputHelper.KeyPressed(Keys.E))
                {
                    holdingPlayer = false;
                    //smallPlayer.stand = false;
                    if (smallPlayer.CollidesWith(this))
                    {
                        holdingPlayer = true;
                    }
                }
            //}
            
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
         
            //Player is climbing the wall
            if (hitClimbWall && zPressed)
            {
                Climb();

                if (inputHelper.IsKeyDown(Keys.Q))
                {
                    velocity.Y = -20;
                }
                if (inputHelper.IsKeyDown(Keys.S))
                {
                    velocity.Y = 20;
                }
            }
            else
            {
                notClimbing();
            }
        }
        public void grabPlayer()
        {
            smallPlayer.pickedUp(new Vector2(position.X, position.Y - 80));
            if (smallPlayer.beingHeld)
            {
                if (left)
                {
                    smallPlayer.left = true;
                    //smallPlayer.effective = SpriteEffects.FlipHorizontally;
                }
                if (right)
                {
                    smallPlayer.right = true;
                    //smallPlayer.effective = SpriteEffects.None;
                }
            }
        }

        public override void Climb()
        {
            base.Climb();
        }
    }
}

