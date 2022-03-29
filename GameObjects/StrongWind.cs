using System;
using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects
{
    public class StrongWind : GameObject
    {
        private readonly float height;
        private readonly float range;
        private readonly WindDirection direction;

        public StrongWind(Vector2 position, float height, float range, WindDirection direction)
        {
            IWindObject.windAreas.Add(this);
            this.position = position;
            this.height = height;
            this.range = Math.Abs(range);
            this.direction = direction;
        }

        public Vector2 WindForce(SpriteGameObject obj)
        {
            var distance = Math.Abs(obj.Position.X - position.X);
            if (!IsObjectUnderInfluence(obj))
                return Vector2.Zero;

            var force = new Vector2();
            
            switch (direction)
            {
                case WindDirection.Left:
                    force = new Vector2(-(1 - 1/(range + Math.Abs(ObjectOffset(obj).X))*distance), 0) * 5;
                    break;
                case WindDirection.Right:
                    force = new Vector2(1 - 1/(range + Math.Abs(ObjectOffset(obj).X))*distance, 0) * 5;
                    break;
            }
            
            return force;
        }

        public bool IsObjectUnderInfluence(SpriteGameObject obj)
        {
            var inHeight = obj.Position.Y > position.Y - height / 2 && obj.Position.Y < position.Y + height / 2;
            switch (direction)
            {
                case WindDirection.Left:
                    var inRange = obj.Position.X > Position.X - range + ObjectOffset(obj).X;
                    return obj.Position.X > Position.X - range + ObjectOffset(obj).X;
                case WindDirection.Right:
                    return obj.Position.X < Position.X + range + ObjectOffset(obj).X;
            }

            return false;
        }

        private Vector2 ObjectOffset(SpriteGameObject obj)
        {
            var offset = new Vector2();
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