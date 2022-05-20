using System;
using BaseProject;
using BaseProject.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BaseProject.GameStates;

namespace BaseProject.GameObjects
{
    public class VerticalPlatform : SpriteGameObject
    {

        public bool movingLeft, movingRight;
        float positionDiff = 0;
        int moveDistance;
        int moveSpeed;
        public VerticalPlatform(int moveDistance, int moveSpeed) : base("platform")
        {
            position = new Vector2(0, 300);
            this.moveDistance = moveDistance;
            this.moveSpeed = moveSpeed;
        }

        public override void Update(GameTime gameTime)
        {
            if (positionDiff == 0)
            {
                movingLeft = false;
                movingRight = true;
            }
            if (positionDiff == moveDistance)
            {
                movingLeft = true;
                movingRight = false;
            }
            MoveLeft();
            MoveRight();
        }

        public void MoveLeft()
        {
            if (movingLeft)
            {
                position.X -= moveSpeed;
                positionDiff -= moveSpeed;
            }
        }

        public void MoveRight()
        {
            if (movingRight)
            {
                position.X += moveSpeed;
                positionDiff += moveSpeed;
            }
        }
    }
}
