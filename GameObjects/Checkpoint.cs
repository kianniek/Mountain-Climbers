using System;
using System.Collections.Generic;
using System.Text;
using BaseProject.GameStates;
using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects
{
    public class Checkpoint : SpriteGameObject
    {

        bool CheckPointAchS = false;
        bool CheckpointAchB = false;
        public Level Level { get; private set; }

       

        private SmallPlayer smallplayer;
        private BigPlayer bigplayer;

        private const float yOffset = -10f;

        private readonly SpriteSheet openSprite = new SpriteSheet("openFlag", 0);
        private readonly SpriteSheet closedSprite = new SpriteSheet("closedFlag", 0);



        public Checkpoint(SmallPlayer smallPlayer, BigPlayer bigPlayer, Vector2 pos, Level level) : base("closedFlag")
        {
            position = pos;
            Level = level;

            Origin = new Vector2(Center.X, Height - Level.TileHeight/2) + Vector2.UnitY * yOffset ;
            scale = 0.75f;

            this.smallplayer = smallPlayer;
            this.bigplayer = bigPlayer;
        }

       
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (PlayingState.activeCheckpoint != this)
            {
                Sprite = closedSprite;
                if (smallplayer.CollidesWith(this) && bigplayer.CollidesWith(this))
                {
                    PlayingState.activeCheckpoint = this;
                }
            }
            else
            {
                sprite = openSprite;
            }

            
            if (bigplayer.isDead && PlayingState.activeCheckpoint == this)
            {
                bigplayer.Position = this.position;
                bigplayer.isDead=false;
                Console.WriteLine("big is dead");
            }
            if (smallplayer.isDead && PlayingState.activeCheckpoint == this)
            {
                smallplayer.Position = this.position;
                smallplayer.isDead = false;
                Console.WriteLine("small is dead");
            }

           

            //checkpoint respawn big player
           
        }
    }
}
