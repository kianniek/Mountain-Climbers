using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameObjects
{
    public class Lava : SpriteGameObject
    {
        public int x;
        public int y;
        public LevelGenerator levelGen;
        private bool changeSprite;

        public Lava(LevelGenerator levelGen, int x, int y, string assetname = "Lava") : base(assetname)
        {
            origin = Center;
            this.y = y;
            this.x = x;
            this.levelGen = levelGen;
            id = Tags.Lava.ToString();
            CheckIfSurfice();
        }
        void CheckIfSurfice()
        {
            if (levelGen != null)
            {
                if (levelGen.tiles[x, y - 1] == null)
                {
                    if (!changeSprite)
                    {
                        changeSprite = true;
                        Sprite = new SpriteSheet("LavaTop");
                    }
                }
            }
        }
    }
}
