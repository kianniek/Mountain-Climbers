using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BaseProject.GameStates
{
    class CreditState : GameObjectList
    {
        backgroundMenu background;
        GameObjectList text;
        MenuButton button;

        bool playMusic = true;
        public CreditState()
        {
            background = new backgroundMenu();
            Add(background);

            text = new GameObjectList();
            text.Add(new Names(new Vector2(0, -50), "This game is made by:"));
            text.Add(new Names(new Vector2(50, 0), "Dreymium"));
            text.Add(new Names(new Vector2(50, 50), "Kian"));
            text.Add(new Names(new Vector2(50, 100), "Luuk"));
            text.Add(new Names(new Vector2(50, 150), "Thimo"));
            text.Add(new Names(new Vector2(50, 200), "Unknownymous"));
            Add(text);

            button = new MenuButton("teststart", new Vector2(-900, 100));
            Add(button);
        }
        public override void Update(GameTime gameTime)
        {
            if (playMusic)
            {
                playMusic = false;
                GameEnvironment.AssetManager.PlaySound("clap");
            }

            base.Update(gameTime);
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
