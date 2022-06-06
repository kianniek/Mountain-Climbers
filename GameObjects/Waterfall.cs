using Microsoft.Xna.Framework;

namespace BaseProject
{
    //Dion
    class Waterfall : SpriteGameObject
    {
        public Waterfall(Vector2 waterPosition) : base("Waterfall200", layer: -3)
        {
            position = waterPosition;
            origin = Center;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
