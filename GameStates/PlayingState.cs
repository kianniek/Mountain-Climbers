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
        SmallPlayer smallPlayer;
        LevelGenerator levelGen;
        BigPlayer bigPlayer;
        Button button;

        Camera cam;
        Vector2 cameraUI_offset; // use this to negate the ccamera movement for UI objects
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
            waterfalls.Add(new Waterfall("Waterfall200", new Vector2(500, 10)));

            rocks.Add(new FallingRock("stone100", new Vector2(100, 0 - 100)));
            rocks.Add(new FallingRock("stone300", new Vector2(800, 0 - 300)));

            this.Add(waterfalls);
            this.Add(bigPlayer);
            this.Add(smallPlayer);
            this.Add(button);
            this.Add(rocks);

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
            base.Update(gameTime);
        }

        public override void Reset()
        {
            base.Reset();
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

        }

        void KeepPlayersCenterd()
        {
            Vector2 sharedPlayerPos = (smallPlayer.Position + bigPlayer.Position) / 2;
            Vector2 offsetFromCenter = new Vector2(10, 0);
            Vector2 moveAmount = Vector2.Zero;

            float falloff = 1f;

            Console.WriteLine(falloff);
            if (Game1.Screen.X / 2 - offsetFromCenter.X - cam._transform.M41 > sharedPlayerPos.X)
            {
                moveAmount += Vector2.Lerp(moveAmount, -Vector2.UnitX, falloff);
            }
            else
            if (Game1.Screen.X / 2 + offsetFromCenter.X - cam._transform.M41 < sharedPlayerPos.X)
            {
                moveAmount += Vector2.Lerp(moveAmount, Vector2.UnitX, falloff);
            }

            if (Game1.Screen.Y / 2 - offsetFromCenter.Y - cam._transform.M42 > sharedPlayerPos.Y)
            {
                moveAmount += Vector2.Lerp(moveAmount, -Vector2.UnitY, falloff);
            }
            else
            if (Game1.Screen.Y / 2 + offsetFromCenter.Y - cam._transform.M42 < sharedPlayerPos.Y)
            {
                moveAmount += Vector2.Lerp(moveAmount, Vector2.UnitY, falloff);
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