using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

//Dion & Thimo
namespace BaseProject.GameObjects
{
    public class BigPlayer : HeadPlayer
    {
        readonly LevelGenerator levelGen;
        readonly SmallPlayer smallPlayer;
        readonly ThrowDirection throwDirection;

        private float directionIncrease;

        public bool holdingPlayer;
        public BigPlayer(LevelGenerator levelGen, SmallPlayer smallPlayer) : base("player2")
        {
            origin = new Vector2(Center.X, Center.Y / 4);
            this.levelGen = levelGen;
            this.smallPlayer = smallPlayer;
            throwDirection = new ThrowDirection(this, smallPlayer);
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            for (var x = 0; x < levelGen.tiles.GetLength(0); x++)
            {
                for (var y = 0; y < levelGen.tiles.GetLength(1); y++)
                {
                    var tile = levelGen.tiles[x, y];
                    if (tile == null || tile == this)
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

            base.Update(gameTime);
            velocity.Y += gravity;

            if (holdingPlayer)
            {
                GrabPlayer();
            }
            else
            {
                smallPlayer.canMove = true;
            }
            directionIncrease = 3 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            throwDirection.Update(gameTime);
        }
        public override void hitWaterfall()
        {
            base.hitWaterfall();
        }




        public override void HandleInput(InputHelper inputHelper)
        {
            velocity.X = 0;
            if (inputHelper.IsKeyDown(Keys.A))
            {
                left = true;
                right = false;
                //effective = SpriteEffects.FlipHorizontally;
                Mirror = true;
                velocity.X = -100;
            }
            if (inputHelper.IsKeyDown(Keys.D))
            {
                right = true;
                left = false;
                //effective = SpriteEffects.None;
                Mirror = false;
                velocity.X = 100;
            }

            if (inputHelper.KeyPressed(Keys.E))
            {
                holdingPlayer = false;
                //smallPlayer.stand = false;
                if (smallPlayer.CollidesWith(this))
                {
                    holdingPlayer = !holdingPlayer;
                    smallPlayer.stand = false;
                }
            }

            if(holdingPlayer)
            {
                if (inputHelper.IsKeyDown(Keys.O))
                {
                    throwDirection.DecreaseAngle(directionIncrease);
                }
                if (inputHelper.IsKeyDown(Keys.P))
                {
                    throwDirection.IncreaseAngle(directionIncrease);
                }
                if (inputHelper.IsKeyDown(Keys.X))
                {
                    throwDirection.ThrowPlayer();
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
        public void GrabPlayer()
        {
            smallPlayer.pickedUp(new Vector2(position.X, position.Y - 80));
            /*if (smallPlayer.beingHeld)
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
            }*/
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            throwDirection.Draw(gameTime, spriteBatch);
        }
    }
}

