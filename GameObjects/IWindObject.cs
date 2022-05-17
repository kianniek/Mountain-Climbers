using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects
{
    public interface IWindObject
    {
        public static readonly List<StrongWind> windAreas = new List<StrongWind>();
        
        protected static bool InWindZone(SpriteGameObject obj, StrongWind wind)
        {
            return wind != null && wind.IsObjectUnderInfluence(obj);
        }

        protected static StrongWind CurrentWind(SpriteGameObject obj)
        {
            foreach (var wind in windAreas)
            {
                if (!InWindZone(obj, wind)) 
                    continue;
                return wind;
            }
            return null;
        }
    }
}