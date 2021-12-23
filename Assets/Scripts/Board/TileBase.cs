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
        // Visuals 
        [SerializeField] protected Material defaultTile;
        [SerializeField] protected Material moveableTile;
        [SerializeField] protected Material pathTile;
        [SerializeField] protected Material aimTile;
        
        // Pathfinding variables
        public int gridX;
        public int gridY;
        public int gCost;
        public int hCost;
        public TileBase parent;
        

        public TileState state;
        public bool walkable;
        public Vector3 worldPosition;

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

        public virtual void SetDefaultVisual()
        {
            state = TileState.Default;
            transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material = defaultTile;
        }

        public virtual void SetMovableVisual()
        {
            state = TileState.Movable;
            transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material = moveableTile;
        }

        public virtual void SetPathfindVisual()
        {
            state = TileState.Pathfind;
            transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material = pathTile;
        }

        public virtual void SetShootableVisual()
        {
            state = TileState.Shootable;
            transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material = aimTile;
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
