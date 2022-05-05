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
    public bool canMove, beingHeld, hitLeftWall, hitRightWall;
    public SmallPlayer(LevelGenerator levelGen) : base("Player")
    {
        origin = new Vector2(Center.X, Center.Y - Center.Y / 2);
        this.levelGen = levelGen;
    }
    public override void Update(GameTime gameTime)
    {
        mPressed = false;

        base.Update(gameTime);

        CollisonWithGround();
        hitClimbWall = CollisonWithRope() || CollisonWithClimebleWall();
    }
    public void CollisonWithGround()
    {
        hitLeftWall = false;
        hitRightWall = false;
        for (var x = 0; x < levelGen.tiles.GetLength(0); x++)
        {
            for (var y = 0; y < levelGen.tiles.GetLength(1); y++)
            {
                var tile = levelGen.tiles[x, y];
                if (tile == null || tile == this || tile.Id == Tags.Interactible.ToString())
                    continue;

                if (this.Position.X + this.Width / 2 > tile.Position.X && this.Position.X < tile.Position.X + tile.Width
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

                    else if(!beingHeld)
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
                    return true;
                }
            }
        }
        return false;
    }
    public bool CollisonWithClimebleWall()
    {
        for (var x = 0; x < levelGen.tiles.GetLength(0); x++)
        {
            for (var y = 0; y < levelGen.tiles.GetLength(1); y++)
            {
                var tile = levelGen.tiles[x, y];

                if (tile == null || tile == this || tile.Sprite.Sprite.Name != "Tile_ClimebleLeftverticalBlock")
                    continue;

                if (this.Position.X + this.Width / 1.5f > tile.Position.X &&
                    this.Position.X < tile.Position.X + tile.Width &&
                    this.Position.Y + this.Height > tile.Position.Y &&
                    this.Position.Y < tile.Position.Y + tile.Height
                    )
                {
                    return true;
                }
            }
        }
        return false;
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.IsKeyDown(Keys.RightShift))
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

            if (inputHelper.IsKeyDown(Keys.Up))
            {
                velocity.Y = -100;
            }
            if (inputHelper.IsKeyDown(Keys.Down))
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


    }
    internal void PickedUp(Vector2 grabPosition)
    {
        velocity = Vector2.Zero;
        position = grabPosition;
        canMove = false;
        beingHeld = true;


    }
}
