﻿using BaseProject.GameStates;
using Microsoft.Xna.Framework;
using System;

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
            GameStateManager.AddGameState("Credits", new CreditState());
            GameStateManager.AddGameState("LoadingState", new LoadingState());
            GameStateManager.AddGameState("PlayingState", new PlayingState(Camera));
            GameStateManager.AddGameState("ControlsMenu", new ControlsMenu());
            GameStateManager.AddGameState("GameOverMenu", new GameOverMenu(Camera));
            GameStateManager.SwitchTo("PlayingState");
            GameStateManager.AddGameState("MainMenu", new MainMenu(Camera));
            GameStateManager.SwitchTo("MainMenu");
            
            
        }

    }
}
