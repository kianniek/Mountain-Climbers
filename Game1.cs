using System;
using BaseProject.GameStates;
using Microsoft.Xna.Framework;

namespace BaseProject
{
    public class Game1 : GameEnvironment
    {      
        protected override void LoadContent()
        {
            base.LoadContent();
            Console.WriteLine("Started game " + DateTime.Now);

            screen = new Point(1920, 1080);
            ApplyResolutionSettings();

            // TODO: use this.Content to load your game content here
            GameStateManager.AddGameState("PlayingState", new PlayingState(Camera));
            GameStateManager.SwitchTo("PlayingState");
            
            
        }

    }
}
