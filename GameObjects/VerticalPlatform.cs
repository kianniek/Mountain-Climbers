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
        int moveDistance;
        int moveSpeed;
        Vector2 startPosition = new Vector2(250, 500);
        public VerticalPlatform(int moveDistance, int moveSpeed) : base("platform")
        {
            position = startPosition;
            this.moveDistance = moveDistance;
            this.moveSpeed = moveSpeed;

            Origin = Center;
            velocity = new Vector2(moveSpeed, 0);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //Console.WriteLine(velocity);

            if (position.X < startPosition.X || position.X > startPosition.X + moveDistance)
            {
                movingLeft = !movingLeft;
                velocity.X *= -1;
            }
            //MovePlayerWithPlatform();
        }

        /*public void MovePlayerWithPlatform()
        {
            if (CollidesWith(bigPlayer))
            {
                var mx = (bigPlayer.position.X - position.X);
                var my = (bigPlayer.position.Y - position.Y);
                if (Math.Abs(mx) > Math.Abs(my))
                {
                    if (mx > 0)
                    {

                        bigPlayer.position.X = position.X + bigPlayer.Height / 2 + Height / 2;
                        //komt van links
                    }

                    if (mx < 0)
                    {
                        bigPlayer.position.X = position.X - bigPlayer.Height / 2 - Height / 2;
                        //komt van rechts
                    }
                }
                else
                {
                    if (my > 0)
                    {
                        //komt van onder
                        velocity.Y = 0;
                        position.Y += 1;
                    }

                    if (my < 0)
                    {
                        //komt van boven
                        if (velocity.Y > 0)
                        {
                            velocity.Y = 0;

                        }
                        bigPlayer.position.X += (bigPlayer.velocity.X + velocity.X);
                        bigPlayer.position.Y = position.Y - Height;
                    }
                }
            }
        }*/
    }
}
