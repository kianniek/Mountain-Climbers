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
            camera.Pos = new Vector2(GameEnvironment.Screen.X/2, GameEnvironment.Screen.Y / 2);
            GameEnvironment.AssetManager.PlaySound("intro");

            background = new backgroundMenu();
            background.Position = new Vector2(0,0);
            Add(background);

            title = new Title(new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 4), "titleGame");
            title.Origin = title.Center;
            Add(title);

            startButton = new MenuButton("teststart", Vector2.Zero);
            startButton.Position = new Vector2(0, GameEnvironment.Screen.Y / 1.5f);
            Add(startButton);

            creditsButton = new CreditsButton();
            creditsButton.Position = new Vector2(GameEnvironment.Screen.X - creditsButton.Width, GameEnvironment.Screen.Y / 1.5f);
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
