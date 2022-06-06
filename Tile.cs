using Microsoft.Xna.Framework;

namespace BaseProject
{
    public class Tile : SpriteGameObject
    {
        protected SmallPlayer smallPlayer;
        protected BigPlayer bigPlayer;

        public Tile(string assetName, Vector2 position, float scale, SmallPlayer smallPlayer, BigPlayer bigPlayer) : base(assetName, 0, "", 0, scale)
        {
            Origin = Center;
            Position = position;
            this.smallPlayer = smallPlayer;
            this.bigPlayer = bigPlayer;
        }
    }
}