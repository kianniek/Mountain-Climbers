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
    PlayingState state;
    public bool canMove, beingHeld, hitLeftWall, hitRightWall, beingThrown;

    public Lives[] livesSmall;
    public Lives[] noLives;
    public int livesPlayer;

    public SmallPlayer(PlayingState playingState) : base("Player")
    {
        state = playingState;
        origin = new Vector2(Center.X, Center.Y - Center.Y / 2);

        livesPlayer = 2;
        noLives = new Lives[livesPlayer * 2];
        livesSmall = new Lives[livesPlayer];
    }

    public override void Update(GameTime gameTime)
    {
        mPressed = false;

        if (stand)
        {
            hitClimbWall = CollisonWithRope();// || CollisonWith(Tags.ClimebleWall);
            velocity.X = 0;
            thrown = false;
        }

        //Music jump
        if (jump)
        {
            playJump = false;
            GameEnvironment.AssetManager.PlaySound("jump");
        }

        //Music walk
        if (left && !jump && musicCounter == 30 && !right)
        {
            playWalk = false;
            GameEnvironment.AssetManager.PlaySound("step");
        }
        if (right && !jump && musicCounter == 30 && !left)
        {
            GameEnvironment.AssetManager.PlaySound("step");
        }

        CollisonWithLevelObjecs();

        base.Update(gameTime);

        CollisonWithGround();
    }
    public void CollisonWithGround()
    {
        hitLeftWall = false;
        hitRightWall = false;
        foreach (var chunk in level.ActiveChunks())
        {
            for (var y = 0; y < Chunk.Height; y++)
            {
                for (var x = 0; x < Chunk.Width; x++)
                {
                    var tile = chunk.TilesInChunk[x, y];
                    if (tile == null || !tile.Visible)
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
                }
            }

        }
    }
    public bool CollisonWithRope()
    {
        for (int x = 0; x < LevelManager.CurrentLevel().LevelObjects.Children.Count; x++)
        {
            var obj = (SpriteGameObject)LevelManager.CurrentLevel().LevelObjects.Children[x];
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
        for (int x = 0; x < LevelManager.CurrentLevel().LevelObjects.Children.Count; x++)
        {
            var obj = (SpriteGameObject)LevelManager.CurrentLevel().LevelObjects.Children[x];
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
            if (tileType == typeof(ButtonWall))
            {
                if (CollidesWith(obj))
                {
                    if (this.Position.X + this.Width / 2 > obj.Position.X &&
                    this.Position.X < obj.Position.X + obj.Width / 2 &&
                    this.Position.Y + this.Height > obj.Position.Y &&
                    this.Position.Y < obj.Position.Y + obj.Height)
                    {
                        var mx = (this.Position.X - obj.Position.X);
                        var my = (this.Position.Y - obj.Position.Y);
                        if (Math.Abs(mx) > Math.Abs(my))
                        {
                            if (mx > 0)
                            {
                                this.velocity.X = 0;
                                this.position.X = obj.Position.X + this.Width / 4;
                            }

                            if (mx < 0)
                            {
                                this.position.X = obj.Position.X - this.Width / 2;
                                this.velocity.X = 0;
                            }
                        }
                        else
                        {
                            if (my > 0)
                            {
                                this.velocity.Y = 0;
                                this.position.Y = obj.Position.Y + obj.Height;
                            }

                            if (my < 0)
                            {
                                this.velocity.Y = 0;
                                this.position.Y = obj.Position.Y - this.Height;
                                this.stand = true;
                            }
                        }
                    }
                }
            }
        }
    }
    public override void HandleInput(InputHelper inputHelper)
    {

        base.HandleInput(inputHelper);
        if (!beingThrown)
        {
            velocity.X = 0;
        }
        if (!beingHeld)
        {
            if (inputHelper.IsKeyDown(ButtonManager.Left_SmallPlayer))
            {
                left = true;
                Mirror = true;
                right = false;
                velocity.X = -100;
            }
            if (inputHelper.IsKeyDown(ButtonManager.Right_SmallPlayer))
            {
                right = true;
                Mirror = false;
                left = false;
                velocity.X = 100;
            }
        }
        if (!beingHeld && stand)
        {
            playJump = true;
            if (inputHelper.KeyPressed(ButtonManager.Jump_SmallPlayer))
            {
                //stand = false;
                jump = true;
            }
        }
        if (!state.bigPlayer.holdingPlayer)
        {
            beingHeld = false;
        }
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
            playJump = false;
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
    }
    internal void PickedUp(Vector2 grabPosition)
    {
        velocity = Vector2.Zero;
        position = grabPosition;
        canMove = false;
        beingHeld = true;
        thrown = true;
        //stand = false;
    }

    public void SetVelocity(Vector2 velocity)
    {
        this.velocity = velocity;
    }
}