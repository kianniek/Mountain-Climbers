using System;
using BaseProject;
using BaseProject.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class SmallPlayer : HeadPlayer
{
    LevelGenerator levelGen;
    public bool canMove, beingHeld, hitLeftWall, hitRightWall;

    public Lives[] livesSmall;
    public Lives[] noLives;
    public int livesPlayer;

    public SmallPlayer(LevelGenerator levelGen) : base("Player")
    {
        origin = new Vector2(Center.X, Center.Y - Center.Y / 2);
        this.levelGen = levelGen;

        livesPlayer = 2;
        noLives = new Lives[livesPlayer * 2];
        livesSmall = new Lives[livesPlayer];
    }
    public override void Update(GameTime gameTime)
    {
        mPressed = false;

        hitClimbWall = CollisonWithRope() || CollisonWith(Tags.ClimebleWall);
       
        if (CollisonWith(Tags.Lava))
        {
            knockback = true;
        }

        base.Update(gameTime);
        CollisonWithGround();
        BreakeblePlatform breakebleplatform = CollisonWithBreakingPlatform();
        if (breakebleplatform != null)
        {
            breakebleplatform.isBreaking = true;
        }

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
                if (tile == null || tile == this || tile.Id != Tags.Ground.ToString() && tile.Id != Tags.BreakeblePlatform.ToString())
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
    public bool CollisonWith(GameObject.Tags Tag)
    {
        string id = Tag.ToString();
        for (var x = 0; x < levelGen.tiles.GetLength(0); x++)
        {
            for (var y = 0; y < levelGen.tiles.GetLength(1); y++)
            {
                var tile = levelGen.tiles[x, y];

                if (tile == null || tile == this || tile.Id != id)
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
    public BreakeblePlatform CollisonWithBreakingPlatform()
    {
        for (var x = 0; x < levelGen.tiles.GetLength(0); x++)
        {
            for (var y = 0; y < levelGen.tiles.GetLength(1); y++)
            {
                var tile = levelGen.tiles[x, y];

                if (tile == null || tile == this || tile.Id != Tags.BreakeblePlatform.ToString())
                    continue;

                if (this.Position.X + this.Width / 2 > tile.Position.X && this.Position.X < tile.Position.X + tile.Width / 2
                    && this.Position.Y + this.Height > tile.Position.Y && this.Position.Y < tile.Position.Y + tile.Height)
                {
                    return (BreakeblePlatform)tile;
                }
            }
        }
        return null;
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
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
