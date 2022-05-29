using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects
{
    internal class PreviousLevelTrigger : RotatingSpriteGameObject
    {
        protected SmallPlayer smallPlayer;
        protected BigPlayer bigPlayer;
        public PreviousLevelTrigger(string assetName, Vector2 position, float scale, SmallPlayer smallPlayer, BigPlayer bigPlayer) : base(assetName, 0, "", 0, scale)
        {
            Origin = Center;
            Position = position;
            this.smallPlayer = smallPlayer;
            this.bigPlayer = bigPlayer;
            Mirror = true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (CollidesWith(smallPlayer) && CollidesWith(bigPlayer))
            {
                LevelManager.GoToPreviousLevel();
            }
        }
    }
}
