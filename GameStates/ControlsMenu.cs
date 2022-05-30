using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameStates
{
    internal class ControlsMenu : GameObjectList
    {
        backgroundMenu background;
        Controls controls;
        MenuButton button;
        Abutton a;



        public ControlsMenu() 
        {
            background = new backgroundMenu();
            background.Position = Vector2.Zero;

            Add(background);

            Console.WriteLine(background.Position);

            controls = new Controls();
            controls.Position = new Vector2(GameEnvironment.Screen.X/2, GameEnvironment.Screen.Y/2);
            Add(controls);

            a = new Abutton();
            a.Position = new Vector2(220, -20);
            a.Scale = 1.5f;
            Add(a);

            button = new MenuButton("backbutton", new Vector2(-200, -100));
            button.Scale = 0.5f;
            Add(button);

        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                GameEnvironment.GameStateManager.SwitchTo("MainMenu");
            }
        }

       /* public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (inputHelper.IsKeyDown(Keys.A))
            {

                GameEnvironment.GameStateManager.SwitchTo("GameOverMenu");
            }
        }*/


    }
}
