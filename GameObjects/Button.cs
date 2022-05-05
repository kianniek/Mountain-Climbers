﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects
{
    class Button : SpriteGameObject
    {
        public Button() : base("new_button", layer : 1)
        {
            origin = Center;
            position.X = 200;
            position.Y = 290;
        }

        //collision zit bij PlayingState

    }
}
