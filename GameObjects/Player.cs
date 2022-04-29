using System;
using BaseProject;
using BaseProject.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//Dion
class SmallPlayer : HeadPlayer
{
    LevelGenerator levelGen;
    public bool canMove, beingHeld;
    public SmallPlayer(LevelGenerator levelGen) : base("Player")
    {
        origin = new Vector2(Center.X, Center.Y / 2);
        this.levelGen = levelGen;
    }
    public override void Update(GameTime gameTime)
    {
        mPressed = false;

        base.Update(gameTime);

        CollisonWithGround();
        CollisonWithRope();
    }
    public override void HandleInput(InputHelper inputHelper)
    {
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
                //stand = false;
                jump = true;
            }
            if (inputHelper.IsKeyDown(Keys.M))
            {
                mPressed = true;
            }
        }

        //Small Player is climbing a wall
        if (hitClimbWall && mPressed)
        {
            Climb();

            if (inputHelper.IsKeyDown(Keys.Up))
            {
                velocity.Y = -20;
            }
            if (inputHelper.IsKeyDown(Keys.Down))
            {
                velocity.Y = 20;
            }
        }
        else
        {
            notClimbing();
        }
    }
    public void CollisonWithRope()
    {
        for (var x = 0; x < levelGen.tiles.GetLength(0); x++)
        {
            for (var y = 0; y < levelGen.tiles.GetLength(1); y++)
            {
                var tile = levelGen.tiles[x, y];
                if (tile == null || tile == this || tile.Sprite.Sprite.Name != "RopeSegment")
                    continue;

                if (this.Position.X + this.Width / 2 > tile.Position.X && this.Position.X < tile.Position.X + tile.Width / 2
                    && this.Position.Y + this.Height > tile.Position.Y && this.Position.Y < tile.Position.Y + tile.Height)
                {
                    var mx = (this.Position.X - tile.Position.X);
                    var my = (this.Position.Y - tile.Position.Y);

                    hitClimbWall = true;
                }
                else
                {
                    hitClimbWall = false;
                }
            }
        }
    }
    public void CollisonWithGround()
    {
        for (var x = 0; x < levelGen.tiles.GetLength(0); x++)
        {
            for (var y = 0; y < levelGen.tiles.GetLength(1); y++)
            {
                var tile = levelGen.tiles[x, y];
                if (tile == null || tile == this || tile.Sprite.Sprite.Name == "RopeSegment")
                    continue;

                if (this.Position.X + this.Width / 2 > tile.Position.X && this.Position.X < tile.Position.X + tile.Width / 2
                    && this.Position.Y + this.Height > tile.Position.Y && this.Position.Y < tile.Position.Y + tile.Height)
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
    internal void pickedUp(Vector2 grabPosition)
    {
        velocity = Vector2.Zero;
        position = grabPosition;
        canMove = false;
        beingHeld = true;

        //stand = false;
    }
}
