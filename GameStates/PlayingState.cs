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
        GameObjectList livesSmall;
        GameObjectList livesBig;
        GameObjectList noLives;
        GameObjectList waterfalls;
        GameObjectList rocks;
        SmallPlayer smallPlayer;
        LevelGenerator levelGen;
        BigPlayer bigPlayer;
        Button button;

        Camera cam;

        int livesSmallPlayer;
        int livesBigPlayer;

        public PlayingState(Camera camera)
        {
            levelGen = new LevelGenerator();
            livesSmall = new GameObjectList();
            livesBig = new GameObjectList();
            noLives = new GameObjectList();
            waterfalls = new GameObjectList();

            rocks = new GameObjectList();
            //smallPlayer = new SmallPlayer();
            //bigPlayer = new BigPlayer();

            smallPlayer = new SmallPlayer(levelGen);
            bigPlayer = new BigPlayer(levelGen);

            button = new Button();

            this.cam = camera;

            livesSmallPlayer = 2;
            livesBigPlayer = 2;

           //waar = false;

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
            waterfalls.Add(new Waterfall("Waterfall200" );

            rocks.Add(new FallingRock("stone100", new Vector2(100, 0 - 100)));
            rocks.Add(new FallingRock("stone300", new Vector2(800, 0 - 300)));

            this.Add(waterfalls);
            this.Add(bigPlayer);
            this.Add(smallPlayer);
            this.Add(button);
            this.Add(rocks);

            //Orange health
            for (int i = 0; i < livesSmallPlayer; i++)
            {
                Lives liveOrange = new Lives("Hartje_oranje", new Vector2(40 * i, 0));
                noLives.Add(new Lives("Hartje_leeg", new Vector2(40 * i, 0)));
                livesSmall.Add(liveOrange);
            }


            //Green health
            for (int i = 0; i < livesBigPlayer; i++)
            {
                Lives liveGreen = new Lives("Hartje_groen", new Vector2(GameEnvironment.Screen.X - 50 - (40 * i), 0));
                noLives.Add(new Lives("Hartje_leeg", new Vector2(GameEnvironment.Screen.X - 50 - (40 * i), 0)));
                livesBig.Add(liveGreen);
            }

            this.Add(bigPlayer);
            this.Add(smallPlayer);

            this.Add(noLives);
            this.Add(livesSmall);
            this.Add(livesBig);

            cam.Pos = new Vector2(Game1.Screen.X / 2, Game1.Screen.Y / 2);
        }


        public override void Update(GameTime gameTime)
        {
            Vector2 sharedPlayerPos = (smallPlayer.Position + bigPlayer.Position)/2;
            int offsetFromCenter = 100;
            Vector2 moveAmount = Vector2.Zero;
            Console.WriteLine(sharedPlayerPos.X - cam.Pos.X);
            if (Game1.Screen.X + offsetFromCenter < sharedPlayerPos.X + cam.Pos.X)
            {
                moveAmount = Vector2.Lerp(moveAmount, Vector2.UnitX, 0.5f);
            }
            else
            if (Game1.Screen.X - offsetFromCenter > sharedPlayerPos.X + cam.Pos.X)
            {
                moveAmount = Vector2.Lerp(moveAmount, -Vector2.UnitX, 0.5f);
            }

            cam.Move(moveAmount);
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
    }
}