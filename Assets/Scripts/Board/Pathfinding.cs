using System.Collections.Generic;
using UnityEngine;

/*
-------------------------------------------
This script handles A* pathfinding, but without diagonals, which is determined 
at Board.cs "GetNeighbours" function that returns the tiles around a specific tile.
-------------------------------------------
*/


namespace Board
{
    public class Pathfinding : MonoBehaviour
    {
        private Board board;

        private void Awake()
        {
            board = GetComponent<Board>();
        }
        
        public List<BaseTile> FindPath(BaseTile startTile, BaseTile targetTile)
        {
            List<BaseTile> openSet = new List<BaseTile>();
            HashSet<BaseTile> closedSet = new HashSet<BaseTile>();
            openSet.Add(startTile);

            while (openSet.Count > 0)
            {
                BaseTile currentTile = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].fCost < currentTile.fCost || openSet[i].fCost == currentTile.fCost && openSet[i].hCost < currentTile.hCost)
                    {
                        currentTile = openSet[i];
                    }
                }

                openSet.Remove(currentTile);
                closedSet.Add(currentTile);

                if (currentTile == targetTile)
                {
                    return RetracePath(startTile, targetTile);
                }

                foreach (BaseTile neighbour in board.GetNeighbours(currentTile))
                {
                    if (!neighbour.walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentTile.gCost + GetDistance(currentTile, neighbour);
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetTile);
                        neighbour.parent = currentTile;

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }
            return null;
        }

        public List<BaseTile> RetracePath(BaseTile startTile, BaseTile endTile)
        {
            List<BaseTile> path = new List<BaseTile>();
            BaseTile currentTile = endTile;

            while (currentTile != startTile)
            {
                path.Add(currentTile);
                currentTile = currentTile.parent;
            }
            path.Reverse();
            return path;
        }
        
        int GetDistance(BaseTile tileA, BaseTile tileB)
        {
            int dstX = Mathf.Abs(tileA.gridX - tileB.gridX);
            int dstY = Mathf.Abs(tileA.gridY - tileB.gridY);

            if (dstX > dstY)
                return 14 * dstY + 10 * (dstX - dstY);
            return 14 * dstX + 10 * (dstY - dstX);
        }
    }
}
