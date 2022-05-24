using System;
using BaseProject.Engine;
using BaseProject.GameObjects;

namespace BaseProject.GameStates
{
    public class Level1 : Level
    {
        
        public Level1(string levelSprite, BigPlayer bigPlayer, SmallPlayer smallPlayer) : base(levelSprite, bigPlayer, smallPlayer)
        {

        }

        protected override void SetupLevel()
        {

        }
    }
}