using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameStates
{
    internal class ControlsMenu : GameObjectList
    {
        public ControlsMenu() : base()
        {
            Add(new SpriteGameObject("MainMenu"));
            
        }



    }
}
