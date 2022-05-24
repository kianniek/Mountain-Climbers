﻿using BaseProject.GameStates;
using Microsoft.Xna.Framework;

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
            //GameStateManager.AddGameState("StartState", new StartState());
            GameStateManager.AddGameState("MainMenu", new MainMenu());
            GameStateManager.AddGameState("Credits", new CreditState());
            GameStateManager.AddGameState("LoadingState", new LoadingState());
            GameStateManager.AddGameState("PlayingState", new PlayingState(Camera));
            GameStateManager.AddGameState("ControlsState", new ControlsState());
            GameStateManager.SwitchTo("PlayingState");
            GameStateManager.SwitchTo("MainMenu");
            
            
        }

    }
}
