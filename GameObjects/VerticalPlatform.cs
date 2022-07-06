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

        public bool movingLeft;
        float positionDiff = 0;
        int moveDistance;
        int moveSpeed = 100;
        public VerticalPlatform(int moveDistance, int moveSpeed) : base("platform")
        {
            position = new Vector2(0, 300);
            this.moveDistance = moveDistance;
            this.moveSpeed = moveSpeed;
            Origin = Center;   
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            velocity = new Vector2(moveSpeed, 0);

            if (positionDiff >= 0 || positionDiff <= moveDistance)
            {
                movingLeft = !movingLeft;
                velocity *= -1;
            }
            if (movingLeft)
            {
                positionDiff--;
            }
            else
            {
                positionDiff++;
            }
        }
    }
}
