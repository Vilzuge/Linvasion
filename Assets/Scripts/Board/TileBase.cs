using System;
using UnityEngine;

/*
-------------------------------------------
Base tile that other tile types are extended from
-------------------------------------------
*/

namespace Board
{
    public class TileBase : MonoBehaviour
    {
        protected Material defaultTile;
        protected Material moveableTile;
        protected Material pathTile;
        protected Material aimTile;

        public TileState state;
        public bool walkable;
        public Vector3 worldPosition;
        
        public int gridX;
        public int gridY;
        
        public int gCost;
        public int hCost;
        public TileBase parent;

        public virtual void Start()
        {
            state = TileState.Default;
        }

        public int fCost
        {
            get
            {
                return gCost + hCost;
            }
        }

        public virtual void SetDefault()
        {
            state = TileState.Default;
        }

        public virtual void SetMovable()
        {
            state = TileState.Movable;
        }

        public virtual void SetPathfind()
        {
            state = TileState.Pathfind;
        }

        public virtual void SetShootable()
        {
            state = TileState.Shootable;
        }
    }
}
