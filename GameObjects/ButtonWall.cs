using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects
{
    public class ButtonWall : SpriteGameObject
    {
        private Vector2 endPosition;
        
        public Vector2 EndPosition { get { return endPosition; } }
        public ButtonWall(Vector2 startPosition, Vector2 endPosition) : base("testWall")
        {

            position = startPosition;
            this.endPosition = endPosition;
            origin = Center;

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
