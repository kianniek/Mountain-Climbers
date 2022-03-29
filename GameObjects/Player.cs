using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//Dion
	public class SmallPlayer : SpriteGameObject
	{
	float gravity;
    bool left, right, jump, stand;
		public SmallPlayer() : base("Player")
		{
		position.Y = 300;
		velocity.Y = 20;
		velocity.X = 0;
        gravity = 10f;
		}

    public override void Update(GameTime gameTime)
    {
        velocity.X = 0;

        if (jump)
        {
            jump = false;
            stand = false;
            velocity.Y = -470;
        }

        if (left)
        {
            velocity.X = -175;
            left = false;
        }
        if (right)
        {
            velocity.X = 175;
            right = false;
        }

        //Console.WriteLine(position.Y);
        
        base.Update(gameTime);
        velocity.Y += gravity;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

		if (inputHelper.IsKeyDown(Keys.Left))
        {
            left = true;
        }
        if (inputHelper.IsKeyDown(Keys.Right))
        {
            right = true;
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
    
    //Player is touches the ground
    //Deze methode kun je gebruiken voor elk object dat collision heeft met de player als die op platform staat.
    public void OnGround(float standPosition)
    {
        if (position.Y >= standPosition)
        {
            velocity.Y = 0;
            stand = true;
            position.Y = standPosition;
        }
    }

    //Deze kun je gebruiken bij een wall collision aan de linkerkant 
    public void hitWallLeft(float leftPosition)
    {
        if (position.X <= leftPosition)
        {
            position.X = leftPosition;
        }
    }

    //Deze kun je gebruiken bij een wall collision aan de rechterkant
    public void hitWallRight(float rightPosition)
    {
        if (position.X >= rightPosition)
        {
            position.X = rightPosition;
        }
    }
}
