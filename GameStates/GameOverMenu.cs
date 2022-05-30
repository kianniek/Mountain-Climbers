using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameStates
{
 internal class GameOverMenu : GameObjectList
    {

        backgroundMenu background;
        SpriteGameObject gameOver;
        SpriteGameObject menu;
        //SelectSprite select;
        public GameOverMenu()
        {
            background = new backgroundMenu();
            background.Position = new Vector2(0, 0);
            Add(background);

            gameOver = new SpriteGameObject("over");
            gameOver.Position = new Vector2(GameEnvironment.Screen.X / 2, 100);
            Add(gameOver);

            menu = new SpriteGameObject("goToMenu");
            menu.Position = new Vector2(GameEnvironment.Screen.X / 2, 500);
            Add(menu);

            //select = new SelectSprite();
            //select.Position = new Vector2(GameEnvironment.Screen.X/3, 500); 
            //Add(select);


        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (inputHelper.IsKeyDown(ButtonManager.Start))
            {
                GameEnvironment.GameStateManager.SwitchTo("MainMenu");
            }

            /*if (inputHelper.IsKeyDown(Keys.Up))
            {
                select.Position = new Vector2(GameEnvironment.Screen.X / 3, 500);
            } 
            if (inputHelper.IsKeyDown(Keys.Down)){
                select.Position = new Vector2(GameEnvironment.Screen.X / 3, 700);
            }

            if (inputHelper.IsKeyDown(Keys.Space) && select.Position.Y == 500)
            {
                GameEnvironment.GameStateManager.SwitchTo("PlayingState");               
            }
            if (inputHelper.IsKeyDown(Keys.Space) && select.Position.Y == 700)
            {
                GameEnvironment.GameStateManager.SwitchTo("MainMenu");
            }*/
        }
    }
}
