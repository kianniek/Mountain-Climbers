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
        SmallPlayer smallPlayer;
        LevelGenerator levelGen;
        BigPlayer bigPlayer;
        Button button;

        int livesSmallPlayer;
        int livesBigPlayer;
        private float groundLevel = Game1.Screen.Y;

        public PlayingState()
        {
            levelGen = new LevelGenerator();
            livesSmall = new GameObjectList();
            livesBig = new GameObjectList();
            noLives = new GameObjectList();
            smallPlayer = new SmallPlayer(levelGen);
            bigPlayer = new BigPlayer(levelGen);
            button = new Button();

            livesSmallPlayer = 2;
            livesBigPlayer = 2;

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

            waterfalls.Add(new Waterfall("player2"));

            this.Add(waterfalls);
            this.Add(bigPlayer);
            this.Add(smallPlayer);
            this.Add(button);

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
        }


        public override void Update(GameTime gameTime)
        {
            Console.WriteLine(smallPlayer.Position);

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