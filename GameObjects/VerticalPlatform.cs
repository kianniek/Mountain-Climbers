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
            this.moveSpeed = 100;
            origin = new Vector2(Width/2, Height/2);
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
            if (positionDiff == 0)
            {
                movingLeft = false;
                movingRight = true;
                velocity = new Vector2(moveSpeed, 0);
            }
            if (positionDiff == moveDistance)
            {
                movingLeft = true;
                movingRight = false;
                velocity = new Vector2(-moveSpeed, 0);
            }
            MoveLeft();
            MoveRight();
        }

        public void MoveLeft()
        {
            if (movingLeft)
            {
                positionDiff--;
            }
        }

        public void MoveRight()
        {
            if (movingRight)
            {
                positionDiff++;
            }
        }
    }
}
