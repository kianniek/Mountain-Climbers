using System;
using System.Collections.Generic;
using System.Text;
using BaseProject.GameObjects;
using BaseProject.GameStates;
using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects
{
    internal class Canon : GameObjectList
    {
        GameObjectList canonBalls;
        CanonBase canonBase;
        CanonBarrel canonBarrel;
        Vector2 barrelOffset = new Vector2(0, 20);
        public Canon(Vector2 position, BigPlayer bigPlayer, SmallPlayer smallPlayer)
        {
            canonBalls = new GameObjectList();
            canonBase = new CanonBase(position, bigPlayer, smallPlayer);
            canonBarrel = new CanonBarrel(position + barrelOffset, bigPlayer, smallPlayer, canonBalls);

            //offset 23
            
            Add(canonBalls);
            Add(canonBarrel);
            Add(canonBase);
        }
    }
}
