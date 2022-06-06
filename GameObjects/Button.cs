using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects
{
    class Button : SpriteGameObject
    {
        public bool ButtonPress = false;

        int distance;

        private ButtonWall wall;
        private SmallPlayer smallPlayer;
        private BigPlayer bigPlayer;

        public Button(SmallPlayer smallPlayer, BigPlayer bigPlayer, ButtonWall wall, Vector2 pos) : base("new_button")
        {
            position = pos;
            this.wall = wall;
            this.smallPlayer = smallPlayer;
            this.bigPlayer = bigPlayer;

        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            int distance = (int)(wall.Position.X - smallPlayer.Position.X);
            if (ButtonPress)
            {
                wall.Position = Vector2.Lerp(wall.Position, wall.EndPosition, wall.WallSpeed);
            }

        }
    }
}
