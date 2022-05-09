using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects
{
    class Button : SpriteGameObject
    {
       
        private ButtonWall wall;
        private SmallPlayer smallPlayer;
        private BigPlayer bigPlayer;

        public Button(SmallPlayer smallPlayer, BigPlayer bigPlayer, ButtonWall wall) : base("new_button")
        {
            position.X = 400;
            position.Y = 350;
            this.wall = wall;
            this.smallPlayer = smallPlayer;
            this.bigPlayer = bigPlayer;
        }



        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (smallPlayer.CollidesWith(this) && inputHelper.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                Console.WriteLine("lets go");
                wall.Velocity = new Vector2(0, -50);


            }





        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);




        }


    }
}
