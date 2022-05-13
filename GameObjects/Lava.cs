using BaseProject.GameStates;
using Microsoft.Xna.Framework;
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
        Level level;

        public Lava(Level level, int x, int y, string assetname = "Lava") : base(assetname)
        {
            origin = Center;
            this.y = y;
            this.x = x;
            Position = new Vector2(x, y);
            this.level = level;
            id = Tags.Lava.ToString();
            CheckIfSurfice();
        }
        void CheckIfSurfice()
        {
            Console.WriteLine(level.TileOnLocation(x, y - 1));
                if (level.TileOnLocation(x, y - 1))
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
