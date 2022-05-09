using Microsoft.Xna.Framework;

namespace BaseProject
{
    public class Tile : SpriteGameObject
    {
        public Tile(string assetName, Vector2 position, float scale) : base(assetName, 0, "", 0, scale)
        {
            Origin = Center;
            Position = position;
        }
    }
}