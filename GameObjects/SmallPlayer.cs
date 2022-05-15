using System;
using BaseProject;
using BaseProject.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BaseProject.GameStates;

//Dion
public class SmallPlayer : HeadPlayer
{
    LevelGenerator levelGen;
    public bool canMove, beingHeld;
    PlayingState state;

    public SmallPlayer(LevelGenerator levelGen, PlayingState playingState) : base("Player")
    {
        origin = new Vector2(Center.X, Center.Y / 2);
        this.levelGen = levelGen;
        state = playingState;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        velocity.X = 0;
        if (!beingHeld)
        {
            if (inputHelper.IsKeyDown(Keys.Left))
            {
                left = true;
                Mirror = true;
                velocity.X = -100;
            }
            if (inputHelper.IsKeyDown(Keys.Right))
            {
                right = true;
                Mirror = false;
                velocity.X = 100;
            }
        }
        if (stand)
        {
            if (inputHelper.KeyPressed(Keys.Up))
            {
                //stand = false;
                jump = true;
            }
        }
        if(!state.bigPlayer.holdingPlayer)
        {
            beingHeld = false;
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (beingHeld)
        {
            Console.WriteLine(state.bigPlayer.left);
            //Console.WriteLine(state.bigPlayer.right);
            if (state.bigPlayer.left)
            {
                left = true;
                this.Mirror = true;
               
            }
            else if (state.bigPlayer.right)
            {
                right = true;
                this.Mirror = false;
            }
        }

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

    public override void hitWaterfall()
    {
        base.hitWaterfall();
    }

    internal void pickedUp(Vector2 grabPosition)
    {
        velocity = Vector2.Zero;
        position = grabPosition;
        canMove = false;
        beingHeld = true;

        //stand = false;
    }
}
