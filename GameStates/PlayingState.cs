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

        int livesSmallPlayer;
        int livesBigPlayer;
        //bool waar;
        private float groundLevel = Game1.Screen.Y;

        public PlayingState()
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
            waterfalls.Add(new Waterfall("Waterfall200", new Vector2(600, 500)));

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
        }


        public override void Update(GameTime gameTime)
        {
            foreach (SpriteGameObject tile in levelGen.tiles)
            {
                if (tile != null)
                {
                    if (tile.CollidesWith(smallPlayer))
                    {
                        //Console.WriteLine(tile);
                        groundLevel = tile.Position.Y + tile.Sprite.Height;
                        //smallPlayer.OnGround(groundLevel);
                    }
                    if (tile.CollidesWith(bigPlayer))
                    {
                        //Console.WriteLine(tile);
                        groundLevel = tile.Position.Y + tile.Sprite.Height;
                        //bigPlayer.OnGround(groundLevel);
                    }

                    //Test collision Rock - Ground
                    foreach(FallingRock rock in rocks.Children)
                    {

                        if (tile.CollidesWith(rock))
                        {
                            rock.fall = true;
                            rock.Visible = false;
                        }
                    }
                }
            }            

            //smallPlayer.hitWallLeft(0);
            //smallPlayer.hitWallRight(1700);

            //bigPlayer.hitWallLeft(0);
            //bigPlayer.hitWallRight(1700);

            foreach(Waterfall waterfall in waterfalls.Children)
            {
                if (waterfall.CollidesWith(smallPlayer))
                {
                    smallPlayer.hitWaterfall();
                }
                if (waterfall.CollidesWith(bigPlayer))
                {
                    bigPlayer.hitWaterfall();
                }
            }

            //Test for losing a live. You can comment these if-statements if it's annoying for you.
            /*if (smallPlayer.jump)
            {
                livesSmallPlayer--;
                livesSmall.Children[livesSmallPlayer].Velocity = new Vector2(0, -20);
            }

            if (bigPlayer.jump)
            {
                livesBigPlayer--;
                livesBig.Children[livesBigPlayer].Velocity = new Vector2(0, -20);
            }*/

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