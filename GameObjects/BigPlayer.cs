using System;
using BaseProject;
using BaseProject.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BaseProject.GameStates;

public class BigPlayer : HeadPlayer
{
    readonly SmallPlayer smallPlayer;
    readonly public ThrowDirection throwDirection;

    private float directionIncrease;

    public Lives[] livesBig;
    public Lives[] noLives;
    public int livesPlayer;

    public bool holdingPlayer;
    public BigPlayer(SmallPlayer smallPlayer) : base("player2")
    {
        origin = new Vector2(Center.X, Center.Y / 4);
        this.smallPlayer = smallPlayer;

        livesPlayer = 2;
        noLives = new Lives[livesPlayer * 2];
        livesBig = new Lives[livesPlayer];
        throwDirection = new ThrowDirection(this, smallPlayer);
    }

    public override void Update(GameTime gameTime)
    {
        zPressed = false;

        if (stand)
        {
            hitClimbWall = CollisonWithRope();// || CollisonWith(Tags.ClimebleWall);
        }
        if (holdingPlayer)
        {
            GrabPlayer();
        }
        else
        {
            smallPlayer.canMove = true;
        }
        directionIncrease = 3 * (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (holdingPlayer)
        {
            GrabPlayer();
            if (smallPlayer.hitRightWall)
            {
                right = false;
            }
            if (smallPlayer.hitLeftWall)
            {
                left = false;
            }
        }
        else
        {
            smallPlayer.canMove = true;
            smallPlayer.beingHeld = false;
        }

        //Jump sound
        if (jump && playJump)
        {
            playJump = false;
            GameEnvironment.AssetManager.PlaySound("jump");
        }

        //Music walk left + right
        if (left && !jump && musicCounter == 30 && !right)
        {
            playWalk = false;
            GameEnvironment.AssetManager.PlaySound("step");
        }
        if (right && !jump && musicCounter == 30 && !left)
        {
            GameEnvironment.AssetManager.PlaySound("step");
        }

        base.Update(gameTime);
        throwDirection.Update(gameTime);

        CollisonWithGround();
        CollisonWithLevelObjecs();
    }
    public void GrabPlayer()
    {
        smallPlayer.PickedUp(new Vector2(position.X, position.Y - smallPlayer.Height));
        if (smallPlayer.beingHeld)
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
        }
    }
    public void CollisonWithGround()
    {
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
                        else
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
        velocity.X = 0;
        if (inputHelper.IsKeyDown(ButtonManager.Left_BigPlayer))
        {
            left = true;
            right = false;
            //effective = SpriteEffects.FlipHorizontally;
            Mirror = true;
            velocity.X = -100;
        }
        if (inputHelper.IsKeyDown(ButtonManager.Right_BigPlayer))
        {
            right = true;
            left = false;
            //effective = SpriteEffects.None;
            Mirror = false;
            velocity.X = 100;
        }
        if (inputHelper.IsKeyDown(ButtonManager.Sprint_Bigplayer))
        {
            horizontalSpeed = sprintingSpeed;
        }
        else
        {
            horizontalSpeed = walkingSpeed;
        }

        //Player is climbing the wall by hitting a climbing wall or rope and pressing Z
        if (hitClimbWall)
        {
            Climb();

            if (inputHelper.IsKeyDown(ButtonManager.Up_BigPlayer))
            {
                velocity.Y = -100;
            }
            if (inputHelper.IsKeyDown(ButtonManager.Down_BigPlayer))
            {
                velocity.Y = 100;
            }
        }
        else
        {
            NotClimbing();
        }
        if (inputHelper.KeyPressed(ButtonManager.Interact_Bigplayer))
        {
            holdingPlayer = false;
            //smallPlayer.stand = false;
            if (smallPlayer.CollidesWith(this))
            {
                holdingPlayer = !holdingPlayer;
                smallPlayer.stand = false;
            }
        }

        if (holdingPlayer)
        {
            InputIndicatorHandler(ButtonManager.LB_Button, this, new Vector2(0, 0), 0);
            InputIndicatorHandler(ButtonManager.RB_Button, this, new Vector2(-Width, 0), 1);

            if (inputHelper.IsKeyDown(ButtonManager.AimL_BigPlayer))
            {
                throwDirection.DecreaseAngle(directionIncrease);
            }
            if (inputHelper.IsKeyDown(ButtonManager.AimR_BigPlayer))
            {
                throwDirection.IncreaseAngle(directionIncrease);
            }
            if (inputHelper.IsKeyDown(ButtonManager.Throw_BigPlayer))
            {
                throwDirection.ThrowPlayer();
            }

        }


        if (stand)
        {
            playJump = true;
            if (inputHelper.KeyPressed(ButtonManager.Up_BigPlayer))
            {
                stand = false;
                jump = true;
                //PlayJumpMusic("jump");
            }
        }
    }

    /*public override void PlayJumpMusic(string music)
    {
        base.PlayJumpMusic(music);
    }*/
}