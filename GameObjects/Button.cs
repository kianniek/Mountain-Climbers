using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects
{
    class Button : SpriteGameObject
    {
        bool ButtonPress = false;

        int distance;

        private ButtonWall wall;
        private SmallPlayer smallPlayer;
        private BigPlayer bigPlayer;

        public Button(SmallPlayer smallPlayer, BigPlayer bigPlayer, ButtonWall wall) : base("new_button")
        {
            position.X = 500;
            position.Y = 1175;
            this.wall = wall;
            this.smallPlayer = smallPlayer;
            this.bigPlayer = bigPlayer;

        }



        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);


            if (smallPlayer.CollidesWith(this) && inputHelper.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                ButtonPress = true;
            }

            if (ButtonPress)
            {
                Console.WriteLine("lets go");

                wall.Velocity = new Vector2(0, -50);


            }

            if (smallPlayer.CollidesWith(wall))
            {

                Console.WriteLine("hit");

               


                smallPlayer.Velocity = Vector2.Zero;
            }

            if (wall.Position.Y < 900)
            {
                ButtonPress = false;

            }

            if (ButtonPress)
            {
                Console.WriteLine("pressed");
            }
            else if (!ButtonPress)
            {
                Console.WriteLine("notPressed");



            }







        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);


            int distance = (int)(wall.Position.X - smallPlayer.Position.X);



        }


    }
}
