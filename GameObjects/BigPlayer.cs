using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

//Dion & Thimo
namespace BaseProject
{
    class BigPlayer : HeadPlayer
    {
        LevelGenerator levelGen;
        public BigPlayer(LevelGenerator levelGen) : base("player2")
        {
            origin = new Vector2(Center.X, Center.Y/4);
            this.levelGen = levelGen;
        }

        public override void Update(GameTime gameTime)
        {
            //Console.WriteLine(velocity.Y);
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
        }

        public override void HandleInput(InputHelper inputHelper)
        {
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

            if (stand)
            {
                if (inputHelper.KeyPressed(Keys.W))
                {
                    stand = false;
                    jump = true;
                }
            }
        }
    }
}
