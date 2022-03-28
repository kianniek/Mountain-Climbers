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
        SmallPlayer smallPlayer;
        BigPlayer bigPlayer;
        LevelGenerator levelGen;

        int livesSmallPlayer;
        int livesBigPlayer;
        private float groundLevel = Game1.Screen.Y;

        public PlayingState()
        {
            livesSmall = new GameObjectList();
            livesBig = new GameObjectList();
            smallPlayer = new SmallPlayer();
            bigPlayer = new BigPlayer();
            levelGen = new LevelGenerator();

            livesSmallPlayer = 2;
            livesBigPlayer = 2;



            //Orange health
            for (int i = 0; i < livesSmallPlayer; i++)
            {
                Lives liveOrange = new Lives("Hartje_oranje", new Vector2(40 * i, 0));
                livesSmall.Add(liveOrange);
            }

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

            //Green health
            for (int i = 0; i < livesBigPlayer; i++)
            {
                Lives liveGreen = new Lives("Hartje_groen", new Vector2(GameEnvironment.Screen.X - 50 - (40 * i), 0));
                //noLives.Add(new Lives("Hartje_leeg", new Vector2(GameEnvironment.Screen.X - 50 - (40 * i), 0)));
                livesBig.Add(liveGreen);
            }

            this.Add(bigPlayer);
            this.Add(smallPlayer);

            this.Add(livesSmall);
            this.Add(livesBig);
        }


        public override void Update(GameTime gameTime)
        {
            foreach (SpriteGameObject tile in levelGen.tiles)
            {
                if (tile != null)
                {
                    if (tile.CollidesWith(smallPlayer))
                    {
                        Console.WriteLine(tile);
                        groundLevel = tile.Position.Y;
                        smallPlayer.OnGround(groundLevel);
                    }
                }
            }
            
            //smallPlayer.OnGround(500);
            smallPlayer.hitWallLeft(0);
            smallPlayer.hitWallRight(1700);

            bigPlayer.OnGround(300 - bigPlayer.Sprite.Height / 2 + 10);
            bigPlayer.hitWallLeft(0);
            bigPlayer.hitWallRight(1700);

            //Test for losing a live. You can comment these if-statements if it's annoying for you.
            if (smallPlayer.jump)
            {
                livesSmallPlayer--;
                livesSmall.Children[livesSmallPlayer].Velocity = new Vector2(0, -20);
            }

            if (bigPlayer.jump)
            {
                livesBigPlayer--;
                livesBig.Children[livesBigPlayer].Velocity = new Vector2(0, -20);
            }
            base.Update(gameTime);
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
        }
    }
}