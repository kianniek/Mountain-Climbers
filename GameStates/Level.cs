using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseProject.Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BaseProject.GameStates
{
    public abstract class Level : GameObjectList
    {
        private readonly Texture2D levelSprite;
        public Tile[,] Tiles { get; private set; }
        private Color[,] colorData;
        
        private const float tileScale = 1f;
        private const int tileWidth = 32;
        private const int tileHeight = 32;

        protected SmallPlayer smallPlayer;
        protected BigPlayer bigPlayer;
        
        public bool Loaded { get; private set; }

        public Vector2 StartPosition { get; private set; }
        public Vector2 EndPosition { get; private set; }

        private static readonly Dictionary<string, Color> colorCodes = new Dictionary<string, Color>
        {
            {"Ground", Color.Green},
            {"Waterfall", Color.Blue},
            {"Spike", Color.Brown},
            {"Rope", Color.Gold},
            {"Boulder", Color.Black},
            {"ClimbWall", Color.Orange},
            {"Lava", Color.Red},
            {"Platform", Color.Purple},
            {"Start", Color.Aqua},
            {"End", Color.Yellow}
        };

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

        public void LoadLevel()
        {
            if (Loaded)
                return;
            
            GenerateLevel();
            Loaded = true;
        }

        public async void GenerateLevel()
        {
            Console.WriteLine("Started Generating " + DateTime.Now);
            
            Tiles = new Tile[levelSprite.Width,levelSprite.Height];
            colorData = FetchColorData(levelSprite);

            await GenerateRows();

            Console.WriteLine("Finished Generating " + DateTime.Now);
            
            Console.WriteLine("Start position =  " + StartPosition);
            
            Console.WriteLine("End position =  " + EndPosition);
            
            SetupLevel();
        }

        private async Task GenerateRows()
        {
            var rows = new List<Task>();
            for (var x = 0; x < levelSprite.Width; x++)
                rows.Add(GenerateObjects(x));

            await Task.WhenAll(rows);
        }

        private async Task GenerateObjects(int x)
        {
            for (var y = 0; y < levelSprite.Height; y++)
            {
                var obj = GeneratedObject(new Vector2(x, y));

                if (obj != null)
                    Add(obj);
            }
        }

        private GameObject GeneratedObject(Vector2 gridPos)
        {
            var color = colorData[(int)gridPos.X, (int)gridPos.Y];
            var offset = new Vector2(tileWidth, tileHeight)/2;
            
            var objPos = gridPos*  new Vector2(tileWidth, tileHeight) * tileScale;
            objPos += offset;
            
            GameObject obj = null;

            if (color == colorCodes["Start"])
                StartPosition = objPos;
            
            if (color == colorCodes["End"])
                EndPosition = objPos;
            
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

        private bool TileOnLocation(int x, int y)
        {
            if (y < 0 || y >= levelSprite.Height || x < 0 || x >= levelSprite.Width) 
                return false;

            foreach (var tile in environmentalTiles)
                if (colorData[x, y] == tile)
                    return true;

            return false;
        }
        
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