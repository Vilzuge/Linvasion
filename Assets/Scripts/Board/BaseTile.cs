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
        // Visuals 
        [SerializeField] protected Material defaultTile;
        [SerializeField] protected Material moveableTile;
        [SerializeField] protected Material pathTile;
        [SerializeField] protected Material aimTile;
        [SerializeField] protected MeshRenderer meshRenderer;
        
        // Pathfinding variables
        public int gridX;
        public int gridY;
        public int gCost;
        public int hCost;
        public BaseTile parent;

        public TileState state = TileState.Default;
        public bool walkable;
        public Vector3 worldPosition;

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

        public virtual void SetDefaultVisual()
        {
            state = TileState.Default;
            meshRenderer.material = defaultTile;
        }

        public virtual void SetMovableVisual()
        {
            state = TileState.Movable;
            meshRenderer.material = moveableTile;
        }

        public virtual void SetPathfindVisual()
        {
            state = TileState.Pathfind;
            meshRenderer.material = pathTile;
        }

        public virtual void SetShootableVisual()
        {
            state = TileState.Shootable;
            meshRenderer.material = aimTile;
        }

        public void SetWalkable()
        {
            walkable = true;
        }
        
        public void SetUnWalkable()
        {
            walkable = false;
        }

        public bool IsWalkable()
        {
            return walkable;
        }
    }
}
