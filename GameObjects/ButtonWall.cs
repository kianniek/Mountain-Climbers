using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects
{
    public class ButtonWall : RotatingSpriteGameObject
    {
        private Vector2 endPosition;
        private Vector2 startPosition;
        private float wallSpeed = 0.01f;
        public bool isHorizontal = false;

        public Vector2 EndPosition { get { return endPosition; } }
        public Vector2 StartPosition { get { return startPosition; } }
        public float WallSpeed { get { return wallSpeed; } }
        public ButtonWall(Vector2 startPosition, Vector2 endPosition) : base("testWall")
        {

            position = startPosition;
            this.endPosition = endPosition;
            this.startPosition = startPosition;
            origin = Center;

            Angle = isHorizontal ? MathF.PI / 2 : 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
