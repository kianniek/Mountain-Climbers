using Microsoft.Xna.Framework;

namespace BaseProject
{
    class ClimbWall : SpriteGameObject
    {
        public ClimbWall(string assetName, Vector2 climbPosition) : base(assetName)
        {
            position = climbPosition;
            id = Tags.ClimebleWall.ToString();
        }
    }
}
