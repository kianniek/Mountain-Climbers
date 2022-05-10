using BaseProject;
using BaseProject.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public class LevelGenerator : SpriteGameObject
{
    public SpriteGameObject[,] tiles;
    public SpriteGameObject ground;
    public Texture2D map;
    public Color[,] colors;
    public float offsetX = 1f;
    public float offsetY = 1f;
    public Vector2 posBlock;

    // Use this for initialization
    public LevelGenerator() : base("")
    {
        map = GameEnvironment.AssetManager.Content.Load<Texture2D>("FirstMapTest");
        tiles = new SpriteGameObject[map.Width, map.Height];
        colors = TextureTo2DArray(map);
        Start();
    }
    public void Start()
    {
        colors = TextureTo2DArray(map);
        for (int x = 0; x < map.Width; x++)
        {
            for (int y = 0; y < map.Height; y++)
            {
                ground = new Ground();
                float heightOffset = Game1.Screen.Y - map.Height * ground.Height;
                posBlock = new Vector2(x * ground.Width, y * ground.Height + heightOffset);

                //De Colors die hier staan coresnsponderen met pixels in de Texture2D van map
                if (colors[x, y] == Color.Red)
                {
                    RoughTerrainTexture(x, y,
                                       "Tile_GrassHorizontal",
                                           "Tile_LeftverticalBlock",
                                           "Tile_RightverticalBlock",
                                           "Tile_Grasstopandbottom",
                                           "Tile_GrassLeftCorner",
                                           "Tile_GrassRightCorner",
                                           "Tile_GrassLeftCornerDown",
                                           "Tile_GrassRightCornerDown",
                                           "Tile_GrassHorizontalDown",
                                           "Tile_Grasstopend",
                                           "Tile_Grassbottomend",
                                           "Tile_Grassrightend",
                                           "Tile_Grassleftend",
                                           "Tile_Grasstopleftknob",
                                           "Tile_Grasstoprightknob",
                                           "Tile_Grassbottomleftknob",
                                           "Tile_Grassbottomrightknob",
                                           "Tile_dirt");
                }
                else if (colors[x, y] == Color.Chocolate) //use this color for smart generation with texture;
                {
                    //Chocolate color (R:210,G:105,B:30,A:255).
                    TerrainTexture(x, y, "Tile_GrassHorizontal", "Tile_dirt", "Tile_dirt", "Tile_GrassLeftCorner", "Tile_GrassRightCorner", "Tile_dirt");
                }
                else if (colors[x, y] == Color.Magenta)
                {
                    tiles[x, y] = new FallingRock("stone100", posBlock);
                }
                //else if (colors[x, y] == Color.Lime)
                //{
                //    tiles[x, y] = new CuttebleRope(this, x, y)
                //    {
                //        Position = posBlock
                //    };
                //}
                else if (colors[x, y] == Color.Yellow)
                {
                    tiles[x, y] = new ClimbWall("Tile_ClimebleLeftverticalBlock", posBlock);
                }
                else if (colors[x, y] == Color.Goldenrod)
                {
                    tiles[x, y] = new Lava(this, x,y) { Position = posBlock };
                }
                else if (colors[x, y] == Color.DarkOrange)
                {
                    tiles[x, y] = new BreakeblePlatform() { Position = posBlock };
                }
            }
        }
    }

    //Deze functie zorgt voor connectieve tiles.
    private void TerrainTexture(int x, int y, string horizontalBlock, string LeftverticalBlock, string RightverticalBlock, string cornerLeft, string cornerRight, string undergroundBlock = "")
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

        if (undergroundBlock != "")
        {
            //add underground blocks
            for (int i = 1; i < map.Height - y; i++)
            {
                float heightOffset = Game1.Screen.Y - map.Height * ground.Height;
                int rgb = 255 - (255 / y * i);
                SpriteGameObject Underground = new UnderGround(assetName: undergroundBlock)
                {
                    Position = new Vector2(x * ground.Width, (y + i) * ground.Height + heightOffset),
                    Shade = new Color(rgb, rgb, rgb)
                };
                tiles[x, y + i] = Underground;
            }
        }
    }
    private void RoughTerrainTexture(int x, int y,
        string horizontalBlock,
        string LeftverticalBlock,
        string RightverticalBlock,
        string GrassTopAndBottom,
        string cornerLeft,
        string cornerRight,
        string cornerLeftDown,
        string cornerRightDown,
        string UndersideBlock,
        string TopLedge,
        string BottomLedge,
        string rightLedge,
        string leftLedge,
        string leftKnob,
        string rightKnob,
        string leftKnobDown,
        string rightKnobDown,
        string undergroundBlock = "")
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
                if (colors[x - 1, y] != Color.Transparent && colors[x + 1, y] != Color.Transparent && colors[x, y + 1] != Color.Transparent && colors[x, y - 1] != Color.Transparent)
                {
                    if (colors[x - 1, y] != Color.Transparent && colors[x, y - 1] != Color.Transparent && colors[x - 1, y - 1] == Color.Transparent)
                    {
                        ground = new Ground(assetName: leftKnob)
                        {
                            Position = posBlock
                        };
                    }
                    else
                    if (colors[x + 1, y] != Color.Transparent && colors[x, y - 1] != Color.Transparent && colors[x + 1, y - 1] == Color.Transparent)
                    {
                        ground = new Ground(assetName: rightKnob)
                        {
                            Position = posBlock
                        };
                    }
                    else
                    if (colors[x - 1, y] != Color.Transparent && colors[x, y + 1] != Color.Transparent && colors[x - 1, y + 1] == Color.Transparent)
                    {
                        ground = new Ground(assetName: leftKnobDown)
                        {
                            Position = posBlock
                        };
                    }
                    else
                    if (colors[x + 1, y] != Color.Transparent && colors[x, y + 1] != Color.Transparent && colors[x + 1, y + 1] == Color.Transparent)
                    {
                        ground = new Ground(assetName: rightKnobDown)
                        {
                            Position = posBlock
                        };
                    }
                    else
                    {
                        ground = new Ground(assetName: undergroundBlock)
                        {
                            Position = posBlock
                        };
                    }
                }
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
                if (colors[x - 1, y] != Color.Transparent && colors[x - 1, y] != Color.Transparent && colors[x, y + 1] == Color.Transparent)
                {
                    ground = new Ground(assetName: UndersideBlock)
                    {
                        Position = posBlock
                    };
                }

                if (colors[x - 1, y] == Color.Transparent && colors[x, y - 1] == Color.Transparent)
                {
                    ground = new Ground(assetName: cornerLeft)
                    {
                        Position = posBlock
                    };
                }
                if (colors[x + 1, y] == Color.Transparent && colors[x, y - 1] == Color.Transparent)
                {
                    ground = new Ground(assetName: cornerRight)
                    {
                        Position = posBlock
                    };
                }
                if (colors[x + 1, y] == Color.Transparent && colors[x, y + 1] == Color.Transparent)
                {
                    ground = new Ground(assetName: cornerRightDown)
                    {
                        Position = posBlock
                    };
                }
                if (colors[x - 1, y] == Color.Transparent && colors[x, y + 1] == Color.Transparent)
                {
                    ground = new Ground(assetName: cornerLeftDown)
                    {
                        Position = posBlock
                    };
                }

                if (colors[x + 1, y] != Color.Transparent && colors[x - 1, y] != Color.Transparent && colors[x, y + 1] == Color.Transparent && colors[x, y - 1] == Color.Transparent)
                {
                    ground = new Ground(assetName: GrassTopAndBottom)
                    {
                        Position = posBlock
                    };
                }

                if (colors[x + 1, y] == Color.Transparent && colors[x, y - 1] == Color.Transparent && colors[x, y + 1] == Color.Transparent)
                {
                    ground = new Ground(assetName: rightLedge)
                    {
                        Position = posBlock
                    };
                }
                if (colors[x - 1, y] == Color.Transparent && colors[x, y - 1] == Color.Transparent && colors[x, y + 1] == Color.Transparent)
                {
                    ground = new Ground(assetName: leftLedge)
                    {
                        Position = posBlock
                    };
                }

                if (colors[x + 1, y] == Color.Transparent && colors[x - 1, y] == Color.Transparent && colors[x, y - 1] == Color.Transparent)
                {
                    ground = new Ground(assetName: TopLedge)
                    {
                        Position = posBlock
                    };
                }
                if (colors[x + 1, y] == Color.Transparent && colors[x - 1, y] == Color.Transparent && colors[x, y + 1] == Color.Transparent)
                {
                    ground = new Ground(assetName: BottomLedge)
                    {
                        Position = posBlock
                    };
                }

            }
            tiles[x, y] = ground;
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

