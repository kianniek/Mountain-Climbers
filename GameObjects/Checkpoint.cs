using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects
{
    public class Checkpoint : SpriteGameObject
    {

        bool CheckPointAchS = false;
        bool CheckpointAchB = false;

        private SmallPlayer smallplayer;
        private BigPlayer bigplayer;


        public Checkpoint(SmallPlayer smallPlayer, BigPlayer bigPlayer) : base("CP")
        {
            position.X = 800;
            position.Y = 1000;

            this.smallplayer = smallPlayer;
            this.bigplayer = bigPlayer;

            
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            //checkpoint respawn small player
            if (CheckPointAchS && smallplayer.isDead)
            {
                Console.WriteLine("cp");
                smallplayer.Position = position;
                smallplayer.isDead = false;
               
            }

            //checkpoint respawn big player
            if(CheckpointAchB && bigplayer.isDead)
            {
                bigplayer.Position = position;
                bigplayer.isDead = false;
            }



           
           
               



        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (smallplayer.CollidesWith(this))
            {
                CheckPointAchS = true;
                
            }

            if (bigplayer.CollidesWith(this))
            {
                CheckpointAchB = true;
                Console.WriteLine("big");
            }


           

           

        }


    }
}
