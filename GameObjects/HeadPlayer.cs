using BaseProject.GameObjects;
using BaseProject.GameStates;
using Microsoft.Xna.Framework;
using System;

namespace BaseProject
{
    public class HeadPlayer : SpriteGameObject
    {
        public bool isDead;
        public float gravity = 20f;
        public bool left, right, jump, stand, hitClimbWall, zPressed, mPressed, noLeft, noRight, climb, hitRock, hitWaterfall, hitRope, playJump, playWalk, thrown;

        public static float JumpForce = 500;
        public float horizontalSpeed = 175;
        public static float walkingSpeed = 175;
        public static float sprintingSpeed = 200;

        public Vector2 LastSavedPos;
        public int savePosTimer;

        public bool knockback;
        public int knockbackForce = 5;
        public int musicCounter = 30;

        public SpriteGameObject InputIndicator { get; protected set; } = new SpriteGameObject("");
        public SpriteGameObject InputIndicatorReserve { get; protected set; } = new SpriteGameObject("");

        protected Tile[,] WorldTiles { get; private set; }
        protected Level level;

        public HeadPlayer(string assetName) : base(assetName)
        {
            position.Y = GameEnvironment.Screen.Y / 1.4f;
            position.X = 10;
            noLeft = false;
            noRight = false;
            playJump = true;
            playWalk = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (knockback)
            {
                Vector2.Normalize(velocity);
                velocity.X *= -1 * knockbackForce;
                velocity.Y = -JumpForce / 4;
                knockback = false;
                left = false;
                right = false;
            }
            if (jump)
            {
                jump = false;
                stand = false;
                velocity.Y = -JumpForce;
                thrown = false;
            }

            if (left)
            {
                velocity.X = -horizontalSpeed;
                musicCounter--;
                left = false;
            }

            if (right)
            {
                velocity.X = horizontalSpeed;
                musicCounter--;
                right = false;
            }

            if (musicCounter < 0)
            {
                musicCounter = 30;
            }

            if (position.Y > 1580)
            {
                isDead = true;
            }

            base.Update(gameTime);
            velocity.Y += gravity;
            if (stand)
            {
                velocity.X = 0;
            }

            if (CollisonWithWaterfall() && !thrown)
            {
                HitWaterfall();
            }
        }

