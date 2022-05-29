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

        public Chunk[,] Chunks { get; private set; }

        private readonly SmallPlayer smallPlayer;
        private readonly BigPlayer bigPlayer;
        
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
            {"BreakablePlatform", Color.Purple},
            {"Start", Color.Aqua},
            {"End", Color.Yellow}
            
        };

        // All color codes that represent level tiles
        private static readonly Color[] environmentalTiles =
        {
            colorCodes["Ground"],
            colorCodes["Lava"],
            colorCodes["BreakablePlatform"]
        };


        protected Level(string levelSprite, BigPlayer bigPlayer, SmallPlayer smallPlayer)
        {
            this.levelSprite = GameEnvironment.AssetManager.Content.Load<Texture2D>(levelSprite);
            this.bigPlayer = bigPlayer;
            this.smallPlayer = smallPlayer;
            Add(LevelObjects);
        }
        
        private async void GenerateLevel()
        {
            colorData = FetchColorData(levelSprite);
            Chunks = ChunksInLevel();

            await GenerateChunks(Chunks);
            
            SetupLevel();
        }
        
        protected abstract void SetupLevel();

        // Load the Level
        public void LoadLevel()
        {
            if (Loaded)
                return;
            
            GenerateLevel();
            Loaded = true;
        }

        private async Task GenerateChunks(Chunk[,] t)
        {
            var a = t.GetLength(0);
            var b = t.GetLength(1);

            for (var y = 0; y < b; y++)
            {
                for (var x = 0; x < a; x++)
                {
                    GenerateChunk(new Tuple<int, int>(x, y),
                        new Tuple<int, int>(x * Chunk.Width, y * Chunk.Height),
                        new Tuple<int, int>(x * Chunk.Width + Chunk.Width, y * Chunk.Height + Chunk.Height));
                }
            }
        }

        private void GenerateChunk(Tuple<int, int> chunkPos, Tuple<int,int> start, Tuple<int,int> end)
        {
            var (startX, startY) = start;
            var (endX, endY) = end;
            
            var chunkWorldX = (startX + (endX - (startX + 1))/2f) * TileWidth;
            var chunkWorldY = (startY + (endY - (startY + 1))/2f) * TileHeight;

            var tiles = new Tile[Chunk.Width, Chunk.Height];

            for (var y = 0; y < endY - startY; y++)
            {
                for (var x = 0; x < endX - startX; x++)
                {
                    
                    var obj = GeneratedObject(new Vector2(startX + x, startY + y));
                    
                    if (obj == null)
                        continue;

                    if (obj.GetType() == typeof(Tile) || obj.GetType().IsSubclassOf(typeof(Tile)))
                        tiles[x,y] = (Tile)obj;
                    Add(obj);
                }
            }
            
            Chunks[chunkPos.Item1, chunkPos.Item2] = new Chunk(this, tiles, chunkPos, new Vector2(chunkWorldX, chunkWorldY));
        }

        private Chunk[,] ChunksInLevel()
        {
            float decimalChunksOnX = (float)levelSprite.Width / Chunk.Width;
            int chunksOnX = decimalChunksOnX - (int)decimalChunksOnX > 0f ? (int)decimalChunksOnX + 1 : (int)decimalChunksOnX;
            
            float decimalChunksOnY = (float)levelSprite.Height / Chunk.Height;
            int chunksOnY = decimalChunksOnY - (int)decimalChunksOnY > 0f ? (int)decimalChunksOnY + 1 : (int)decimalChunksOnY;

            var chunkArray = new Chunk[chunksOnX, chunksOnY];
            
            return chunkArray;
        }        

        // Generate the correct object according to the color code
        private GameObject GeneratedObject(Vector2 gridPos)
        {
            if (gridPos.X >= colorData.GetLength(0) || gridPos.Y >= colorData.GetLength(1))
                return null;
            
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

            if (color == colorCodes["Waterfall"])
                LevelObjects.Add(new Waterfall(new Vector2((int)objPos.X, (int)objPos.Y)));

            if (environmentalTiles.Any(c => c == color))
            {
                var sprite = FetchTileSprite(color, (int)gridPos.X, (int)gridPos.Y);

                if (sprite == string.Empty)
                    return null;

                Tile t;
                
                if (color == colorCodes["BreakablePlatform"])
                    t = new BreakablePlatform(objPos, tileScale, smallPlayer, bigPlayer);
                else
                    t = new Tile(sprite, objPos, tileScale, smallPlayer, bigPlayer);

                obj = t;
            }

            return obj;
        }

        // Fetch the correct sprite for the tile
        private string FetchTileSprite(Color tileColor, int x, int y)
        {
            if (tileColor == Color.Transparent)
                return string.Empty;

            if (tileColor == colorCodes["BreakablePlatform"])
                return "Tile_ClimebleLeftverticalBlock";

            if (tileColor == colorCodes["Ground"])
            {
                if (TileOnLocation(x - 1, y) && TileOnLocation(x + 1, y) && TileOnLocation(x, y - 1) &&
                    TileOnLocation(x, y + 1) && !TileOnLocation(x - 1, y - 1))
                    return "Tile_Grasstopleftknob";
                if (TileOnLocation(x - 1, y) && TileOnLocation(x + 1, y) && TileOnLocation(x, y - 1) &&
                    TileOnLocation(x, y + 1) && !TileOnLocation(x - 1, y + 1))
                    return "Tile_Grassbottomleftknob";
                if (TileOnLocation(x - 1, y) && TileOnLocation(x + 1, y) && TileOnLocation(x, y - 1) &&
                    TileOnLocation(x, y + 1) && !TileOnLocation(x + 1, y - 1))
                    return "Tile_Grasstoprightknob";
                if (TileOnLocation(x - 1, y) && TileOnLocation(x + 1, y) && TileOnLocation(x, y - 1) &&
                    TileOnLocation(x, y + 1) && !TileOnLocation(x + 1, y + 1))
                    return "Tile_Grassbottomrightknob";
                
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
                
                
                
                // grass bars
                if (TileOnLocation(x - 1, y) && TileOnLocation(x + 1, y) && !TileOnLocation(x, y - 1) && !TileOnLocation(x, y + 1))
                    return "Tile_Grasstopandbottom";
                if (!TileOnLocation(x - 1, y) && !TileOnLocation(x + 1, y) && TileOnLocation(x, y - 1) && TileOnLocation(x, y + 1))
                    return "Tile_Grassleftandright";
                
                if (!TileOnLocation(x - 1, y) && TileOnLocation(x + 1, y) && !TileOnLocation(x, y - 1) && !TileOnLocation(x, y + 1)) // Left grass end
                    return "Tile_Grassleftend";
                if (TileOnLocation(x - 1, y) && !TileOnLocation(x + 1, y) && !TileOnLocation(x, y - 1) && !TileOnLocation(x, y + 1)) // Right grass end
                    return "Tile_Grassrightend";
                if (!TileOnLocation(x - 1, y) && !TileOnLocation(x + 1, y) && !TileOnLocation(x, y - 1) && TileOnLocation(x, y + 1)) // Top grass end
                    return "Tile_Grasstopend";
                if (!TileOnLocation(x - 1, y) && !TileOnLocation(x + 1, y) && TileOnLocation(x, y - 1) && !TileOnLocation(x, y + 1)) // Bottom grass end
                    return "Tile_Grassbottomend";
                
                
                if (!TileOnLocation(x - 1, y) && !TileOnLocation(x + 1, y) && !TileOnLocation(x, y - 1) && !TileOnLocation(x, y + 1))
                    return "Tile_Grassgrass";
                
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

        public Chunk[] ActiveChunks()
        {
            var chunks = new List<Chunk>();
            
            if (!Loaded)
                return Array.Empty<Chunk>();
            
            foreach (var chunk in Chunks)
            {
                if (!chunk.InChunk(bigPlayer) && !chunk.InChunk(smallPlayer))
                    continue;
                
                chunks.Add(chunk);
                chunks.AddRange(chunk.SurroundingChunks());
            }
            
            return chunks.ToArray();
        }
    }
}