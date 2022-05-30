using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject
{
    class FallingRock : RotatingSpriteGameObject
    {
        public bool fall, playFallMusic, closeByRock;
        public int fallingRockCount;
        float gravity;
        Vector2 resetPosition;

        public SmallPlayer smallPlayer;
        public BigPlayer bigPlayer;

        public FallingRock(string assetName, Vector2 rockPosition, SmallPlayer smallPlayer, BigPlayer bigPlayer) : base(assetName)
        {
            resetPosition = rockPosition;
            Origin = Center;
            fallingRockCount = 240;
            gravity = 1.5f;
            velocity.Y = 100;
            fall = false;
            closeByRock = false;

            this.smallPlayer = smallPlayer;
            this.bigPlayer = bigPlayer;

            Reset();
        }

        public override void Update(GameTime gameTime)
        {
            velocity.Y += gravity;

            Angle += 0.1f;

            if (fall)
            {
                fallingRockCount--;
            }

            if (fallingRockCount < 0)
            {
                Reset();
                fall = false;
            }

            if (playFallMusic && closeByRock)
            {
                playFallMusic = false;
                GameEnvironment.AssetManager.PlaySound("falling");
            }

            Vector2 rockSmallPlayer = Position - smallPlayer.Position;
            Vector2 rockBigPlayer = Position - bigPlayer.Position;

            //Music is playing when both player distances < 30
            if (rockSmallPlayer.X < 30 || rockBigPlayer.X < 30)
            {
                closeByRock = true;
            }
            else
            {
                closeByRock = false;
            }

            //Resets rock if rock is off screen
            if (Position.Y > GameEnvironment.Screen.Y - smallPlayer.state.cam._transform.M42)
            {
                Reset();
            }

            //Rock hits one of the players and that causes knockback
            if (CollidesWith(smallPlayer))
            {
                smallPlayer.knockback = true;
            }

            if (CollidesWith(bigPlayer))
            {
                bigPlayer.knockback = true;
            }
            else
            {
                bigPlayer.hitRock = false;
            }

            base.Update(gameTime);
        }

        public override void Reset()
        {
            fallingRockCount = 240;
            velocity.Y = 100;
            position = resetPosition;
            visible = true;
            playFallMusic = true;
            base.Reset();
        }
    }
}
