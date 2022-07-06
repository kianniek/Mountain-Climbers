using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using BaseProject.GameObjects;
using BaseProject.GameStates;
using System.Linq;


namespace BaseProject.GameObjects
{
    public class CannonBarrel : RotatingSpriteGameObject
    {
        int canonRange = 400;
        Vector2 targetPosition;
        BigPlayer bigPlayer;
        SmallPlayer smallPlayer;
        Projectile cannonBall;

        GameObjectList cannonBalls;

        int rotateOffsetCanon = 20;
        int aimingOffset = 270;

        public CannonBarrel(Vector2 position, BigPlayer bigPlayer, SmallPlayer smallPlayer, GameObjectList cannonBalls) : base("CanonBarrel")
        {
            this.position = position;
            Origin = new Vector2(Width / 2, Height + rotateOffsetCanon);
            this.bigPlayer = bigPlayer;
            this.smallPlayer = smallPlayer;
            this.cannonBalls = cannonBalls;
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
            float smallPlayerDistanceToCannon = Vector2.Distance(position, smallPlayer.position);
            float bigPlayerDistanceToCannon = Vector2.Distance(position, bigPlayer.position);

            //Console.WriteLine(smallPlayerDistanceToCanon);

            if ((smallPlayerDistanceToCannon > canonRange && bigPlayerDistanceToCannon > canonRange) || !visible)
            {
                return;
            }

            if (smallPlayerDistanceToCannon <= canonRange || bigPlayerDistanceToCannon <= canonRange)
            {
                if (smallPlayerDistanceToCannon < bigPlayerDistanceToCannon)
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

            cannonBalls.Add(new Projectile(directionToPlayer, bigPlayer, smallPlayer));
        }
    }
}
