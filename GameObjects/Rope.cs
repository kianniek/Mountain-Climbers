namespace BaseProject.GameObjects
{
    public class Rope : SpriteGameObject
    {
        public Rope(string assetname = "RopeSegment") : base(assetname, layer: -1)
        {
            origin = Center;
            id = Tags.Interactible.ToString();
        }
    }
}
