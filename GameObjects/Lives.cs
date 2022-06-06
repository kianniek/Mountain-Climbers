using Microsoft.Xna.Framework;

namespace BaseProject
{
    public class Lives : SpriteGameObject
    {
        public Lives(string assetName, Vector2 startPosition) : base(assetName)
        {
            position = startPosition;
            velocity.Y = 0;
        }
    }
}
