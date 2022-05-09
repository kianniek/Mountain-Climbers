using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects
{
    public class ButtonWall : SpriteGameObject
    {

        private Vector2 endPosition;

        public ButtonWall(Vector2 startPosition, Vector2 endPosition) : base("testWall")
        {

            position = startPosition;
            this.endPosition = endPosition;


        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (position.Y < endPosition.Y)
            {
                Velocity = Vector2.Zero;
            }

            



        }













    }
}
