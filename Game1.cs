using BaseProject.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BaseProject
{
    public class Game1 : GameEnvironment
    {      
        protected override void LoadContent()
        {
            base.LoadContent();

            screen = new Point(1920, 1080);
            
            ApplyResolutionSettings();

            // TODO: use this.Content to load your game content here 
            GameStateManager.AddGameState("PlayingState", new PlayingState());
            GameStateManager.SwitchTo("PlayingState");
            
            
        }

    }
}
