using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameStates
{
    internal class StartState : GameObjectList
    {
        public StartState() : base()
        {
            Add(new SpriteGameObject("MainMenu"));
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (inputHelper.AnyKeyPressed)
            {
                GameEnvironment.GameStateManager.SwitchTo("PlayingState");
            }


        }
    }
}
