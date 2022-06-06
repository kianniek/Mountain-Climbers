using Microsoft.Xna.Framework;

namespace BaseProject
{
    class CreditsButton : SpriteGameObject
    {
        public CreditsButton(string assetName, Vector2 newPosition) : base(assetName)
        {
            position = newPosition;
        }
    }
}
