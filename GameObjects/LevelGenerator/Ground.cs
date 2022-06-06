using Microsoft.Xna.Framework;

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
    }
}
