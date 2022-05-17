using System;
using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects
{
    public class StrongWind : GameObject
    {
        private readonly float height;
        private readonly float range;
        private readonly WindDirection direction;
        private const float maxWindForce = 150;

        public StrongWind(Vector2 position, float height, float range, WindDirection direction)
        {
            IWindObject.windAreas.Add(this);
            this.position = position;
            this.height = height;
            this.range = Math.Abs(range);
            this.direction = direction;
        }

        public Vector2 WindForce(SpriteGameObject obj, GameTime gameTime)
        {
            var distance = Math.Abs(obj.Position.X - position.X);
            if (!IsObjectUnderInfluence(obj))
                return Vector2.Zero;

            var force = new Vector2();
            
            switch (direction)
            {
                case WindDirection.Left:
                    force = new Vector2(-(1 - 1/(range + Math.Abs(ObjectOffset(obj).X))*distance), 0) * maxWindForce * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case WindDirection.Right:
                    force = new Vector2(1 - 1/(range + Math.Abs(ObjectOffset(obj).X))*distance, 0) * maxWindForce * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
            }
            
            return force;
        }

        public bool IsObjectUnderInfluence(SpriteGameObject obj)
        {
            var offset = ObjectOffset(obj);
            var inHeight = obj.Position.Y >= position.Y - height/2 && obj.Position.Y <= position.Y + height/2;
            
            if (!inHeight)
                return false;
            
            switch (direction)
            {
                case WindDirection.Left:
                    return obj.Position.X <= Position.X && obj.Position.X > Position.X - range + offset.X;
                case WindDirection.Right:
                    return obj.Position.X >= Position.X && obj.Position.X < Position.X + range + offset.X;
            }

            return false;
        }

        private Vector2 ObjectOffset(SpriteGameObject obj)
        {
            var offset = new Vector2();

            if (obj.Position.Y < position.Y)
                offset.Y = obj.Origin.Y - obj.Center.Y + obj.Height / 2f;
            else
                offset.Y = obj.Origin.Y - obj.Center.Y - obj.Height / 2f;
            
            switch (direction)
            {
                case WindDirection.Left:
                    offset.X = obj.Origin.X - obj.Center.X - obj.Width / 2f;
                    break;
                case WindDirection.Right:
                    offset.X = obj.Origin.X - obj.Center.X + obj.Width / 2f;
                    break;
            }

            return offset;
        }
    }
}