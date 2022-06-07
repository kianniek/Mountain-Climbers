using BaseProject;
using BaseProject.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

//Dion
public class SmallPlayer : HeadPlayer
{
    public PlayingState state;
    public bool canMove, beingHeld, hitLeftWall, hitRightWall, beingThrown;

    public Lives[] livesSmall;
    public Lives[] noLives;
    public int livesPlayer;

    private DateTime breakStartTime;

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
            hitClimbWall = hitRope;
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
            if (inputHelper.KeyPressed(ButtonManager.Up_SmallPlayer))
            {
                //stand = false;
                jump = true;
            }
            TimeSpan timeBetweenNowAndStart = DateTime.Now - breakStartTime;
            var secondsPassed = timeBetweenNowAndStart.Seconds + timeBetweenNowAndStart.Milliseconds / 1000f;
            if (secondsPassed > 3)
            {
                if (inputHelper.KeyPressed(ButtonManager.DogeL_SmallPlayer))
                {
                    velocity.X = -1000;
                    breakStartTime = DateTime.Now;
                }
                if (inputHelper.KeyPressed(ButtonManager.DogeR_SmallPlayer))
                {
                    velocity.X = -1000;
                    breakStartTime = DateTime.Now;
                }
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

            if (inputHelper.IsKeyDown(ButtonManager.Up_SmallPlayer))
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
            if (inputHelper.KeyPressed(ButtonManager.Up_SmallPlayer))
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