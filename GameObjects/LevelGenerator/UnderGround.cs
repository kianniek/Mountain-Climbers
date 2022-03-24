using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameObjects
{
    internal class UnderGround : Ground
    {
        public UnderGround(string assetName = "Tile_dirt", float scale = 1.6f) : base("Tile_dirt", scale: 1.6f)
        {
            position = new Vector2(Game1.Screen.X / 2, Game1.Screen.Y / 2);
            shade = Color.White;
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
