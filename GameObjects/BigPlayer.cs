using BaseProject.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseProject
{
    public class BigPlayer : HeadPlayer
    {
        readonly SmallPlayer smallPlayer;

        public Lives[] livesBig;
        public Lives[] noLives;
        public int livesPlayer;

        public Vector2 buttonIndicatorPos;

        public bool holdingPlayer;
        public BigPlayer(Tile[,] worldTiles, SmallPlayer smallPlayer) : base("player2", worldTiles)
        {
            origin = new Vector2(Center.X, Center.Y / 4);
            this.smallPlayer = smallPlayer;

            livesPlayer = 2;
            noLives = new Lives[livesPlayer * 2];
            livesBig = new Lives[livesPlayer];
        }

        public override void Update(GameTime gameTime)
        {
            buttonIndicatorPos = position + new Vector2(0, Height / 2);

            zPressed = false;

            if (stand)
            {
                hitClimbWall = CollisonWithRope() || CollisonWith(Tags.ClimebleWall);
            }

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

            base.Update(gameTime);

            CollisonWithGround();
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
            for (var x = 0; x < WorldTiles.GetLength(0); x++)
            {
                for (var y = 0; y < WorldTiles.GetLength(1); y++)
                {
                    var tile = WorldTiles[x, y];
                    if (tile == null)
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

                    if (tileType == typeof(Lava))
                    {
                        isDead = true;
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
        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
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

                if (inputHelper.IsKeyDown(ButtonManager.Jump_BigPlayer))
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

            if (stand)
            {
                if (inputHelper.KeyPressed(ButtonManager.Jump_BigPlayer))
                {
                    stand = false;
                    jump = true;
                }
                if (inputHelper.IsKeyDown(Keys.Z))
                {
                    zPressed = true;
                }
            }

            if (inputHelper.KeyPressed(ButtonManager.Interact_Bigplayer))
            {
                holdingPlayer = false;
                //smallPlayer.stand = false;
                if (smallPlayer.CollidesWith(this))
                {
                    holdingPlayer = true;
                }
            }

            if (inputHelper.IsKeyDown(ButtonManager.Left_BigPlayer))
            {
                left = true;
                Mirror = true;
            }
            if (inputHelper.IsKeyDown(ButtonManager.Right_BigPlayer))
            {
                right = true;
                Mirror = false;
            }
        }
    }
}