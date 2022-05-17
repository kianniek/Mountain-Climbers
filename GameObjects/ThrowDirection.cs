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
        float cosVel;
        float sinVel;
        public ThrowDirection(BigPlayer bigPlayer, SmallPlayer smallPlayer) : base("aimlijn")
        {
            Origin = Center - Vector2.UnitX * Width / 2f;
            this.bigPlayer = bigPlayer;
            this.smallPlayer = smallPlayer;

            
        }

        public override void Update(GameTime gameTime)
        {
            cosVel = MathF.Cos(throwAngle);
            sinVel = MathF.Sin(throwAngle);
            base.Update(gameTime);
            if (smallPlayer.beingHeld)
            {
                position = smallPlayer.Position - new Vector2(10, 10);
                visible = true;
            }

            if(!smallPlayer.beingHeld)
            {
                visible = false;
            }
            
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

        public void ThrowPlayer()
        {
            smallPlayer.beingHeld = false;
            bigPlayer.holdingPlayer = false;
            float throwVelX = cosVel * 500;
            float throwVelY = sinVel * 500;
            smallPlayer.SetVelocity(new Vector2(throwVelX, throwVelY));
            
        }
    }

}