        public bool CollisonWithWaterfall()
        {
            for (int x = 0; x < LevelManager.CurrentLevel().LevelObjects.Children.Count; x++)
            {
                var obj = (SpriteGameObject)LevelManager.CurrentLevel().LevelObjects.Children[x];
                var tileType = obj.GetType();
                if (tileType == typeof(Waterfall))
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

                if (tileType == typeof(Lava) && CollidesWith(obj))
                {
                    isDead = true;
                }

                if (tileType == typeof(ButtonWall) && CollidesWith(obj))
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
        public bool CollisonWithRope()
        {
            for (int x = 0; x < LevelManager.CurrentLevel().LevelObjects.Children.Count; x++)
            {
                var obj = (SpriteGameObject)LevelManager.CurrentLevel().LevelObjects.Children[x];
                var tileType = obj.GetType();
                if (tileType == typeof(Rope) && CollidesWith(obj))
                {
                    return true;
                }
            }
            return false;
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
            InputIndicator.Visible = InputIndicatorReserve.Visible = false;
            InputIndicator.Position = InputIndicatorReserve.Position = this.Position;
            //Player with Rope Collision test
            for (int x = 0; x < LevelManager.CurrentLevel().LevelObjects.Children.Count; x++)
            {
                var obj = (SpriteGameObject)LevelManager.CurrentLevel().LevelObjects.Children[x];
                var tileType = obj.GetType();
                if (tileType == typeof(CuttebleRope))
                {
                    CuttebleRope cuttebleRope = (CuttebleRope)LevelManager.CurrentLevel().LevelObjects.Children[x];
                    if (CollidesWith(obj) && !cuttebleRope.isOut)
                    {
                        InputIndicatorHandler(ButtonManager.interract_Button, obj, Vector2.Zero, 0);

                        if (inputHelper.KeyPressed(ButtonManager.Interact_Bigplayer) || inputHelper.KeyPressed(ButtonManager.Interact_SmallPlayer))
                        {
                            DropDownRope(cuttebleRope);
                        }
                    }
                }
            }
        }

        public void DropDownRope(CuttebleRope cuttebleRope)
        {
            if (!cuttebleRope.isOut)
            {
                int x = cuttebleRope.x;
                int y = cuttebleRope.y;
                if (cuttebleRope.level.TileOnLocation(x - 1, y + 1))
                {
                    for (int i = 0; i < 60; i++)
                    {
                        Rope rope;
                        Vector2 ropePos = new Vector2(cuttebleRope.Position.X + Level.TileWidth, cuttebleRope.Position.Y + Level.TileWidth * i);
                        if (i == 0)
                        {
                            rope = new Rope("RopeConnectingLeft")
                            {
                                Position = ropePos
                            };
                            LevelManager.CurrentLevel().Add(rope);
                        }
                        else
                        {
                            rope = new Rope()
                            {
                                Position = ropePos
                            };
                            LevelManager.CurrentLevel().Add(rope);
                        }
                        LevelManager.CurrentLevel().LevelObjects.Add(rope);
                    }
                }
                else
                if (!cuttebleRope.level.TileOnLocation(x + 1, y + 1))
                {

                    for (int i = 0; i < 60; i++)
                    {
                        Rope rope;
                        Vector2 ropePos = new Vector2(cuttebleRope.Position.X - Level.TileWidth, cuttebleRope.Position.Y + Level.TileWidth * i);
                        if (i == 0)
                        {
                            rope = new Rope("RopeConnectingRight")
                            {
                                Position = ropePos
                            };
                            LevelManager.CurrentLevel().Add(rope);
                        }
                        else
                        {
                            rope = new Rope()
                            {
                                Position = ropePos
                            };
                            LevelManager.CurrentLevel().Add(rope);
                        }
                        LevelManager.CurrentLevel().LevelObjects.Add(rope);
                    }
                }
            }
            cuttebleRope.isOut = true;
        }


        public virtual void Climb()
        {
            //left = false;
            // right = false;
            stand = true;
            velocity.Y = 0;
            velocity.X = 0;
            climb = true;

            if (velocity.Y < 0)
            {
                InputIndicatorHandler(ButtonManager.Dpad_Down_Button, this, Vector2.Zero, 0);
            }
            else
            {
                InputIndicatorHandler(ButtonManager.Dpad_Up_Button, this, Vector2.Zero, 0);
            }
        }

        public virtual void NotClimbing()
        {
            climb = false;
        }

        public virtual void Knockback()
        {
            velocity.Y *= -1;
            velocity.X *= -1;
        }

        public void GoToNewLevel(Level lvl, Vector2 pos)
        {
            level = lvl;
            position = pos;
            velocity = Vector2.Zero;
        }

        public void InputIndicatorHandler(string assetname, SpriteGameObject obj, Vector2 offset, int index)
        {
            switch (index)
            {
                case 0:
                    InputIndicator.Sprite = new SpriteSheet(assetname);
                    InputIndicator.Origin = InputIndicator.Center;
                    InputIndicator.Scale = 0.5f;
                    InputIndicator.Position = obj.Position - new Vector2(obj.Width / 2 + offset.X, obj.Height + offset.Y);
                    InputIndicator.Visible = true;
                    break;
                case 1:
                    InputIndicatorReserve.Sprite = new SpriteSheet(assetname);
                    InputIndicatorReserve.Origin = InputIndicator.Center;
                    InputIndicatorReserve.Scale = 0.5f;
                    InputIndicatorReserve.Position = obj.Position - new Vector2(obj.Width / 2 + offset.X, obj.Height + offset.Y);
                    InputIndicatorReserve.Visible = true;
                    break;
                default:
                    Console.WriteLine("index not in range");
                    break;
            }

        }
    }
}