using BaseProject.GameObjects;
using BaseProject.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseProject.GameStates;
using Microsoft.Xna.Framework.Audio;

namespace BaseProject
{
    public class HeadPlayer : SpriteGameObject
    {
        public bool isDead;
        public float gravity = 20f;
        public bool left, right, jump, stand, hitClimbWall, zPressed, mPressed, noLeft, noRight, climb, hitRock, hitWaterfall, hitRope, playJump, playWalk;

        public SpriteGameObject inputIndicator { get; protected set; } = new SpriteGameObject("");


        public static float JumpForce = 500;
        public float horizontalSpeed = 175;
        public static float walkingSpeed = 175;
        public static float sprintingSpeed = 200;



        public bool knockback;
        public int knockbackForce = 100;
        public int musicCounter = 30;

        public LevelManager levelManager;

        protected Tile[,] WorldTiles { get; private set; }
        protected Level level;
        
        public HeadPlayer(string assetName) : base(assetName)
        {

            position.Y = GameEnvironment.Screen.Y / 1.4f;
            position.X = 10;
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
                musicCounter--;
                left = false;
            }

            if (right)
            {
                velocity.X = horizontalSpeed;
                musicCounter--;
                right = false;
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

            Console.WriteLine(musicCounter);
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
            inputIndicator.Visible = false;
            inputIndicator.Position = this.Position;
            //Player with Rope Collision test
            for (int x = 0; x < levelManager.CurrentLevel().LevelObjects.Children.Count; x++)
            {
                var obj = (SpriteGameObject)levelManager.CurrentLevel().LevelObjects.Children[x];
                var tileType = obj.GetType();
                if (tileType == typeof(CuttebleRope))
                {
                    CuttebleRope cuttebleRope = (CuttebleRope)levelManager.CurrentLevel().LevelObjects.Children[x];
                    if (CollidesWith(obj) && !cuttebleRope.isOut)
                    {
                        inputIndicator.Sprite = new SpriteSheet(ButtonManager.interract_Button);
                        inputIndicator.Origin = inputIndicator.Center;
                        inputIndicator.Scale = 0.5f;
                        inputIndicator.Position = obj.Position - new Vector2(obj.Width / 2, obj.Height);
                        inputIndicator.Visible = true;

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
                Console.WriteLine(cuttebleRope.level.TileOnLocation(x + 1, y + 1) + "  " + cuttebleRope.level.TileOnLocation(x - 1, y + 1));
                if (cuttebleRope.level.TileOnLocation(x - 1, y + 1))
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Rope rope;
                        Vector2 ropePos = new Vector2(cuttebleRope.Position.X + Level.TileWidth, cuttebleRope.Position.Y + Level.TileWidth * i);
                        if (i == 0)
                        {
                            rope = new Rope("RopeConnectingLeft")
                            {
                                Position = ropePos
                            };
                            levelManager.CurrentLevel().Add(rope);
                        }
                        else
                        {
                            rope = new Rope()
                            {
                                Position = ropePos
                            };
                            levelManager.CurrentLevel().Add(rope);
                        }
                        levelManager.CurrentLevel().LevelObjects.Add(rope);
                    }
                }
                else
                if (!cuttebleRope.level.TileOnLocation(x + 1, y + 1))
                {

                    for (int i = 0; i < 10; i++)
                    {
                        Rope rope;
                        Vector2 ropePos = new Vector2(cuttebleRope.Position.X - Level.TileWidth, cuttebleRope.Position.Y + Level.TileWidth * i);
                        if (i == 0)
                        {
                            rope = new Rope("RopeConnectingRight")
                            {
                                Position = ropePos
                            };
                            levelManager.CurrentLevel().Add(rope);
                        }
                        else
                        {
                            rope = new Rope()
                            {
                                Position = ropePos
                            };
                            levelManager.CurrentLevel().Add(rope);
                        }
                        levelManager.CurrentLevel().LevelObjects.Add(rope);
                    }
                }
                cuttebleRope.isOut = true;
            }
        }
        public virtual void Climb()
        {
            left = false;
            right = false;
            stand = true;
            velocity.Y = 0;
            velocity.X = 0;
            climb = true;

            if (velocity.Y < 0)
            {
                inputIndicator.Sprite = new SpriteSheet(ButtonManager.Dpad_Down_Button);
                inputIndicator.Origin = inputIndicator.Center;
                inputIndicator.Scale = 0.5f;
                inputIndicator.Position = Position - new Vector2(Width / 2, Height);
                inputIndicator.Visible = true;
            }
            else
            {
                inputIndicator.Sprite = new SpriteSheet(ButtonManager.Dpad_Up_Button);
                inputIndicator.Origin = inputIndicator.Center;
                inputIndicator.Scale = 0.5f;
                inputIndicator.Position = Position - new Vector2(Width / 2, Height / 2);
                inputIndicator.Visible = true;
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

        public void GoToNewLevel(Tile[,] tiles, Vector2 pos)
        {
            level = lvl;
            position = pos;
            velocity = Vector2.Zero;
        }
    }
}
