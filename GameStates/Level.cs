using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseProject.Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using BaseProject.GameObjects;

namespace BaseProject.GameStates
{
    public abstract class Level : GameObjectList
    {
        private readonly Texture2D levelSprite;
        public Tile[,] Tiles { get; private set; }
        private Color[,] colorData;
        public GameObjectList LevelObjects { get; private set; } = new GameObjectList();
        
        private const float tileScale = 1f;
        public const int TileWidth = 32;
        public const int TileHeight = 32;

        protected SmallPlayer smallPlayer;
        protected BigPlayer bigPlayer;
        
        public bool Loaded { get; private set; }

        public Vector2 StartPosition { get; private set; }
        public Vector2 EndPosition { get; private set; }

        // Color codes for all the objects
        private static readonly Dictionary<string, Color> colorCodes = new Dictionary<string, Color>
        {
            {"Ground", Color.Green},
            {"Waterfall", Color.Blue},
            {"Spike", Color.Brown},
            {"Rope", Color.Honeydew},
            {"Boulder", Color.Gainsboro},
            {"ClimbWall", Color.Orange},
            {"Lava", Color.Red},
            {"Platform", Color.Purple},
            {"Start", Color.Aqua},
            {"End", Color.Yellow}
        };

        // All color codes that represent level tiles
        private static readonly Color[] environmentalTiles =
        {
            colorCodes["Ground"],
            colorCodes["Lava"],
        };
        
        
        public Level(string levelSprite, BigPlayer bigPlayer, SmallPlayer smallPlayer)
        {
            this.levelSprite = GameEnvironment.AssetManager.Content.Load<Texture2D>(levelSprite);
            this.bigPlayer = bigPlayer;
            this.smallPlayer = smallPlayer;
        }

        // Load the Level
        public void LoadLevel()
        {
            if (Loaded)
                return;
            
            GenerateLevel();
            Loaded = true;
        }

        // Generate the level
        private async void GenerateLevel()
        {
            Tiles = new Tile[levelSprite.Width,levelSprite.Height];
            colorData = FetchColorData(levelSprite);

            await GenerateRows();

            SetupLevel();
        }

        // Split the source image to generate the level faster
        private async Task GenerateRows()
        {
            var rows = new List<Task>();
            for (var x = 0; x < levelSprite.Width; x++)
                rows.Add(GenerateObjects(x));

            await Task.WhenAll(rows);
        }

        // Generate all the objects of row X
        private async Task GenerateObjects(int x)
        {
            for (var y = 0; y < levelSprite.Height; y++)
            {
                var obj = GeneratedObject(new Vector2(x, y));

                if (obj != null)
                    Add(obj);
            }
            Add(LevelObjects);
        }

        // Generate the correct object according to the color code
        private GameObject GeneratedObject(Vector2 gridPos)
        {
            var color = colorData[(int)gridPos.X, (int)gridPos.Y];
            var offset = new Vector2(TileWidth, TileHeight)/2;
            
            var objPos = gridPos * new Vector2(TileWidth, TileHeight) * tileScale;
            objPos += offset;
            
            GameObject obj = null;

            if (color == colorCodes["Start"])
                StartPosition = objPos;
            
            if (color == colorCodes["End"])
                EndPosition = objPos;

            if (color == colorCodes["Rope"])
                LevelObjects.Add(new CuttebleRope(this, (int)objPos.X, (int)objPos.Y));

            if (color == colorCodes["Lava"])
                LevelObjects.Add(new Lava(this, (int)objPos.X, (int)objPos.Y));

            if (environmentalTiles.Any(c => c == color))
            {
                var sprite = FetchTileSprite(color, (int)gridPos.X, (int)gridPos.Y);

                if (sprite == string.Empty)
                    return null;

                var t = new Tile(sprite, objPos, tileScale);
                
                Tiles[(int)gridPos.X, (int)gridPos.Y] = t;

                obj = t;
            }

            return obj;
        }

        // Fetch the correct sprite for the tile
        private string FetchTileSprite(Color tileColor, int x, int y)
        {
            if (tileColor == Color.Transparent)
                return string.Empty;

            if (tileColor == colorCodes["Ground"])
            {
                if (TileOnLocation(x - 1, y) && TileOnLocation(x + 1, y) && TileOnLocation(x, y - 1) && TileOnLocation(x, y + 1)) // Surrounded
                    return "Tile_dirt";
                if (TileOnLocation(x - 1, y) && TileOnLocation(x + 1, y) && TileOnLocation(x, y - 1)) // No tile under
                    return "Tile_GrassHorizontalDown";
                if (TileOnLocation(x - 1, y) && TileOnLocation(x + 1, y) && TileOnLocation(x, y + 1)) // No tile above
                    return "Tile_GrassHorizontal";
                if (TileOnLocation(x + 1, y) && TileOnLocation(x, y + 1) && TileOnLocation(x, y - 1)) // No tile left
                    return "Tile_LeftverticalBlock";
                if (TileOnLocation(x - 1, y) && TileOnLocation(x, y + 1) && TileOnLocation(x, y - 1)) // No tile right
                    return "Tile_RightverticalBlock";
            
            
                if (TileOnLocation(x + 1, y) && TileOnLocation(x, y + 1)) // No tile left and above
                    return "Tile_GrassLeftCorner";
                if (TileOnLocation(x + 1, y) && TileOnLocation(x, y - 1)) // No tile left and under
                    return "Tile_GrassLeftCornerDown";
                if (TileOnLocation(x - 1, y) && TileOnLocation(x, y + 1)) // No tile right and above
                    return "Tile_GrassRightCorner";
                if (TileOnLocation(x - 1, y) && TileOnLocation(x, y - 1)) // No tile right and under
                    return "Tile_GrassRightCornerDown";
            }

            return string.Empty;
        }

        // Returns if there is a tile on the given position
        public bool TileOnLocation(int x, int y)
        {
            if (y < 0 || y >= levelSprite.Height || x < 0 || x >= levelSprite.Width) 
                return false;

            foreach (var tile in environmentalTiles)
                if (colorData[x, y] == tile)
                    return true;

            return false;
        }
        
        // Convert the image to an 2D color array
        private Color[,] FetchColorData(Texture2D texture)
        {
            var colors = new Color[levelSprite.Width * levelSprite.Height];
            texture.GetData(colors);
            var colors2D = new Color[texture.Width, texture.Height];
            
            for (var x = 0; x < texture.Width; x++)
                for (var y = 0; y < texture.Height; y++)
                    colors2D[x, y] = colors[x + y * texture.Width];
            
            return colors2D;
        }

        protected abstract void SetupLevel();
    }
}