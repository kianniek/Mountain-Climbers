using BaseProject;
using BaseProject.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public class LevelGenerator
{
    public SpriteGameObject[,] tiles;
    public SpriteGameObject ground;
    public Texture2D map;
    public Color[,] colors;
    public float offsetX = 1f;
    public float offsetY = 1f;

    public int sectionLoaded;
    public int sectionStep = 1;
    public int sectionSizeX;
    public int sectionSizeY;


    // Use this for initialization
    public LevelGenerator()
    {
        map = GameEnvironment.AssetManager.Content.Load<Texture2D>("PerTes");
        tiles = new SpriteGameObject[map.Width, map.Height];
        colors = TextureTo2DArray(map);
        Load(1);
    }
    public void Load(int section)
    {
        sectionLoaded = section;
        sectionSizeX = (int) sectionStep * sectionLoaded;
        sectionSizeY = (int) map.Height;

        colors = TextureTo2DArray(map);
        
        for (int x = sectionSizeX - sectionStep; x < sectionSizeX; x++)
        {
            for (int y = 0; y < sectionSizeY; y++)
            {
                if(colors[x, y] == Color.Transparent)
                {
                    continue;
                }
                ground = new Ground();
                float heightOffset = Game1.Screen.Y - map.Height * ground.Height;
                Vector2 posBlock = new Vector2(x * ground.Width, y * ground.Height + heightOffset);

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

