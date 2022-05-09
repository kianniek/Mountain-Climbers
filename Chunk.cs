using System;
using System.Collections.Generic;
using BaseProject.GameStates;
using Microsoft.Xna.Framework;

namespace BaseProject
{
    public class Chunk
    {
        private Level level;
        public List<Tile> TilesInChunk { get; private set; }
        public Tuple<int, int> ChunkPosition { get; private set; }
        
        
        public const int Height = 5;
        public const int Width = 5;


        public Chunk(Level level, List<Tile> tilesInChunk, Tuple<int, int> chunkPosition)
        {
            this.level = level;
            TilesInChunk = tilesInChunk;
            ChunkPosition = chunkPosition;
        }

        public Chunk[] SurroundingTiles()
        {
            return null;
        }

        public bool InChunk(HeadPlayer player)
        {
            /*if (player.Position )
            {
                
            }*/
            
            return false;
        }
        
    }
}