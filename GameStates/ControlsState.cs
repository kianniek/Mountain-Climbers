using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameStates
{
    internal class ControlsMenu : GameObjectList
    {
        backgroundMenu background;
        ControlsMenu controls;
       

        

        public ControlsMenu() : base()
        {
            background = new backgroundMenu();
            Add(background);

           






        }



    }
}
