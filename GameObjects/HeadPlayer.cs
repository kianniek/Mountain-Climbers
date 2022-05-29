using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using BaseProject.GameStates;
using Microsoft.Xna.Framework.Audio;
using BaseProject.GameObjects;

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
        public int knockbackForce = 100;
        public int musicCounter = 30;

        public SpriteGameObject InputIndicator { get; protected set; } = new SpriteGameObject("");

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
                velocity.X = -1 * knockbackForce;
                jump = true;
                knockback = false;
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
        //Roep deze functie aan als de speler normaal springt en de waterval raakt,
        //maar zodra je de pickup gebruikt, roep deze niet aan.
        public virtual void HitWaterfall()
        {
            velocity.Y = 520;
        }



        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            InputIndicator.Visible = false;
            InputIndicator.Position = this.Position;
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
                        InputIndicator.Sprite = new SpriteSheet(ButtonManager.interract_Button);
                        InputIndicator.Origin = InputIndicator.Center;
                        InputIndicator.Scale = 0.5f;
                        InputIndicator.Position = obj.Position - new Vector2(obj.Width / 2, obj.Height);
                        InputIndicator.Visible = true;

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
                InputIndicator.Sprite = new SpriteSheet(ButtonManager.Dpad_Down_Button);
                InputIndicator.Origin = InputIndicator.Center;
                InputIndicator.Scale = 0.5f;
                InputIndicator.Position = Position - new Vector2(Width / 2, Height);
                InputIndicator.Visible = true;
            }
            else
            {
                InputIndicator.Sprite = new SpriteSheet(ButtonManager.Dpad_Up_Button);
                InputIndicator.Origin = InputIndicator.Center;
                InputIndicator.Scale = 0.5f;
                InputIndicator.Position = Position - new Vector2(Width / 2, Height / 2);
                InputIndicator.Visible = true;
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
    }
}