using Microsoft.Xna.Framework;

namespace BaseProject.GameStates
{
    public class LoadingState : GameObjectList
    {
        //public int levelNumber = 0;
        public LoadingState() : base()
        {
            TextGameObject text = new TextGameObject("GameFont")
            {
                Text = "   loading...",
                Position = new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2)
            };
            Add(text);
            text.Position -= new Vector2(text.Size.X / 2, text.Size.Y / 2);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            GameEnvironment.GameStateManager.SwitchTo("PlayingState");
        }
    }
}
