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
        public List<Level> Levels { get; } = new List<Level>();
        private int currentLevelIndex = 0;

        private SmallPlayer smallPlayer;
        private BigPlayer bigPlayer;

        private Level PreviousLevel()
        {
            var index = currentLevelIndex - 1;
            if (index < 0)
                return null;
            return Levels[index];
        }

        public Level CurrentLevel() => Levels[currentLevelIndex];
        
        private Level NextLevel()
        {
            var index = currentLevelIndex + 1;
            if (index >= Levels.Count)
                return null;
            return Levels[index];
        }

        public LevelManager(BigPlayer bigPlayer, SmallPlayer smallPlayer)
        {
            this.bigPlayer = bigPlayer;
            this.smallPlayer = smallPlayer;
            
            Levels.Add(new Level1("level1", this.bigPlayer, this.smallPlayer));
            Levels.Add(new Level2("level2", this.bigPlayer, this.smallPlayer));
            Levels.Add(new Level3("level3", this.bigPlayer, this.smallPlayer));
            Levels.Add(new Level4("level4", this.bigPlayer, this.smallPlayer));
            Levels.Add(new Level5("level5", this.bigPlayer, this.smallPlayer));
            Levels.Add(new Level6("level6", this.bigPlayer, this.smallPlayer));
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

        public void GoToNextLevel()
        {
            if (NextLevel() == null)
                return;
            GoToLevel(currentLevelIndex + 1);
            
        }

        public void GoToPreviousLevel()
        {
            if (PreviousLevel() == null)
                return;
            GoToLevel(currentLevelIndex - 1);
        }

        public void GoToLevel(int index)
        {
            if (index < 0 || index >= Levels.Count)
                return;
            
            currentLevelIndex = index;
            
            PreviousLevel()?.LoadLevel();
            Levels[currentLevelIndex]?.LoadLevel();
            NextLevel()?.LoadLevel();
            bigPlayer.GoToNewLevel(CurrentLevel(), CurrentLevel().StartPosition);
            smallPlayer.GoToNewLevel(CurrentLevel(), CurrentLevel().StartPosition);
        }
    }
}