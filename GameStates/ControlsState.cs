using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameStates
{
    internal class ControlsState : GameObjectList
    {
        public ControlsState() : base()
        {
            Add(new SpriteGameObject("CP"));
        }



    }
}
