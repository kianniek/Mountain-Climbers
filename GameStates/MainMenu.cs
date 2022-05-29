using System;
using System.Collections.Generic;
using System.Text;
using BaseProject.Engine;
using Microsoft.Xna.Framework;

namespace BaseProject.GameStates
{
    class MainMenu : GameObjectList
    {
        backgroundMenu background;
        Title title;
        MenuButton startButton;
        CreditsButton creditsButton;
        SelectSprite select;
        public MainMenu(Camera camera) 
        {
            GameEnvironment.AssetManager.PlaySound("intro");

            background = new backgroundMenu();
            background.Position = new Vector2(-background.Width/2, 80);
            Add(background);

            title = new Title(new Vector2(-GameEnvironment.Screen.X/3,100), "titleGame");
            Add(title);

            startButton = new MenuButton("teststart", new Vector2(-GameEnvironment.Screen.X / 3, 300));
            Add(startButton);

            creditsButton = new CreditsButton();
            creditsButton.Position = new Vector2(-GameEnvironment.Screen.X/2, 480);
            Add(creditsButton);

            select = new SelectSprite();
            Add(select);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (select.Position.X == -400 && select.Position.Y == 1250 && inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                GameEnvironment.GameStateManager.SwitchTo("LoadingState");
            }
            if (select.Position.X == 700 && select.Position.Y == 1250 && inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                GameEnvironment.GameStateManager.SwitchTo("Credits");
            }
        }
    }
}
