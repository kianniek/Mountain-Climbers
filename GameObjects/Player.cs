using System;
using BaseProject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//Dion
class SmallPlayer : HeadPlayer
{

    public SmallPlayer() : base("Player")
    {
        origin = new Vector2(Center.X, Center.Y + sprite.Height);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.IsKeyDown(Keys.Left))
        {
            left = true;
            Mirror = true;
        }
        if (inputHelper.IsKeyDown(Keys.Right))
        {
            right = true;
            Mirror = false;
        }

        if (stand)
        {
            if (inputHelper.KeyPressed(Keys.Up))
            {
                stand = false;
                jump = true;
            }
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
}
