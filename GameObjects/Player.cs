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
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

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

    //Player is touching the ground
    //Deze methode kun je gebruiken voor elk object dat collision heeft met de player als die op platform staat.
    public override void OnGround(float standPosition)
    {
        base.OnGround(standPosition);
    }

    //Deze kun je gebruiken bij een wall collision aan de linkerkant 
    public override void hitWallLeft(float leftPosition)
    {
        base.hitWallLeft(leftPosition);
    }

    //Deze kun je gebruiken bij een wall collision aan de rechterkant
    public void hitWallRight(float rightPosition)
    {
        base.hitWallRight(rightPosition);
    }
}
