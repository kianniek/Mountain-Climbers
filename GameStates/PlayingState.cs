﻿using BaseProject.Engine;
using BaseProject.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace BaseProject.GameStates
{

    public class PlayingState : GameObjectList
    {
        SpriteGameObject background;
        GameObjectList rocks;
        GameObjectList climbWall;
        public SmallPlayer smallPlayer;
        public BigPlayer bigPlayer;
        //Button button;
        //ButtonWall wall;


        public Camera cam { get; private set; }

        bool playBackgroundMusic = true;
        int musicCount = 14580;

        Vector2 cameraUI_offset; // use this to negate the camera movement for UI objects

        public PlayingState(Camera camera)
        {
            background = new SpriteGameObject("DarkForestBackground", -10) { Shade = new Color(200, 200, 200) };
            Add(background);

            smallPlayer = new SmallPlayer(this);
            bigPlayer = new BigPlayer(smallPlayer);

            Add(bigPlayer.throwDirection);

            climbWall = new GameObjectList();


            rocks = new GameObjectList();

            //wall = new ButtonWall(new Vector2(1000, 1010), new Vector2(1000, 950));
            //button = new Button(smallPlayer, bigPlayer, wall);



            this.cam = camera;

            this.Add(bigPlayer.InputIndicator);
            this.Add(smallPlayer.InputIndicator);
            //this.Add(button);
            //this.Add(wall);

            this.Add(rocks);
            this.Add(climbWall);

            ////Small health
            //for (int i = 0; i < smallPlayer.livesPlayer; i++)
            //{
            //    Lives liveOrange = new Lives("Hartje_oranje", new Vector2(40 * i - cameraUI_offset.X, 0));
            //    smallPlayer.livesSmall[i] = liveOrange;
            //    smallPlayer.noLives[i] = (new Lives("Hartje_leeg", new Vector2(40 * i - cameraUI_offset.X, 0)));
            //    this.Add(smallPlayer.noLives[i]);
            //    this.Add(smallPlayer.livesSmall[i]);
            //}


            ////Big health
            //for (int i = 0; i < bigPlayer.livesPlayer; i++)
            //{
            //    Lives liveGreen = new Lives("Hartje_groen", new Vector2(GameEnvironment.Screen.X - cameraUI_offset.X - 50 - (40 * i), 0));
            //    bigPlayer.livesBig[i] = liveGreen;
            //    bigPlayer.noLives[i + bigPlayer.livesPlayer] = new Lives("Hartje_leeg", new Vector2(GameEnvironment.Screen.X - cameraUI_offset.X - 50 - (40 * i), 0));
            //    this.Add(bigPlayer.noLives[i + bigPlayer.livesPlayer]);
            //    this.Add(bigPlayer.livesBig[i]);
            //}




            this.Add(new LevelManager(bigPlayer, smallPlayer));
            this.Add(bigPlayer);
            this.Add(smallPlayer);

            cam = camera;
            cam.Pos = bigPlayer.Position;
        }
        public override void Update(GameTime gameTime)
        {
            PlayMusic();
            CollisionLevelObejcts();
            base.Update(gameTime);
            KeepPlayersCenterd();
            UI_ElementUpdate();
            CheckGameOver();
        }
        private void PlayMusic()
        {
            if (playBackgroundMusic)
            {
                playBackgroundMusic = false;
                GameEnvironment.AssetManager.PlaySound("MusicWaterfall");
            }

            musicCount--;

            if (musicCount < 0)
            {
                playBackgroundMusic = true;
                musicCount = 14580;
            }
        }
        private void CollisionLevelObejcts()
        {
            //Falling Rocks
            foreach (FallingRock rock in rocks.Children)
            {
                Vector2 rockSmallPlayer = rock.Position - smallPlayer.Position;
                Vector2 rockBigPlayer = rock.Position - bigPlayer.Position;

                //Music is playing when both player distances < 30
                if (rockSmallPlayer.X < 30 || rockBigPlayer.X < 30)
                {
                    rock.closeByRock = true;
                }
                else
                {
                    rock.closeByRock = false;
                }

                //Resets rock if rock is off screen
                if (rock.Position.Y > GameEnvironment.Screen.Y - cam._transform.M42)
                {
                    rock.Reset();
                }

                //Rock hits one of the players and that causes knockback
                if (rock.CollidesWith(smallPlayer))
                {
                    smallPlayer.knockback = true;
                }

                if (rock.CollidesWith(bigPlayer))
                {
                    bigPlayer.knockback = true;
                }
                else
                {
                    bigPlayer.hitRock = false;
                }
            }
        }
        private void CheckGameOver()
        {
            if (smallPlayer.Position.Y > 1000)
            {
                smallPlayer.isDead = true;
            }
            if (bigPlayer.Position.Y > 1000)
            {
                bigPlayer.isDead = true;
            }
            if (smallPlayer.isDead && bigPlayer.isDead)
            {
                bigPlayer.Reset();
                smallPlayer.Reset();
            }

            //Losing live
            if (smallPlayer.isDead)
            {
                smallPlayer.livesPlayer--;
                //smallPlayer.livesSmall[smallPlayer.livesPlayer].Visible = false; 
            }
            if (bigPlayer.isDead)
            {
                bigPlayer.livesPlayer--;
                //bigPlayer.livesBig[bigPlayer.livesPlayer].Visible = false;
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
                        LevelManager.CurrentLevel().LevelObjects.Add(rope);
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
                        LevelManager.CurrentLevel().LevelObjects.Add(rope);
                    }
                }
                cuttebleRope.isOut = true;
            }
        }
        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (inputHelper.IsKeyDown(Keys.Enter))
            {
                GameEnvironment.GameStateManager.SwitchTo("ControlsMenu");
            }

            for (int i = 0; i < LevelManager.CurrentLevel().LevelObjects.Children.Count; i++)
            {
                var obj = (SpriteGameObject)LevelManager.CurrentLevel().LevelObjects.Children[i];
                var tileType = obj.GetType();
                if (tileType == typeof(Button))
                {
                    Button button = (Button)obj;
                    if (bigPlayer.CollidesWith(button) && inputHelper.IsKeyDown(Keys.Space))
                    {
                        button.ButtonPress = true;
                    }

                    if (smallPlayer.CollidesWith(button) && inputHelper.IsKeyDown(Keys.Space))
                    {
                        button.ButtonPress = true;
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
            float distanceBetweenPlayer = Vector2.Distance(camToScreenPos, sharedPlayerPos);
            float falloff = 0.5f;//distanceBetweenPlayer > 1 ? 1 : 0;

            if (camToScreenPos.X > sharedPlayerPos.X)
            {
                moveAmount += Vector2.Lerp(moveAmount, -Vector2.UnitX, falloff);
            }
            else
            if (camToScreenPos.X < sharedPlayerPos.X)
            {
                moveAmount += Vector2.Lerp(moveAmount, Vector2.UnitX, falloff);
            }

            if (camToScreenPos.Y > sharedPlayerPos.Y)
            {
                moveAmount += Vector2.Lerp(moveAmount, -Vector2.UnitY, falloff);
            }
            else
            if (camToScreenPos.Y < sharedPlayerPos.Y)
            {
                moveAmount += Vector2.Lerp(moveAmount, Vector2.UnitY, falloff);
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

            ////orange health
            //for (int i = 0; i < smallPlayer.livesPlayer; i++)
            //{
            //    smallPlayer.livesSmall[i].Position = new Vector2(40 * i - cameraUI_offset.X, 0 - cameraUI_offset.Y);
            //    smallPlayer.noLives[i].Position = new Vector2(40 * i - cameraUI_offset.X, 0 - cameraUI_offset.Y);
            //}


            ////Green health
            //for (int i = 0; i < bigPlayer.livesPlayer; i++)
            //{
            //    bigPlayer.livesBig[i].Position = new Vector2(GameEnvironment.Screen.X - cameraUI_offset.X - 50 - (40 * i), 0 - cameraUI_offset.Y);
            //    bigPlayer.noLives[i + bigPlayer.livesPlayer].Position = new Vector2(GameEnvironment.Screen.X - cameraUI_offset.X - 50 - (40 * i), 0 - cameraUI_offset.Y);
            //}

            //for background
            background.Position = -cameraUI_offset;
        }
    }
}