using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameObjects
{
    public class Rope : SpriteGameObject
    {
        public Rope(string assetname = "RopeSegment") : base(assetname, layer  : -1)
        {
            origin = Center;
            id = Tags.Interactible.ToString();
        }
    }
}
