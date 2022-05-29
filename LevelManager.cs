using System;
using System.Collections.Generic;
using BaseProject.Engine;
using BaseProject.GameObjects;
using BaseProject.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BaseProject
{
    public class LevelManager : GameObject
    {
        public static List<Level> Levels { get; } = new List<Level>();
        private static int currentLevelIndex = 0;

        private static SmallPlayer smallPlayer;
        private static BigPlayer bigPlayer;

        private static Level PreviousLevel()
        {
            var index = currentLevelIndex - 1;
            if (index < 0)
                return null;
            return Levels[index];
        }

        public static Level CurrentLevel() => Levels[currentLevelIndex];
        
        private static Level NextLevel()
        {
            var index = currentLevelIndex + 1;
            if (index >= Levels.Count)
                return null;
            return Levels[index];
        }

        public LevelManager(BigPlayer bigPlayer, SmallPlayer smallPlayer)
        {
            LevelManager.bigPlayer = bigPlayer;
            LevelManager.smallPlayer = smallPlayer;
            
            Levels.Add(new Level1("Levels/level1test", LevelManager.bigPlayer, LevelManager.smallPlayer));
            Levels.Add(new Level2("Levels/level2test", LevelManager.bigPlayer, LevelManager.smallPlayer));
            Levels.Add(new Level3("Levels/level3test", LevelManager.bigPlayer, LevelManager.smallPlayer));
            Levels.Add(new Level4("Levels/level4test", LevelManager.bigPlayer, LevelManager.smallPlayer));
            Levels.Add(new Level5("Levels/level5test", LevelManager.bigPlayer, LevelManager.smallPlayer));
            Levels.Add(new Level6("Levels/level6", LevelManager.bigPlayer, LevelManager.smallPlayer));
            Levels.Add(new Level7("Levels/Level7", LevelManager.bigPlayer, LevelManager.smallPlayer));
            GoToLevel(0);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Levels[currentLevelIndex]?.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            Levels[currentLevelIndex]?.Draw(gameTime, spriteBatch);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (inputHelper.KeyPressed(Keys.N))
                GoToNextLevel();
        }

        public static void GoToNextLevel()
        {
            if (NextLevel() == null)
                return;
            GoToLevel(currentLevelIndex + 1);
            
        }

        public static void GoToPreviousLevel()
        {
            if (PreviousLevel() == null)
                return;
            GoToLevel(currentLevelIndex - 1);
        }

        public static void GoToLevel(int index)
        {
            if (index < 0 || index >= Levels.Count)
                return;

            int tempIndex = currentLevelIndex;
            currentLevelIndex = index;
            
            PreviousLevel()?.LoadLevel();
            Levels[currentLevelIndex]?.LoadLevel();
            NextLevel()?.LoadLevel();
            Vector2 pos = currentLevelIndex >= tempIndex ? LevelManager.CurrentLevel().StartPosition : LevelManager.CurrentLevel().EndPosition;
            bigPlayer.GoToNewLevel(LevelManager.CurrentLevel(), pos);
            smallPlayer.GoToNewLevel(LevelManager.CurrentLevel(), pos);

            smallPlayer.state.cam.Pos = pos;
        }
    }
}