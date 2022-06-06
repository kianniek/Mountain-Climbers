using Microsoft.Xna.Framework;

namespace BaseProject
{
    class Title : SpriteGameObject
    {
        public Title(Vector2 titlePosition, string assetName) : base(assetName)
        {
            position = titlePosition;//new Vector2(-100, 600);
        }
    }
}
