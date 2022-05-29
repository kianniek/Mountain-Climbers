using Microsoft.Xna.Framework;
using System;
using BaseProject.GameStates;

namespace BaseProject.GameObjects
{
    public class BreakablePlatform : Tile
    {

        private const float secondsBeforeBreak = 1f;
        private const float secondsUntilRespawn = 5f;
        
        private readonly SpriteSheet[] sprites =
        {
            new SpriteSheet("Tile_platform1"),
            new SpriteSheet("Tile_platform2"),
            new SpriteSheet("Tile_platform3"),
            new SpriteSheet("Tile_platform4"),
            new SpriteSheet("Tile_platform5")
        };
        
        private readonly float secondsBetweenSprites;
        
        private DateTime breakStartTime;
        private DateTime breakTime;
        private bool breaking = false;

        public BreakablePlatform(Vector2 position, float scale, SmallPlayer smallPlayer, BigPlayer bigPlayer) : base("Tile_platform1", position, scale, smallPlayer, bigPlayer)
        {
            secondsBetweenSprites = secondsBeforeBreak / sprites.Length;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if ((PlayerOnPlatform(smallPlayer) || PlayerOnPlatform(bigPlayer)) && !breaking)
            {
                breakStartTime = DateTime.Now;
                breaking = true;
            }

            if (breaking)
                Break();

            if (!Visible)
            {
                TimeSpan timeBetweenNowAndBreak = DateTime.Now - breakTime;
                var secondsPassed = timeBetweenNowAndBreak.Seconds + timeBetweenNowAndBreak.Milliseconds / 1000f;

                if (secondsPassed > secondsUntilRespawn & !(PlayerOnPlatform(smallPlayer) && !PlayerOnPlatform(bigPlayer)))
                {
                    Visible = true;
                    sprite = sprites[0];
                }
            }
        }

        private void Break()
        {
            TimeSpan timeBetweenNowAndStart = DateTime.Now - breakStartTime;
            var secondsPassed = timeBetweenNowAndStart.Seconds + timeBetweenNowAndStart.Milliseconds / 1000f;
                
            for (var i = 0; i < sprites.Length; i++)
            {
                if (!(secondsPassed / secondsBetweenSprites < i)) 
                    continue;
                sprite = sprites[i];
                break;
            }
                
            if (secondsPassed < secondsBeforeBreak)
                return;
            breakTime = DateTime.Now;
                
                
            Console.WriteLine($"{breakStartTime}    Break    {DateTime.Now}");
            breaking = false;
            visible = false;
        }

        private bool PlayerOnPlatform(HeadPlayer player)
        {
            var playerTop = player.Position.Y - Height/2f;
            var playerBottom = player.Position.Y + Height/2f;
            var playerLeft = player.Position.X - Width/2f;
            var playerRight = player.Position.X + Width/2f;
            
            return playerTop <= position.Y + Height/2f && playerBottom >= position.Y - Height/2f - player.Height && playerLeft <= position.X + Width/2f && playerRight >= position.X - Width/2f;
        }
    }
}
