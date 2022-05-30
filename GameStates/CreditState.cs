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
        Title credit;

        bool playMusic = true;
        public CreditState()
        {
            background = new backgroundMenu();
            Add(background);

            /*text = new GameObjectList();
            text.Add(new Names(new Vector2(500, 3000), "This game is made by:"));
            text.Add(new Names(new Vector2(50, 150), "Dreymiun"));
            text.Add(new Names(new Vector2(50, 200), "Kian"));
            text.Add(new Names(new Vector2(50, 250), "Luuk"));
            text.Add(new Names(new Vector2(50, 300), "Thimo"));
            text.Add(new Names(new Vector2(50, 350), "Unknownymous"));
            Add(text);*/

            credit = new Title(new Vector2(300, 50), "madeBy");
            Add(credit);

            button = new MenuButton("backbutton", new Vector2(300, 500));
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

            if (inputHelper.KeyPressed(ButtonManager.Select) || inputHelper.KeyPressed(ButtonManager.Start))
            {
                GameEnvironment.GameStateManager.SwitchTo("MainMenu");
            }
        }
    }
}
