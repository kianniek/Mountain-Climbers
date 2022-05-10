using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject
{
    class FallingRock : SpriteGameObject
    {
        public bool fall;
        public int fallingRockCount;
        float gravity;
        Vector2 resetPosition;
        public FallingRock(string assetName, Vector2 rockPosition) : base(assetName)
        {
            resetPosition = rockPosition;
            Reset();
            velocity.Y = 100;
            fall = false;
            fallingRockCount = 240;
            gravity = 1.5f;
        }

        public override void Update(GameTime gameTime)
        {
            velocity.Y += gravity;

            if (fall)
            {
                fallingRockCount--;
            }

            if (fallingRockCount < 0)
            {
                Reset();
                fall = false;
            }

            base.Update(gameTime);
        }

        public override void Reset()
        {
            fallingRockCount = 240;
            velocity.Y = 100;
            position = resetPosition;
            visible = true;
            base.Reset();
        }
    }
}
