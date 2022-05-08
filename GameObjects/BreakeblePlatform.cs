using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameObjects
{
    public class BreakeblePlatform : Ground
    {
        public bool isBreaking;
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
        public BreakeblePlatform() : base("BreakPlatformAnim/CrumbelingBlockAnim1")
        {
            origin = Center;
            id = Tags.BreakeblePlatform.ToString();

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            int milliSecNow = (int)gameTime.TotalGameTime.TotalMilliseconds/10;
            if (breakingStage == frames.Length -1)
            {
                isBreaking = false;
                id = "";
            }

            if (breakingStage == frames.Length - 1)
            {
                if (milliSecNow % 100 == 0)
                {
                    breakingStage = 0;
                    id = Tags.BreakeblePlatform.ToString();
                }
            }

            if (isBreaking)
            {
                
                if (milliSecNow != preFrameSec && milliSecNow % 10 == 0)
                {
                    breakingStage++;
                }
            }

            preFrameSec = (int)gameTime.TotalGameTime.TotalSeconds;
            Sprite = new SpriteSheet(frames[breakingStage]);
        }
    }
}
