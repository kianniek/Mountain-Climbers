using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using BaseProject.GameStates;
using Microsoft.Xna.Framework.Audio;

namespace BaseProject
{
    public class HeadPlayer : SpriteGameObject
    {
        public bool isDead;
        public float gravity = 20f;
        public bool left, right, jump, stand, hitClimbWall, zPressed, mPressed, noLeft, noRight, climb, hitRock, hitWaterfall, hitRope, playJump, playWalk, throwToWaterfall;

        public static float JumpForce = 500;
        public float horizontalSpeed = 175;
        public static float walkingSpeed = 175;
        public static float sprintingSpeed = 200;

        public Vector2 LastSavedPos;
        public int savePosTimer;

        public bool knockback;
        public int knockbackForce = 100;
        public int musicCounter = 30;

        public LevelManager levelManager;

        protected Tile[,] WorldTiles { get; private set; }
        protected Level level;

        public HeadPlayer(string assetName) : base(assetName)
        {
            position.Y = GameEnvironment.Screen.Y / 1.4f;
            position.X = 10;
            noLeft = false;
            noRight = false;
            playJump = true;
            playWalk = true;
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
                throwToWaterfall = false;
            }

            if (left)
            {
                velocity.X = -horizontalSpeed;
                musicCounter--;
                left = false;
            }

            if (right)
            {
                velocity.X = horizontalSpeed;
                musicCounter--;
                right = false;
            }

            if (musicCounter < 0)
            {
                musicCounter = 30;
            }

            if (position.Y > 1580)
            {
                isDead = true;
            }

            


            base.Update(gameTime);
            velocity.Y += gravity;
            if (stand)
            {
                velocity.X = 0;
            }

            Console.WriteLine(musicCounter);
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