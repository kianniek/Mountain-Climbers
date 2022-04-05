using BaseProject;
using BaseProject.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public class LevelGenerator : GameObject
{
    public SpriteGameObject[,] tiles;
    public SpriteGameObject ground;
    public Texture2D map;
    public Color[,] colors;
    public float offsetX = 1f;
    public float offsetY = 1f;


    // Use this for initialization
    public LevelGenerator()
    {
        map = GameEnvironment.AssetManager.Content.Load<Texture2D>("LevelLayout");
        tiles = new SpriteGameObject[map.Width, map.Height];
        Start();
    }
    public void Start()
    {
        colors = TextureTo2DArray(map);
        for (int x = 0; x < map.Width; x++)
        {
            for (int y = 0; y < map.Height; y++)
            {
                //De Colors die hier staan coresnsponderen met pixels in de Texture2D van map
                if (colors[x, y] == Color.Red)
                {
                    //Om nieuwe objects toe te voegen volg de volgende template
                    /*
                    SpriteGameObject ground = new Ground();
                    ground.Position = new Vector2(x * ground.Width, y * ground.Height);
                    tiles[x, y] = ground;
                    */
                }
                else if (colors[x, y] == Color.Yellow)
                {
                }
                else if (colors[x, y] == Color.Lime)
                {
                }
                else if (colors[x, y] == Color.Aqua)
                {
                }
                else if (colors[x, y] == Color.Blue)
                {
                }
                else if (colors[x, y] == Color.Chocolate) //use this color for smart generation with texture;
                {
                    //Chocolate color (R:210,G:105,B:30,A:255).
                    TerrainTexture(x, y, "Tile_GrassHorizontal", "Tile_GrassHorizontal", "Tile_GrassRightCorner", "Tile_GrassHorizontal", "Tile_GrassRightCorner", "Tile_dirt");
                }
                else if (colors[x, y] == Color.Magenta)
                {

                }
            }
        }
    }

    //Deze functie zorgt voor connectieve tiles.
    private void TerrainTexture(int x, int y, string horizontalBlock, string LeftverticalBlock, string RightverticalBlock, string cornerLeft, string cornerRight, string undergroundBlock)
    {
        bool arrayOutOfBound = y < 0 || y >= map.Height || x < 0 || x >= map.Width;

        if (!arrayOutOfBound)
        {
            ground = new Ground(assetName: horizontalBlock);
            float heightOffset = Game1.Screen.Y - map.Height * ground.Height;
            Vector2 posBlock = new Vector2(x * ground.Width, y * ground.Height + heightOffset);
            ground.Position = posBlock;
            if (x != 0)
            {

                if (colors[x - 1, y] == Color.Transparent)
                {
                    ground = new Ground(assetName: LeftverticalBlock)
                    {
                        Position = posBlock
                    };
                }
                if (colors[x + 1, y] == Color.Transparent)
                {
                    ground = new Ground(assetName: RightverticalBlock)
                    {
                        Position = posBlock
                    };
                }
                if (colors[x - 1, y] == Color.Transparent && colors[x, y - 1] == Color.Transparent)
                {
                    if (colors[x - 1, y - 1] == Color.Transparent)
                    {
                        ground = new Ground(assetName: cornerLeft)
                        {
                            Position = posBlock
                        };
                    }
                    else
                    {
                        ground = new Ground(assetName: horizontalBlock)
                        {
                            Position = posBlock
                        };
                    }
                }
                if (colors[x + 1, y] == Color.Transparent && colors[x, y - 1] == Color.Transparent)
                {
                    if (colors[x + 1, y - 1] == Color.Transparent)
                    {
                        ground = new Ground(assetName: cornerRight)
                        {
                            Position = posBlock
                        };
                    }
                    else
                    {
                        ground = new Ground(assetName: horizontalBlock)
                        {
                            Position = posBlock
                        };
                    }
                }
            }
            tiles[x, y] = ground;
        }


        //add underground blocks
        for (int i = 1; i < map.Height - y; i++)
        {
            float heightOffset = Game1.Screen.Y - map.Height * ground.Height;
            int rgb = 255 - (255 / y * i);
            SpriteGameObject Underground = new UnderGround(assetName: undergroundBlock)
            {
                Position = new Vector2(x * ground.Width, (y+i) * ground.Height + heightOffset),
                Shade = new Color(rgb, rgb, rgb)
            };
            tiles[x, y + i] = Underground;
        }
    }

    private long ToColint(int x, int y, Color[,] colors) // Color + int = Colint
    {
        //string a = colors[x, y].A.ToString();
        string r = colors[x, y].R.ToString();
        string g = colors[x, y].G.ToString();
        string b = colors[x, y].B.ToString();
        string rgb = "1" + r + "1" + g + "1" + b;
        long Intrgb = long.Parse(rgb);
        return Intrgb;
    }

    private Color[,] TextureTo2DArray(Texture2D texture)
    {
        Color[] colors1D = new Color[texture.Width * texture.Height];
        texture.GetData(colors1D);
        Color[,] colors2D = new Color[texture.Width, texture.Height];
        for (int x = 0; x < texture.Width; x++)
        {
            for (int y = 0; y < texture.Height; y++)
            {
                colors2D[x, y] = colors1D[x + y * texture.Width];
            }
        }
        return colors2D;
    }

}

