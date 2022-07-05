using System;
using System.Collections.Generic;
using System.Text;
using BaseProject.GameObjects;
using BaseProject.GameStates;
using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects
{
    public class Projectile : SpriteGameObject
    {
        Vector2 direction;
        const int shootingPower = 25;
        BigPlayer bigPlayer;
        SmallPlayer smallPlayer;
        public Projectile(Vector2 direction, BigPlayer bigPlayer, SmallPlayer smallPlayer) : base("CanonBall")
        {
            this.direction = direction;
            this.bigPlayer = bigPlayer;
            this.smallPlayer = smallPlayer;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //Console.WriteLine(position);

            position += direction * shootingPower;
        }

        public void CollidesWithPlayer()
        {
            if(CollidesWith(bigPlayer)||CollidesWith(smallPlayer))
            {
                //Neem damage
            }
        }
    }
}
