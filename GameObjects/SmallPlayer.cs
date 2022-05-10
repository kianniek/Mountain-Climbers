using System;
using BaseProject;
using BaseProject.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class SmallPlayer : HeadPlayer
{
    public bool canMove, beingHeld, hitLeftWall, hitRightWall;

    public Lives[] livesSmall;
    public Lives[] noLives;
    public int livesPlayer;

    public SmallPlayer(Tile[,] worldTiles) : base("Player", worldTiles)
    {
        origin = new Vector2(Center.X, Center.Y - Center.Y / 2);

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

        if (stand)
        {
            hitWaterfall = true;
        }

        base.Update(gameTime);
        BreakeblePlatform breakebleplatform = CollisonWithBreakingPlatform();
        if (breakebleplatform != null)
        {
            breakebleplatform.isBreaking = true;
        }

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
                if (tileType == typeof(Tile))
                {
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
                }
                if (tileType == typeof(Rope))
                {
                    hitRope = (this.Position.X + this.Width / 2 > tile.Position.X && this.Position.X < tile.Position.X + tile.Width / 2
                    && this.Position.Y + this.Height > tile.Position.Y && this.Position.Y < tile.Position.Y + tile.Height);
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
    public BreakeblePlatform CollisonWithBreakingPlatform()
    {
        for (var x = 0; x < WorldTiles.GetLength(0); x++)
        {
            for (var y = 0; y < WorldTiles.GetLength(1); y++)
            {
                var tile = WorldTiles[x, y];

                if (tile == null || tile.Id != Tags.BreakeblePlatform.ToString())
                    continue;

                if (this.Position.X + this.Width / 2 > tile.Position.X && this.Position.X < tile.Position.X + tile.Width / 2
                    && this.Position.Y + this.Height > tile.Position.Y && this.Position.Y < tile.Position.Y + tile.Height)
                {
                    return null;//(BreakeblePlatform)tile;
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
        if (hitClimbWall && mPressed)
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

        if (!hitClimbWall)
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
        }

    }

    public override void Knockback()
    {
        base.Knockback();
    }

    internal void PickedUp(Vector2 grabPosition)
    {
        velocity = Vector2.Zero;
        position = grabPosition;
        canMove = false;
        beingHeld = true;
        hitWaterfall = false;
    }
}
