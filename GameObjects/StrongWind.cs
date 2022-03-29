using System;
using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects
{
    public class StrongWind : SpriteGameObject
    {
        public float Height { get; private set; }
        public float Range { get; private set; }
        public WindDirection Direction { get; private set; }

        public StrongWind(Vector2 position, float height, float range, WindDirection direction) : base("wind")
        {
            IWindObject.windAreas.Add(this);
            this.position = position;
            Height = height;
            Range = Math.Abs(range);
            Direction = direction;
        }

        public Vector2 WindForce(SpriteGameObject obj)
        {
            var distance = Math.Abs(obj.Position.X - position.X);
            if (!IsObjectUnderInfluence(obj))
                return Vector2.Zero;

            var force = new Vector2();
            
            switch (Direction)
            {
                case WindDirection.Left:
                    force = new Vector2(-(1 - 1/(Range + Math.Abs(ObjectOffset(obj).X))*distance), 0) * 5;
                    break;
                case WindDirection.Right:
                    force = new Vector2(1 - 1/(Range + Math.Abs(ObjectOffset(obj).X))*distance, 0) * 5;
                    break;
            }
            
            return force;
        }

        public bool IsObjectUnderInfluence(SpriteGameObject obj)
        {
            
            
            switch (Direction)
            {
                case WindDirection.Left:
                    return obj.Position.X > Position.X - Range + ObjectOffset(obj).X;
                case WindDirection.Right:
                    return obj.Position.X < Position.X + Range + ObjectOffset(obj).X;
            }

            return false;
        }

        private Vector2 ObjectOffset(SpriteGameObject obj)
        {
            var offset = new Vector2();
            switch (Direction)
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