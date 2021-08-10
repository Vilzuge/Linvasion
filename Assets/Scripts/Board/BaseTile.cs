using System;
using UnityEngine;

/*
-------------------------------------------
Base tile that other tile types are extended from
-------------------------------------------
*/
namespace Board
{
    public class BaseTile : MonoBehaviour
    {
        protected Material defaultTile;
        protected Material moveableTile;
        
        public bool walkable;
        public Vector3 worldPosition;
        
        public int gridX;
        public int gridY;
        
        public int gCost;
        public int hCost;
        public BaseTile parent;

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

        public virtual void SetDefaultMaterial()
        {
            
        }
        
    }
}
