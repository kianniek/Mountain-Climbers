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

        public FallingRock(string assetName, Vector2 rockPosition) : base(assetName)
        {
            resetPosition = rockPosition;
            Reset();
            velocity.Y = 100;
            fall = false;
            closeByRock = false;
            fallingRockCount = 240;
            gravity = 1.5f;
            Origin = Center;
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
