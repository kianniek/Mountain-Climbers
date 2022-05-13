using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using BaseProject.GameStates;

namespace BaseProject.GameObjects
{
    class ThrowDirection : RotatingSpriteGameObject
    {
        private float throwAngle;
        BigPlayer bigPlayer;
        SmallPlayer smallPlayer;

        public ThrowDirection(BigPlayer player, SmallPlayer player2) : base("aimlijn")
        {
            Origin = Center - Vector2.UnitX * Width / 2f;
            this.bigPlayer = player;
            this.smallPlayer = player2;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
           // position = smallPlayer.Position - new Vector2(10, 10);
            Angle = throwAngle;
        }

        public void IncreaseAngle(float value)
        {
            throwAngle += value;
        }

        public void DecreaseAngle(float value)
        {
            throwAngle -= value;
        }
    }

}
