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
        
        public bool walkable;
        public Vector3 worldPosition;
        
        public int gridX;
        public int gridY;
        
        public int gCost;
        public int hCost;
        public TileBase parent;

        public virtual void Start()
        {
            
        }

        public int fCost
        {
            get
            {
                return gCost + hCost;
            }
        }

        public virtual void SetDefaultMaterial() { }
        
        public virtual void SetMovableMaterial() { }
        
        public virtual void SetPathMaterial() { }
        
        public virtual void SetShootableMaterial() { }
        
        
    }
}
