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


        public Checkpoint(SmallPlayer smallPlayer, BigPlayer bigPlayer, Vector2 pos) : base("new_checkpoint")
        {
            position = pos;

            Origin = Center;
            scale = 0.75f;

            this.smallplayer = smallPlayer;
            this.bigplayer = bigPlayer;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            //checkpoint respawn small player
            if (CheckPointAchS && smallplayer.isDead)
            {

                smallplayer.Position = position;
                smallplayer.isDead = false;

            }

            //checkpoint respawn big player
            if (CheckpointAchB && bigplayer.isDead)
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
            }

            if (bigplayer.isDead)
            {
                bigplayer.Position = this.position;
                bigplayer.isDead=false;
                Console.WriteLine("big is dead");
            }
            if (smallplayer.isDead)
            {
                smallplayer.Position = this.position;
                smallplayer.isDead = false;
                Console.WriteLine("small is dead");
            }
        }
    }
}
