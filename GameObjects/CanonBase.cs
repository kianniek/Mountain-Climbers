using System;
using System.Collections.Generic;
using System.Text;
using BaseProject.GameObjects;
using BaseProject.GameStates;
using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects
{
    public class CanonBase : SpriteGameObject
    {
        BigPlayer bigPlayer;
        SmallPlayer smallPlayer;
        public CanonBase(Vector2 position, BigPlayer bigPlayer, SmallPlayer smallPlayer) : base("CanonBase")
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
