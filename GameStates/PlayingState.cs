using BaseProject.GameObjects;
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

            this.Add(bigPlayer);
            this.Add(smallPlayer);
            
            
            Add(new StrongWind(GameEnvironment.Screen.ToVector2()/2, 5, 200, WindDirection.Left));
            Add(new Box(GameEnvironment.Screen.ToVector2()/2));
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