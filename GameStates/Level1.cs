﻿using System;
using BaseProject.Engine;
using BaseProject.GameObjects;
using Microsoft.Xna.Framework;

namespace BaseProject.GameStates
{
    public class Level1 : Level
    {
        Button button;
        ButtonWall wall;
        Checkpoint cp;
        public Level1(string levelSprite, BigPlayer bigPlayer, SmallPlayer smallPlayer) : base(levelSprite, bigPlayer, smallPlayer)
        {
            wall = new ButtonWall(new Vector2(smallPlayer.Position.X + 100, smallPlayer.Position.Y), new Vector2(smallPlayer.Position.X + 100, smallPlayer.Position.Y -100));
            wall.isHorizontal = true;
            button = new Button(smallPlayer, bigPlayer, wall, new Vector2(smallPlayer.Position.X + 50, smallPlayer.Position.Y + 100));
           // cp = new Checkpoint(smallPlayer, bigPlayer, smallPlayer.Position);
        }

        protected override void SetupLevel()
        {
            LevelObjects.Add(button);
            LevelObjects.Add(wall);
            //LevelObjects.Add(cp);
        }
    }
}