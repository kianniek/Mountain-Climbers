using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseProject.GameStates
{
    public class PlayingState : GameObjectList
    {
        Spikes spike;

        Player player;
        public PlayingState()
        {
            spike = new Spikes();
            this.Add(spike);
            player = new Player();

            this.Add(player);
        }

        public override void Update(GameTime gameTime)
        {
            player.OnGround(300);
            player.hitWallLeft(0);
            player.hitWallRight(700);
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