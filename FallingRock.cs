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
        Vector2 resetPosition;
        public FallingRock(string assetName, Vector2 rockPosition) : base(assetName)
        {
            resetPosition = rockPosition;
            Reset();
            velocity.Y = 100;
            fall = false;
            fallingRockCount = 240;
        }

        public override void Update(GameTime gameTime)
        {
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
           position = resetPosition;
            visible = true;
           base.Reset();
       }
    }
}
