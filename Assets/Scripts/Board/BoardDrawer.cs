using System.Collections.Generic;
using System.Linq;
using Characters;
using UnityEngine;

namespace Board
{
    public class BoardDrawer : MonoBehaviour
    {
        
        private BoardCalculator boardCalculator;
        
        // Start is called before the first frame update
        void Start()
        {
            boardCalculator = GetComponent<BoardCalculator>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        
        // Drawing movem
        public void DrawMovableTiles(GameObject hoverUnit)
        {
            int moves = hoverUnit.transform.GetComponent<TankBase>().movementValue;
            Vector2Int myPos = hoverUnit.transform.GetComponent<TankBase>().position;
            List<TileBase> moveTiles = boardCalculator.CalculateMovableTiles(myPos, moves);
            
            foreach (TileBase tile in moveTiles)
            {
                tile.SetMovable();
            }
        }
        
        public void TryDrawPath(GameObject selectedUnit, Vector2Int mousePosition)
        {
            if (!selectedUnit || !boardCalculator.CheckIfCoordinatesAreOnBoard(mousePosition) ||
                selectedUnit.GetComponent<TankBase>().state != TankState.Selected)
            {
                ClearBoardPathfinding();
                return;
            }

            int BoardSize = boardCalculator.GetBoardSize();
            
            Vector2Int startPosition = selectedUnit.GetComponent<TankBase>().position;
            if (mousePosition.x >= 0 && mousePosition.y >= 0 && mousePosition.x <= BoardSize-1 && mousePosition.y <= BoardSize-1)
                DrawPath(startPosition, mousePosition);
        }
        
        private void DrawPath(Vector2Int start, Vector2Int end)
        {
            var tileArray = boardCalculator.GetTileArray();
            if (!tileArray[end.x, end.y].walkable)
                return;
            
            List<TileBase> drawTiles = GetComponent<Pathfinding>().FindPath(tileArray[start.x, start.y], tileArray[end.x, end.y]);
            if (drawTiles.Any())
                drawTiles = drawTiles.Where(tile => tile.GetComponent<TileBase>().walkable).ToList();
            List<TileBase> noDrawTiles = new List<TileBase>();
            
            foreach (TileBase tile in drawTiles)
            {
                if (tile.GetComponent<TileBase>().walkable)
                    tile.SetPathfind();
            }
            
            foreach (TileBase tile in tileArray)
            {
                if (!drawTiles.Contains(tile))
                {
                    noDrawTiles.Add(tile);
                }
            }

            foreach (TileBase tile in noDrawTiles)
            {
                if (tile.walkable)
                    tile.SetDefault();
            }
            
        }
        
        public void DrawShootableTiles(List<TileBase> tilesToShoot)
        {
            foreach (TileBase tile in tilesToShoot)
            {
                tile.SetShootable();
            }
        }
        
        public void ClearBoardMovement()
        {
            var tileArray = boardCalculator.GetTileArray();
            foreach (TileBase tile in tileArray)
            {
                if (tile.state == TileState.Movable)
                    tile.SetDefault();
            }
        }
        
        public void ClearBoardPathfinding()
        {
            var tileArray = boardCalculator.GetTileArray();
            foreach (TileBase tile in tileArray)
            {
                if (tile.state == TileState.Pathfind)
                    tile.SetDefault();
            }
        }

        public void ClearBoardShootables()
        {
            var tileArray = boardCalculator.GetTileArray();
            foreach (TileBase tile in tileArray)
            {
                if (tile.state == TileState.Shootable)
                    tile.SetDefault();
            }
        }
    }
}
