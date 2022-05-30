using Microsoft.Xna.Framework;
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



        public ControlsMenu() 
        {
            background = new backgroundMenu();
            Add(background);

            controls = new Controls();
            Add(controls);

            button = new MenuButton("backbutton", new Vector2(300, 500));
            //button.Scale = 0.5f;
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
    }
}
