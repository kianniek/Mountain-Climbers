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
        Lives[] livesSmall;
        Lives[] livesBig;
        Lives[] noLives;
        GameObjectList waterfalls;
        GameObjectList rocks;
        GameObjectList climbWall;
        SmallPlayer smallPlayer;
        LevelGenerator levelGen;
        BigPlayer bigPlayer;
        Button button;

        Camera cam;
        Vector2 cameraUI_offset; // use this to negate the camera movement for UI objects
        int livesPlayer;

        public PlayingState(Camera camera)
        {
            livesPlayer = 2;
            noLives = new Lives[livesPlayer * 2];
            livesSmall = new Lives[livesPlayer];
            livesBig = new Lives[livesPlayer];
            levelGen = new LevelGenerator();

            waterfalls = new GameObjectList();
            smallPlayer = new SmallPlayer(levelGen);
            bigPlayer = new BigPlayer(levelGen, smallPlayer);
            climbWall = new GameObjectList();

            rocks = new GameObjectList();

            button = new Button();

            this.cam = camera;

            this.Add(levelGen);
            foreach (GameObject tile in levelGen.tiles)
            {
                GameObject levelObject = tile;
                if (levelObject == null)
                {
                    continue;
                }
                Add(levelObject);
            }

            //Test
            //waterfalls.Add(new Waterfall("Waterfall200", new Vector2(500, 10)));

            //rocks.Add(new FallingRock("stone100", new Vector2(100, 0 - 100)));
            //rocks.Add(new FallingRock("stone300", new Vector2(800, 0 - 300)));
            //climbWall.Add(new ClimbWall("Waterfall200", new Vector2(200, 500)));

            this.Add(waterfalls);
            this.Add(bigPlayer);
            this.Add(smallPlayer);
            this.Add(button);
            this.Add(rocks);
            this.Add(climbWall);

            //Orange health
            for (int i = 0; i < livesPlayer; i++)
            {
                Lives liveOrange = new Lives("Hartje_oranje", new Vector2(40 * i - cameraUI_offset.X, 0));
                livesSmall[i] = liveOrange;
                noLives[i] = (new Lives("Hartje_leeg", new Vector2(40 * i - cameraUI_offset.X, 0)));
                this.Add(noLives[i]);
                this.Add(livesSmall[i]);
            }


            //Green health
            for (int i = 0; i < livesPlayer; i++)
            {
                Lives liveGreen = new Lives("Hartje_groen", new Vector2(GameEnvironment.Screen.X - cameraUI_offset.X - 50 - (40 * i), 0));
                livesBig[i] = liveGreen;
                noLives[i + livesPlayer] = new Lives("Hartje_leeg", new Vector2(GameEnvironment.Screen.X - cameraUI_offset.X - 50 - (40 * i), 0));
                this.Add(noLives[i + livesPlayer]);
                this.Add(livesBig[i]);
            }

            this.Add(bigPlayer);
            this.Add(smallPlayer);


            cam.Pos = new Vector2(Game1.Screen.X / 2, Game1.Screen.Y / 2);

        }


        public override void Update(GameTime gameTime)
        {
            KeepPlayersCenterd();
            UI_ElementUpdate();



            

            //Climbing test!!!
            foreach (ClimbWall climb in climbWall.Children)
            {
                if (bigPlayer.stand)
                {
                    if (bigPlayer.CollidesWith(climb))
                    {
                        bigPlayer.hitClimbWall = true;
                    }
                }

                if (smallPlayer.stand)
                {
                    if (smallPlayer.CollidesWith(climb))
                    {
                        smallPlayer.hitClimbWall = true;
                    }
                }
            }

            Console.WriteLine(bigPlayer.hitClimbWall);
            base.Update(gameTime);
        }

        public void DropDownRope(CuttebleRope cuttebleRope, int x, int y)
        {
            if (!cuttebleRope.isOut)
            {
                Console.WriteLine("dropdown");
                float heightOffset = Game1.Screen.Y - levelGen.map.Height * levelGen.ground.Height;

                if (levelGen.tiles[cuttebleRope.x - 1, cuttebleRope.y + 1] != null)
                {
                    for (int i = 1; i < 10; i++)
                    {
                        Rope rope = new Rope()
                        {
                            Position = new Vector2(cuttebleRope.Position.X + levelGen.ground.Width, cuttebleRope.Position.Y + levelGen.ground.Width * i),
                        };
                        Add(rope);
                        levelGen.tiles[x - 1, y + i] = rope;
                    }
                }
                cuttebleRope.isOut = true;
            }
            else
            if (levelGen.tiles[cuttebleRope.x + 1, cuttebleRope.y + 1] != null)
            {
                for (int i = 1; i < 10; i++)
                {
                    Rope rope = new Rope()
                    {
                        Position = new Vector2(cuttebleRope.Position.X - levelGen.ground.Width, cuttebleRope.Position.Y + levelGen.ground.Width * i),
                    };
                    Add(rope);
                    levelGen.tiles[x + 1, y + i] = rope;
                }
            }
            cuttebleRope.isOut = true;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if ((smallPlayer.CollidesWith(button)) && inputHelper.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                Console.WriteLine("lets go");

            }

            if ((bigPlayer.CollidesWith(button)) && inputHelper.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter))
            {
                Console.WriteLine("alleen voor de grote spelers");
            }

            //Player with Rope Collision test
            for (int x = 0; x < levelGen.tiles.GetLength(0); x++)
            {
                for (int y = 0; y < levelGen.tiles.GetLength(1); y++)
                {
                    if (levelGen.tiles[x, y] == null || !(levelGen.tiles[x, y] is CuttebleRope))
                        continue;

                    if (smallPlayer.CollidesWith(levelGen.tiles[x, y]))
                    {
                        if (inputHelper.KeyPressed(Keys.E))
                        {
                            DropDownRope((CuttebleRope)levelGen.tiles[x, y], x, y);
                            //levelGen.tiles[x, y].Scale = 0f;
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
                //moveAmount += -Vector2.UnitX * falloff;
            }
            else
            if (camToScreenPos.X < sharedPlayerPos.X)
            {
                moveAmount += Vector2.SmoothStep(moveAmount, Vector2.UnitX, falloff);
                //moveAmount += Vector2.UnitX * falloff;
            }

            if (camToScreenPos.Y > sharedPlayerPos.Y)
            {
                moveAmount += Vector2.SmoothStep(moveAmount, -Vector2.UnitY, falloff);
                //moveAmount += -Vector2.UnitY * falloff;
            }
            else
            if (camToScreenPos.Y < sharedPlayerPos.Y)
            {
                moveAmount += Vector2.SmoothStep(moveAmount, Vector2.UnitY, falloff);
                //moveAmount += Vector2.UnitY * falloff;
            }
            if (cam.Pos.X < GameEnvironment.Screen.X / 2)
            {
                moveAmount += Vector2.UnitX * falloff;
            }
            if (cam.Pos.Y > GameEnvironment.Screen.Y / 2)
            {
                moveAmount -= Vector2.UnitY * falloff;
            }

            cam.Move(moveAmount);
        }
        void UI_ElementUpdate()
        {
            cameraUI_offset = new Vector2(cam._transform.M41, cam._transform.M42);

            //orange health
            for (int i = 0; i < livesPlayer; i++)
            {
                livesSmall[i].Position = new Vector2(40 * i - cameraUI_offset.X, 0 - cameraUI_offset.Y);
                noLives[i].Position = new Vector2(40 * i - cameraUI_offset.X, 0 - cameraUI_offset.Y);
            }


            //Green health
            for (int i = 0; i < livesPlayer; i++)
            {
                livesBig[i].Position = new Vector2(GameEnvironment.Screen.X - cameraUI_offset.X - 50 - (40 * i), 0 - cameraUI_offset.Y);
                noLives[i + livesPlayer].Position = new Vector2(GameEnvironment.Screen.X - cameraUI_offset.X - 50 - (40 * i), 0 - cameraUI_offset.Y);
            }
        }
    }
}