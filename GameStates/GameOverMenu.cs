using BaseProject.Engine;
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
        SelectSprite select;
        public GameOverMenu(Camera camera)

        {
            camera.Pos = new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);

            background = new backgroundMenu();
            background.Position = Vector2.Zero;
            Add(background);

            select = new SelectSprite();
            select.Position = new Vector2(GameEnvironment.Screen.X/3, 500); 
            Add(select);


        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (inputHelper.IsKeyDown(Keys.Up))
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
            }



        }
    }
}
