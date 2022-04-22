using System;
using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects
{
    public class Box : SpriteGameObject, IWindObject
    {
        public Box(Vector2 position) : base("Player")
        {
            this.position = position;
            Origin = Center;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var wind = IWindObject.CurrentWind(this);
            
            if (wind != null)
                position += wind.WindForce(this, gameTime);
        }
    }
}