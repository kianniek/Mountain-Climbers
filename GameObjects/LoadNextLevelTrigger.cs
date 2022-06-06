using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects
{
    public class LoadNextLevelTrigger : SpriteGameObject
    {
        protected SmallPlayer smallPlayer;
        protected BigPlayer bigPlayer;
        public LoadNextLevelTrigger(string assetName, Vector2 position, float scale, SmallPlayer smallPlayer, BigPlayer bigPlayer) : base(assetName, 0, "", 0, scale)
        {
            Origin = Center;
            Position = position;
            this.smallPlayer = smallPlayer;
            this.bigPlayer = bigPlayer;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (CollidesWith(smallPlayer) && CollidesWith(bigPlayer))
            {
                LevelManager.GoToNextLevel();
            }
        }
    }
}
