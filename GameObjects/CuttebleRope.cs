using BaseProject.GameStates;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameObjects
{
    public class CuttebleRope : SpriteGameObject
    {
        public int x;
        public int y;
        public bool isOut, outLeft, outRight;
        bool changeSprite = false;
        public Level level;

        public CuttebleRope(Level level, int x, int y) : base("RopePile")
        {
            origin = Center;
            this.y = y;
            this.x = x;
            this.level = level;
            position = new Vector2(x, y);
            id = Tags.Interactible.ToString();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (isOut && !changeSprite)
            {
                changeSprite = true;
                if (level.TileOnLocation(x - 1, y + 1))
                {
                    //left
                    changeSprite = true;
                    Sprite = new SpriteSheet("RopeAnchorLeft");
                }
                else if (level.TileOnLocation(x + 1, y + 1))
                {
                    //right
                    changeSprite = true;
                    Sprite = new SpriteSheet("RopeAnchorRight");
                }
                else
                {
                    Sprite = new SpriteSheet("RopeAnchorRight");
                }
            }
        }

    }
}
