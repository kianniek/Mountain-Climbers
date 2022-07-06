using System;
using System.Collections.Generic;
using System.Text;
using BaseProject.GameObjects;
using BaseProject.GameStates;
using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects
{
    internal class Cannon : GameObjectList
    {
        GameObjectList cannonBalls;
        CannonBase cannonBase;
        CannonBarrel cannonBarrel;
        Vector2 barrelOffset = new Vector2(0, 20);
        public Cannon(Vector2 position, BigPlayer bigPlayer, SmallPlayer smallPlayer)
        {
            cannonBalls = new GameObjectList();
            cannonBase = new CannonBase(position, bigPlayer, smallPlayer);
            cannonBarrel = new CannonBarrel(position + barrelOffset, bigPlayer, smallPlayer, canonBalls);

            //offset 23
            
            Add(cannonBalls);
            Add(cannonBarrel);
            Add(cannonBase);
        }
    }
}
