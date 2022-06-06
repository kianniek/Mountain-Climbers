using BaseProject.GameStates;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace BaseProject
{
    public class Chunk
    {
        private Level level;
        public Tile[,] TilesInChunk { get; private set; }
        public Tuple<int, int> ChunkPosition { get; private set; }
        public Vector2 WorldPosition { get; private set; }

        public const int Height = 5;
        public const int Width = 5;


        public Chunk(Level level, Tile[,] tilesInChunk, Tuple<int, int> chunkPosition, Vector2 worldPosition)
        {
            this.level = level;
            TilesInChunk = tilesInChunk;
            ChunkPosition = chunkPosition;
            WorldPosition = worldPosition;
        }

        public List<Chunk> SurroundingChunks()
        {
            var chunks = new List<Chunk>();

            var x = ChunkPosition.Item1;
            var y = ChunkPosition.Item2;

            // Chunks on the left
            if (x > 0 && y > 0)
                chunks.Add(level.Chunks[x - 1, y - 1]);
            if (x > 0)
                chunks.Add(level.Chunks[x - 1, y]);
            if (x > 0 && y < level.Chunks.GetLength(1) - 1)
                chunks.Add(level.Chunks[x - 1, y + 1]);

            // Chunk above and under
            if (y > 0)
                chunks.Add(level.Chunks[x, y - 1]);
            if (y < level.Chunks.GetLength(1) - 1)
                chunks.Add(level.Chunks[x, y + 1]);

            // Chunks on the right
            if (x < level.Chunks.GetLength(0) - 1 && y > 0)
                chunks.Add(level.Chunks[x + 1, y - 1]);
            if (x < level.Chunks.GetLength(0) - 1)
                chunks.Add(level.Chunks[x + 1, y]);
            if (x < level.Chunks.GetLength(0) - 1 && y < level.Chunks.GetLength(1) - 1)
                chunks.Add(level.Chunks[x + 1, y + 1]);

            return chunks;
        }

        public bool InChunk(HeadPlayer player)
        {
            var playerOrigin = player.Origin;

            player.Origin = player.Center;

            var inHeight = player.Position.Y + player.Height / 2f >= WorldPosition.Y - Height / 2f * Level.TileHeight &&
                           player.Position.Y - player.Height / 2f <= WorldPosition.Y + Height / 2f * Level.TileHeight;

            var inWidth = player.Position.X - player.Width / 2f <= WorldPosition.X + Width / 2f * Level.TileWidth &&
                          player.Position.X + player.Width / 2f >= WorldPosition.X - Width / 2f * Level.TileWidth;

            player.Origin = playerOrigin;

            return inHeight && inWidth;
        }
    }
}