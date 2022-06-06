using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using BaseProject.GameStates;

namespace BaseProject.GameObjects
{
    public class ThrowDirection : RotatingSpriteGameObject
    {
        private float throwAngle;
        BigPlayer bigPlayer;
        SmallPlayer smallPlayer;
        float throwVelocityMultiplierX;
        float throwVelocityMultiplierY;
        int pickupOffsetX;
        int throwPower = 800;
        public ThrowDirection(BigPlayer bigPlayer, SmallPlayer smallPlayer) : base("aimlijn")
        {
            Origin = Center - Vector2.UnitX * Width / 2f;
            this.bigPlayer = bigPlayer;
            this.smallPlayer = smallPlayer;
        }

        public override void Update(GameTime gameTime)
        {
            throwVelocityMultiplierX = MathF.Cos(throwAngle);
            throwVelocityMultiplierY = MathF.Sin(throwAngle);

            base.Update(gameTime);
            if (smallPlayer.beingHeld)
            {
                position = smallPlayer.Position - new Vector2(pickupOffsetX, 0);
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
            float throwVelX = throwVelocityMultiplierX * throwPower;
            float throwVelY = throwVelocityMultiplierY * throwPower;
            smallPlayer.SetVelocity(new Vector2(throwVelX, throwVelY));
            smallPlayer.beingThrown = true;
        }
    }

}
