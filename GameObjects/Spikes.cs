using BaseProject;
using BaseProject.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class Spikes : SpriteGameObject
{
    //vervangen met player class
    public Spikes() : base("stoneSpike")
    {
        velocity = Vector2.Zero;
        position = new Vector2(Game1.Screen.X / 2, 0);

        //Als je de spikes raakt krijg je knock back. 
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        //if (CollidesWith(player))
        //{
        //    player.Velocity *= new Vector2(-1, MathF.Pow((player.Velocity.Y < 0 ? -1 : 1), 0));
        //    Destroy();
        //}
        ////We willen checken of de player onder de spikes staat.
        ////Check of de x positie van de player kleine is dan een getal in vergelijking met de spike.
        //Console.WriteLine(player.Position.X + "  " + position.X);

        //if (player.Position.X < position.X + 50 && player.Position.X > position.X - 50)
        //{
        //    Velocity += new Vector2(0, 10);
        //}

        //if (CollidesWith(ground))
        //{
        //    Destroy();
        //}

    }

    //public void Destroy()
    //{
    //    position = new Vector2(-100, -100);
    //    velocity = Vector2.Zero;

    //    //Spawn particles
    //}
}