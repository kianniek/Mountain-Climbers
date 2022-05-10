using BaseProject.Engine;
using BaseProject.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace BaseProject.GameStates
{

    public class PlayingState : GameObjectList
    {
        SpriteGameObject background;
        GameObjectList waterfalls;
        GameObjectList rocks;
        GameObjectList climbWall;
        SmallPlayer smallPlayer;
        BigPlayer bigPlayer;
        Button button;
        ButtonWall wall;
        Checkpoint cp;

        Camera cam;
        Vector2 cameraUI_offset; // use this to negate the camera movement for UI objects


        private LevelManager levelManager;

        public PlayingState(Camera camera)
        {
            background = new SpriteGameObject("DarkForestBackground", -10) { Shade = new Color(200, 200, 200) };
            Add(background);
            
            smallPlayer = new SmallPlayer(new Tile[0,0]);
            bigPlayer = new BigPlayer(new Tile[0,0], smallPlayer);

            waterfalls = new GameObjectList();
            climbWall = new GameObjectList();

            rocks = new GameObjectList();

            wall = new ButtonWall(new Vector2(900, 1010), new Vector2(900, 950));
            button = new Button(smallPlayer, bigPlayer, wall);

            cp = new Checkpoint();

            this.cam = camera;

            //Test
            //waterfalls.Add(new Waterfall("Waterfall200", new Vector2(500, 10)));

            //rocks.Add(new FallingRock("stone100", new Vector2(100, 0 - 100)));
            //rocks.Add(new FallingRock("stone300", new Vector2(800, 0 - 300)));
            //climbWall.Add(new ClimbWall("Waterfall200", new Vector2(200, 500)));

            this.Add(waterfalls);
            this.Add(bigPlayer);
            this.Add(smallPlayer);
            this.Add(button);
            this.Add(wall);
            this.Add(cp);
            this.Add(rocks);
            this.Add(climbWall);

            //Small health
            for (int i = 0; i < smallPlayer.livesPlayer; i++)
            {
                Lives liveOrange = new Lives("Hartje_oranje", new Vector2(40 * i - cameraUI_offset.X, 0));
                smallPlayer.livesSmall[i] = liveOrange;
                smallPlayer.noLives[i] = (new Lives("Hartje_leeg", new Vector2(40 * i - cameraUI_offset.X, 0)));
                this.Add(smallPlayer.noLives[i]);
                this.Add(smallPlayer.livesSmall[i]);
            }


            //Big health
            for (int i = 0; i < bigPlayer.livesPlayer; i++)
            {
                Lives liveGreen = new Lives("Hartje_groen", new Vector2(GameEnvironment.Screen.X - cameraUI_offset.X - 50 - (40 * i), 0));
                bigPlayer.livesBig[i] = liveGreen;
                bigPlayer.noLives[i + bigPlayer.livesPlayer] = new Lives("Hartje_leeg", new Vector2(GameEnvironment.Screen.X - cameraUI_offset.X - 50 - (40 * i), 0));
                this.Add(bigPlayer.noLives[i + bigPlayer.livesPlayer]);
                this.Add(bigPlayer.livesBig[i]);
            }
            
            
            
            
            levelManager = new LevelManager(bigPlayer, smallPlayer);
            Add(levelManager);
            
            cam = camera;
            cam.Pos = bigPlayer.Position;
        }
        public override void Update(GameTime gameTime)
        {
            if (smallPlayer.CollidesWith(wall) && (!smallPlayer.Mirror))
            {

               
                smallPlayer.right = false;

            }
            else
                smallPlayer.noRight = false;
            if (smallPlayer.CollidesWith(wall) && (smallPlayer.Mirror))
            {
                //smallPlayer.left = false;
            }
            else
                smallPlayer.noLeft = false;

            base.Update(gameTime);
            KeepPlayersCenterd();
            UI_ElementUpdate();
            CheckGameOver();
        }
        private void CheckGameOver()
        {
            return;
            if (smallPlayer.isDead)
            {
                smallPlayer.Position = bigPlayer.Position;
                smallPlayer.canMove = true;
                //smallPlayer.Visible = false;
            }
            if (bigPlayer.isDead)
            {
                bigPlayer.Position = smallPlayer.Position;
                //bigPlayer.Visible = false;
            }
            if (smallPlayer.isDead && bigPlayer.isDead)
            {
                bigPlayer.Reset();
                smallPlayer.Reset();

                GameEnvironment.GameStateManager.SwitchTo("StartState");
            }
        }
        public void DropDownRope(CuttebleRope cuttebleRope, int x, int y)
        {
            if (!cuttebleRope.isOut)
            {
                if (levelManager.CurrentLevel().Tiles[cuttebleRope.x - 1, cuttebleRope.y + 1] != null)
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
                            Add(rope);
                        }
                        else
                        {
                            rope = new Rope()
                            {
                                Position = ropePos
                            };
                            Add(rope);
                        }
                        //levelManager.CurrentLevel().Tiles[x - 1, y + i] = rope;
                    }
                }
                else
                if (levelManager.CurrentLevel().Tiles[cuttebleRope.x + 1, cuttebleRope.y + 1] != null)
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
                            Add(rope);
                        }
                        else
                        {
                            rope = new Rope()
                            {
                                Position = ropePos
                            };
                            Add(rope);
                        }
                        //levelManager.CurrentLevel().Tiles[x + 1, y + i] = rope;
                    }
                }
                cuttebleRope.isOut = true;
            }
        }


        public override void Reset()
        {
            base.Reset();
        }


        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if ((smallPlayer.CollidesWith(button)) && inputHelper.IsKeyDown(Keys.Space))
            {
                Console.WriteLine("lets go");

            }

            if ((bigPlayer.CollidesWith(button)) && inputHelper.IsKeyDown(Keys.Enter))
            {
                Console.WriteLine("alleen voor de grote spelers");
            }

            //Player with Rope Collision test
            for (int x = 0; x < levelManager.CurrentLevel().Tiles.GetLength(0); x++)
            {
                for (int y = 0; y < levelManager.CurrentLevel().Tiles.GetLength(1); y++)
                {
                    if (levelManager.CurrentLevel().Tiles[x, y] == null || !(levelManager.CurrentLevel().Tiles[x, y] is CuttebleRope))
                        continue;

                    if (smallPlayer.CollidesWith(levelManager.CurrentLevel().Tiles[x, y]) || bigPlayer.CollidesWith(levelManager.CurrentLevel().Tiles[x, y]))
                    {
                        if (inputHelper.KeyPressed(Keys.E))
                        {
                            //DropDownRope((CuttebleRope)levelManager.CurrentLevel().Tiles[x, y], x, y);
                        }
                    }
                }
            }

        }
        void KeepPlayersCenterd()
        {
            Vector2 sharedPlayerPos = (smallPlayer.Position + bigPlayer.Position) / 2;
            Vector2 offsetFromCenter = new Vector2(10, 10);
            Vector2 moveAmount = Vector2.Zero;
            Vector2 camToScreenPos = new Vector2(Game1.Screen.X / 2 - offsetFromCenter.X - cam._transform.M41, Game1.Screen.Y / 2 - offsetFromCenter.Y - cam._transform.M42);

            float falloff = Vector2.Distance(camToScreenPos, sharedPlayerPos) > 1 ? 1 : 0;

            if (camToScreenPos.X > sharedPlayerPos.X)
            {
                moveAmount += Vector2.SmoothStep(moveAmount, -Vector2.UnitX, falloff);
            }
            else
            if (camToScreenPos.X < sharedPlayerPos.X)
            {
                moveAmount += Vector2.SmoothStep(moveAmount, Vector2.UnitX, falloff);
            }

            if (camToScreenPos.Y > sharedPlayerPos.Y)
            {
                moveAmount += Vector2.SmoothStep(moveAmount, -Vector2.UnitY, falloff);
            }
            else
            if (camToScreenPos.Y < sharedPlayerPos.Y)
            {
                moveAmount += Vector2.SmoothStep(moveAmount, Vector2.UnitY, falloff);
            }
            //if (cam.Pos.X < GameEnvironment.Screen.X / 2)
            //{
            //    moveAmount += Vector2.UnitX * falloff;
            //    Console.WriteLine("offscreen");
            //}
            //if (cam.Pos.Y > GameEnvironment.Screen.Y / 2)
            //{
            //    moveAmount -= Vector2.UnitY * falloff;
            //    Console.WriteLine("offscreen");
            //}

            cam.Move(moveAmount);
        }
        void UI_ElementUpdate()
        {
            cameraUI_offset = new Vector2(cam._transform.M41, cam._transform.M42);

            //orange health
            for (int i = 0; i < smallPlayer.livesPlayer; i++)
            {
                smallPlayer.livesSmall[i].Position = new Vector2(40 * i - cameraUI_offset.X, 0 - cameraUI_offset.Y);
                smallPlayer.noLives[i].Position = new Vector2(40 * i - cameraUI_offset.X, 0 - cameraUI_offset.Y);
            }


            //Green health
            for (int i = 0; i < bigPlayer.livesPlayer; i++)
            {
                bigPlayer.livesBig[i].Position = new Vector2(GameEnvironment.Screen.X - cameraUI_offset.X - 50 - (40 * i), 0 - cameraUI_offset.Y);
                bigPlayer.noLives[i + bigPlayer.livesPlayer].Position = new Vector2(GameEnvironment.Screen.X - cameraUI_offset.X - 50 - (40 * i), 0 - cameraUI_offset.Y);
            }

            //for background
            background.Position = -cameraUI_offset;
        }
    }
}