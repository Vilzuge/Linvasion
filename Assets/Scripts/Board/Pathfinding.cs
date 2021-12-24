﻿using System.Collections.Generic;
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
        private BoardCalculator boardCalculator;

        private void Awake()
        {
            boardCalculator = GetComponent<BoardCalculator>();
        }
        
        public List<BaseTile> FindPath(BaseTile start, BaseTile target)
        {
            var openSet = new List<BaseTile>();
            var closedSet = new HashSet<BaseTile>();
            openSet.Add(start);

            while (openSet.Count > 0)
            {
                BaseTile current = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].fCost < current.fCost || openSet[i].fCost == current.fCost && openSet[i].hCost < current.hCost)
                    {
                        current = openSet[i];
                    }
                }

                openSet.Remove(current);
                closedSet.Add(current);

                if (current == target)
                {
                    return RetracePath(start, target);
                }

                foreach (BaseTile neighbour in boardCalculator.GetNeighbours(current))
                {
                    if ( !neighbour.IsWalkable() || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = current.gCost + GetDistance(current, neighbour);
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, target);
                        neighbour.parent = current;

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }
            return null;
        }

        private List<BaseTile> RetracePath(BaseTile start, BaseTile end)
        {
            List<BaseTile> path = new List<BaseTile>();
            BaseTile current = end;

            while (current != start)
            {
                path.Add(current);
                current = current.parent;
            }
            path.Reverse();
            return path;
        }

        private int GetDistance(BaseTile a, BaseTile b)
        {
            int dstX = Mathf.Abs(a.gridX - b.gridX);
            int dstY = Mathf.Abs(a.gridY - b.gridY);

            if (dstX > dstY)
                return 14 * dstY + 10 * (dstX - dstY);
            return 14 * dstX + 10 * (dstY - dstX);
        }
    }
}
