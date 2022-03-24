using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseProject.GameStates
{
    public class PlayingState : GameObjectList
    {
        SmallPlayer smallPlayer;
        BigPlayer bigPlayer;
        public PlayingState()
        {
            smallPlayer = new SmallPlayer();
            bigPlayer = new BigPlayer();
            LevelGenerator levelGen = new LevelGenerator();

            this.Add(bigPlayer);
            this.Add(smallPlayer);

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
        }

        public override void Update(GameTime gameTime)
        {
            smallPlayer.OnGround(300);
            smallPlayer.hitWallLeft(0);
            smallPlayer.hitWallRight(700);

            bigPlayer.OnGround(300 - bigPlayer.Sprite.Height/2 + 10);
            bigPlayer.hitWallLeft(0);
            bigPlayer.hitWallRight(700);
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