using System;
using System.Collections.Generic;
using System.Text;
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
        public MainMenu() 
        {
            GameEnvironment.AssetManager.PlaySound("intro");

            background = new backgroundMenu();
            Add(background);

            title = new Title();
            Add(title);

            startButton = new MenuButton("teststart", new Vector2(-1400, 100));
            Add(startButton);

            creditsButton = new CreditsButton();
            Add(creditsButton);

            select = new SelectSprite();
            Add(select);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (select.Position.X == -700 && select.Position.Y == 530 && inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                GameEnvironment.GameStateManager.SwitchTo("LoadingState");
            }
            if (select.Position.X == 380 && select.Position.Y == 530 && inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                GameEnvironment.GameStateManager.SwitchTo("Credits");
            }
        }
    }
}
