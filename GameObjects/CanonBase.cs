using System;
using System.Collections.Generic;
using System.Text;
using BaseProject.GameObjects;
using BaseProject.GameStates;
using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects
{
    public class CannonBase : SpriteGameObject
    {
        BigPlayer bigPlayer;
        SmallPlayer smallPlayer;
        public CannonBase(Vector2 position, BigPlayer bigPlayer, SmallPlayer smallPlayer) : base("CanonBase")
        {
            //Midden onder
            Origin = Center;
            this.position = position;
            this.bigPlayer = bigPlayer;
            this.smallPlayer = smallPlayer;
        }

        public override void Update(GameTime gameTime)
        {
           // GetDestroyed();
            base.Update(gameTime);
        }
        public void GetDestroyed()
        {
                visible = false;
        }
    }
}
