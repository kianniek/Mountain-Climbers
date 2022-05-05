﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameObjects
{
    public class Ground : SpriteGameObject
    {
        public Ground(string assetName = "Tile_dirt") : base(assetName)
        {
            origin = Center;
            position = new Vector2(Game1.Screen.X / 2, Game1.Screen.Y / 2);
            id = Tags.Ground.ToString();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

        }
    }
}
