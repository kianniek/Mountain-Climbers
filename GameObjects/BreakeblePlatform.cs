using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameObjects
{
    public class BreakeblePlatform : Ground
    {
        public bool isBreaking;
        public bool broken;
        int breakingStage = 0;
        int preFrameSec; //store second cound of previous frame
        string[] frames =
        {
            "BreakPlatformAnim/CrumbelingBlockAnim1",
            "BreakPlatformAnim/CrumbelingBlockAnim2",
            "BreakPlatformAnim/CrumbelingBlockAnim3",
            "BreakPlatformAnim/CrumbelingBlockAnim4",
            "BreakPlatformAnim/CrumbelingBlockAnim5",
            "BreakPlatformAnim/CrumbelingBlockAnim6",
            "BreakPlatformAnim/CrumbelingBlockAnim7"
        };
        public BreakeblePlatform(Vector2 pos) : base("BreakPlatformAnim/CrumbelingBlockAnim1")
        {
            position = pos;
            origin = Center;
            broken = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            int milliSecNow = (int)gameTime.TotalGameTime.TotalMilliseconds/10;
            if (breakingStage == frames.Length -1)
            {
                isBreaking = false;
                broken = true;
            }

            if (breakingStage == frames.Length - 1)
            {
                if (milliSecNow % 100 == 0)
                {
                    breakingStage = 0;
                    broken = false;
                }
            }

            if (isBreaking)
            {
                if (milliSecNow != preFrameSec && milliSecNow % 10 == 0)
                {
                    breakingStage++;
                    Console.Write(breakingStage);
                }
            }

            preFrameSec = (int)gameTime.TotalGameTime.TotalSeconds;
            Sprite = new SpriteSheet(frames[breakingStage]);
        }
    }
}
