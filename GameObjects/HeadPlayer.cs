using BaseProject.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject
{
    public class HeadPlayer : SpriteGameObject
    {
        public bool isDead;
        public float gravity;
        public bool left, right, jump, stand, hitClimbWall, zPressed, mPressed, noLeft, noRight, climb, hitRock, hitWaterfall, hitRope;

        public BreakeblePlatform currentPlatform;

        public static float JumpForce = 460;
        public float horizontalSpeed = 175;
        public static float walkingSpeed = 175;
        public static float sprintingSpeed = 200;

        public Vector2 LastSavedPos;
        public int savePosTimer;

        public bool knockback;
        public int knockbackForce = 100;

        public LevelManager levelManager;

        protected Tile[,] WorldTiles { get; private set; }

        public HeadPlayer(string assetName, Tile[,] worldTiles) : base(assetName)
        {
            position.Y = GameEnvironment.Screen.Y / 1.4f;
            position.X = 10;
            gravity = 10f;
            noLeft = false;
            noRight = false;
            this.WorldTiles = worldTiles;
        }

        public override void Update(GameTime gameTime)
        {
            if (knockback)
            {
                velocity.X = -1 * knockbackForce;
                jump = true;
                knockback = false;
            }
            if (jump)
            {
                jump = false;
                stand = false;
                velocity.Y = -JumpForce;
            }

            if (left)
            {
                velocity.X = -horizontalSpeed;
                left = false;
            }
            if (right)
            {
                velocity.X = horizontalSpeed;
                right = false;
            }



            base.Update(gameTime);
            velocity.Y += gravity;
            if (stand)
            {
                velocity.X = 0;
            }
        }

        public virtual void CollisonWithLevelObjecs()
        {
            for (int x = 0; x < levelManager.CurrentLevel().LevelObjects.Children.Count; x++)
            {
                var obj = (SpriteGameObject)levelManager.CurrentLevel().LevelObjects.Children[x];
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
                if (tileType == typeof(BreakeblePlatform))
                {
                    var objBP = (BreakeblePlatform)obj;
                    if (CollidesWith(objBP) && !objBP.isBreaking)
                    {
                        objBP.isBreaking = true;
                    }
                    if (this.Position.X + this.Width / 2 > objBP.Position.X &&
                    this.Position.X < objBP.Position.X + objBP.Width / 2 &&
                    this.Position.Y + this.Height > objBP.Position.Y &&
                    this.Position.Y < objBP.Position.Y + objBP.Height)
                    {
                        var mx = (this.Position.X - objBP.Position.X);
                        var my = (this.Position.Y - objBP.Position.Y);
                        if (Math.Abs(mx) > Math.Abs(my))
                        {
                            if (mx > 0)
                            {
                                this.velocity.X = 0;
                                this.position.X = objBP.Position.X + this.Width / 4;
                            }
                            if (mx < 0)
                            {
                                this.position.X = objBP.Position.X - this.Width / 2;
                                this.velocity.X = 0;
                            }
                        }
                        else
                        {
                            if (my > 0)
                            {
                                this.velocity.Y = 0;
                                this.position.Y = objBP.Position.Y + objBP.Height;
                            }
                            if (my < 0)
                            {
                                this.velocity.Y = 0;
                                this.position.Y = objBP.Position.Y - this.Height;
                                this.stand = true;
                            }
                        }
                    }
                }
            }
        }

        //Roep deze functie aan als de speler normaal springt en de waterval raakt,
        //maar zodra je de pickup gebruikt, roep deze niet aan.
        public virtual void HitWaterfall()
        {
            velocity.Y = 520;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            //     if (inputHelper.IsKeyDown(Keys.Left))
            //   {
            //     left = true;
            //   Mirror = true;
            //}


            // if (inputHelper.IsKeyDown(Keys.Right))
            //{
            //  right = true;
            //Mirror = false;
        }

        public virtual void Climb()
        {
            left = false;
            right = false;
            stand = true;
            gravity = 0;
            velocity.Y = 0;
            velocity.X = 0;
            climb = true;
        }

        public virtual void NotClimbing()
        {
            gravity = 10f;
            climb = false;
        }

        public virtual void Knockback()
        {
            velocity.Y *= -1;
            velocity.X *= -1;
        }

        public void GoToNewLevel(Tile[,] tiles, Vector2 pos)
        {
            WorldTiles = tiles;
            position = pos;
            velocity = Vector2.Zero;
        }
    }
}
