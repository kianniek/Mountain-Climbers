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
        SpriteGameObject controls;
        public MainMenu(Camera camera)
        {
            camera.Pos = new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);
            GameEnvironment.AssetManager.PlaySound("intro");

            background = new backgroundMenu();
            background.Position = new Vector2(0, 0);
            Add(background);

            title = new Title(new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 4), "titleGame");
            title.Origin = title.Center;
            Add(title);

            startButton = new MenuButton("StartButton", Vector2.Zero);
            startButton.Position = new Vector2(150, GameEnvironment.Screen.Y / 1.5f);
            Add(startButton);

            creditsButton = new CreditsButton("CreditsButton", Vector2.Zero);
            creditsButton.Position = new Vector2(GameEnvironment.Screen.X - creditsButton.Width - 150, GameEnvironment.Screen.Y / 1.5f);
            Add(creditsButton);

            controls = new SpriteGameObject("controlsbutton");
            controls.Position = new Vector2(GameEnvironment.Screen.X / 2 - 200, GameEnvironment.Screen.Y / 1.3f);
            Add(controls);

            select = new SelectSprite();
            Add(select);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (select.Position.X == 630 && select.Position.Y == 875 && inputHelper.IsKeyDown(ButtonManager.Select))
            {
                GameEnvironment.GameStateManager.SwitchTo("ControlsMenu");
            }
            else
            if (select.Position.X == 0 && select.Position.Y == 725 && inputHelper.KeyPressed(ButtonManager.Select))//&& inputHelper.KeyPressed(ButtonManager.Start)
            {
                GameEnvironment.GameStateManager.SwitchTo("LoadingState");
            }
            else
            if (select.Position.X == 1150 && select.Position.Y == 725 && inputHelper.KeyPressed(ButtonManager.Select))
            {
                GameEnvironment.GameStateManager.SwitchTo("Credits");
            }
        }
    }
}
