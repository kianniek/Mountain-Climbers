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
        public LevelGenerator levelGen;
        public bool isOut, outLeft, outRight;
        bool changeSprite = false;

        public CuttebleRope(LevelGenerator levelGen, int x, int y) : base("RopePile")
        {
            origin = Center;
            this.y = y;
            this.x = x;
            this.levelGen = levelGen;
            id = Tags.Interactible.ToString();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (isOut && !changeSprite)
            {
                if (levelGen.tiles[x - 1, y + 1] != null)
                {
                    //left
                    changeSprite = true;
                    Sprite = new SpriteSheet("RopeAnchorLeft");
                }
                else if (levelGen.tiles[x + 1, y + 1] != null)
                {
                    //right
                    changeSprite = true;
                    Sprite = new SpriteSheet("RopeAnchorRight");
                }
                
            }
        }

    }
}
