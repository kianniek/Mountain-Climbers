using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using BaseProject.GameObjects;
using BaseProject.GameStates;
using System.Linq;


namespace BaseProject.GameObjects
{
    public class CanonBarrel : RotatingSpriteGameObject
    {
        int canonRange = 400;
        Vector2 targetPosition;
        BigPlayer bigPlayer;
        SmallPlayer smallPlayer;
        Projectile canonBall;

        GameObjectList canonBalls;

        int rotateOffsetCanon = 20;
        int aimingOffset = 270;

        public CanonBarrel(Vector2 position, BigPlayer bigPlayer, SmallPlayer smallPlayer, GameObjectList canonBalls) : base("CanonBarrel")
        {
            this.position = position;
            Origin = new Vector2(Width / 2, Height + rotateOffsetCanon);
            this.bigPlayer = bigPlayer;
            this.smallPlayer = smallPlayer;
            this.canonBalls = canonBalls;
        }

        public override void Update(GameTime gameTime)
        {
            if (CollidesWith(bigPlayer) || CollidesWith(smallPlayer))
            {
                GetDestroyed();
            }

            CheckForInRangePlayers();

            base.Update(gameTime);


        }

        public void GetDestroyed()
        {
            visible = false;
            //canonBase.GetDestroyed();
        }

        public void CheckForInRangePlayers()
        {
            float smallPlayerDistanceToCanon = Vector2.Distance(position, smallPlayer.position);
            float bigPlayerDistanceToCanon = Vector2.Distance(position, bigPlayer.position);

            //Console.WriteLine(smallPlayerDistanceToCanon);

            if ((smallPlayerDistanceToCanon > canonRange && bigPlayerDistanceToCanon > canonRange) || !visible)
            {
                Degrees = 0;
                return;
            }

            if (smallPlayerDistanceToCanon <= canonRange || bigPlayerDistanceToCanon <= canonRange)
            {
                if (smallPlayerDistanceToCanon < bigPlayerDistanceToCanon)
                {
                    FireCanonball(smallPlayer);
                    LookAt(smallPlayer, aimingOffset);
                }
                else
                {
                    FireCanonball(bigPlayer);
                    LookAt(bigPlayer, aimingOffset);
                }
                //inRangeTargets.Add(player);
            }
        }

        public void FireCanonball(SpriteGameObject player)
        {
            //targetPosition = inRangeTargets.ElementAt(0).position;
            //Degrees = (float)Math.Tan(directionToPlayer.Y / directionToPlayer.X);

            targetPosition = player.position;
            Vector2 directionToPlayer = targetPosition - position;
            directionToPlayer.Normalize();

            canonBalls.Add(new Projectile(directionToPlayer, bigPlayer, smallPlayer));
        }
    }
}
