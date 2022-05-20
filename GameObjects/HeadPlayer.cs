using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using BaseProject.GameStates;
using BaseProject.GameObjects;

namespace BaseProject
{
    public class HeadPlayer : SpriteGameObject
    {
        public bool isDead;
        public float gravity = 20f;
        public bool left, right, jump, stand, hitClimbWall, zPressed, mPressed, noLeft, noRight, climb, hitRock, hitWaterfall, hitRope;



        public static float JumpForce = 500;
        public float horizontalSpeed = 175;
        public static float walkingSpeed = 175;
        public static float sprintingSpeed = 200;

        public Vector2 LastSavedPos;
        public int savePosTimer;

        public bool knockback;
        public int knockbackForce = 100;

        public LevelManager levelManager;
        VerticalPlatform verticalPlatform;
        protected PlayingState state;

        protected Tile[,] WorldTiles { get; private set; }
        protected Level level;

        public HeadPlayer(string assetName, PlayingState playingState) : base(assetName)
        {
            position.Y = GameEnvironment.Screen.Y / 1.4f;
            position.X = 10;
            noLeft = false;
            noRight = false;
            state = playingState;
            verticalPlatform = playingState.verticalPlatform;
        }

        public override void Update(GameTime gameTime)
        {
            if (knockback)
            {
                velocity.X = -1 * knockbackForce;
                jump = true;
                knockback = false;
            }
            if (jump)
            {
                jump = false;
                stand = false;
                velocity.Y = -JumpForce;
            }

            if (left)
            {
                velocity.X = -horizontalSpeed;
                left = false;
            }
            if (right)
            {
                velocity.X = horizontalSpeed;
                right = false;
            }



            base.Update(gameTime);
            velocity.Y += gravity;
            if (stand)
            {
                velocity.X = 0;
            }
           
            CheckVerticalPlatformCollision();
        }

        public void CheckVerticalPlatformCollision()
        {
            var playerOrigin = Origin;
            Origin = Center;

            bool collidePlatformLeft = position.X + Width / 2 > verticalPlatform.Position.X - verticalPlatform.Width / 2;
            bool collidePlatformRight = position.X - Width / 2 < verticalPlatform.Position.X + verticalPlatform.Width / 2;
            bool collidePlatformTop = position.Y + Height/2 > verticalPlatform.Position.Y - verticalPlatform.Height/2;
            bool collidePlatformBottom = position.Y - Height / 2 < verticalPlatform.Position.Y + verticalPlatform.Height / 2;

           

            if (collidePlatformLeft && collidePlatformRight && collidePlatformTop && collidePlatformBottom)
            {
                if (position.X + Width/2 == verticalPlatform.Position.X - verticalPlatform.Width/2 && verticalPlatform.movingLeft)
                {
                    position.X = (verticalPlatform.Position.X - verticalPlatform.Width / 2 + Width / 2) -1;
                }
            }





            Origin = playerOrigin;
        }

        //Roep deze functie aan als de speler normaal springt en de waterval raakt,
        //maar zodra je de pickup gebruikt, roep deze niet aan.
        public virtual void HitWaterfall()
        {
            velocity.Y = 520;
        }



        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
        }


        public virtual void Climb()
        {
            left = false;
            right = false;
            stand = true;
            velocity.Y = 0;
            velocity.X = 0;
            climb = true;
        }

        public virtual void NotClimbing()
        {
            climb = false;
        }

        public virtual void Knockback()
        {
            velocity.Y *= -1;
            velocity.X *= -1;
        }

        public void GoToNewLevel(Level lvl, Vector2 pos)
        {
            level = lvl;
            position = pos;
            velocity = Vector2.Zero;
        }
    }
}
