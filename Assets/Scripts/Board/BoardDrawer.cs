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
        
        public void UpdateHoverDraws(Vector2Int mousePosition, GameObject selectedUnit)
        {
            var hoverUnit = boardCalculator.GetUnitOnTile(mousePosition);
            if (hoverUnit && hoverUnit.GetComponent<TankBase>() && hoverUnit.GetComponent<TankBase>().state != TankState.Aiming && !selectedUnit)
                DrawMovableTiles(hoverUnit);
            else
            {
                ClearBoardPathfinding();
                ClearBoardMovement();
            }
            TryDrawPath(selectedUnit, mousePosition);
        }
        
        // Drawing movement tiles
        public void DrawMovableTiles(GameObject hoverUnit)
        {
            int moves = hoverUnit.transform.GetComponent<TankBase>().movementValue;
            Vector2Int myPos = hoverUnit.transform.GetComponent<TankBase>().position;
            List<TileBase> moveTiles = boardCalculator.CalculateMovableTiles(myPos, moves);
            
            foreach (TileBase tile in moveTiles)
            {
                tile.SetMovableVisual();
            }
        }
        
        // Trying to draw a path for the unit
        public void TryDrawPath(GameObject selectedUnit, Vector2Int mousePosition)
        {
            if (!selectedUnit || !BoardCalculator.CheckIfCoordinatesAreOnBoard(mousePosition) ||
                selectedUnit.GetComponent<TankBase>().state != TankState.Selected)
            {
                ClearBoardPathfinding();
                return;
            }

            var boardSize = boardCalculator.GetBoardSize();
            var startPosition = selectedUnit.GetComponent<TankBase>().position;
            
            if (mousePosition.x >= 0 && mousePosition.y >= 0 && mousePosition.x <= boardSize-1 && mousePosition.y <= boardSize-1)
                DrawPath(startPosition, mousePosition);
        }
        
        private void DrawPath(Vector2Int start, Vector2Int end)
        {
            var tileArray = boardCalculator.GetTileArray();
            if (!tileArray[end.x, end.y].IsWalkable())
                return;
            
            List<TileBase> drawTiles = GetComponent<Pathfinding>().FindPath(tileArray[start.x, start.y], tileArray[end.x, end.y]);
            if (drawTiles.Any())
                drawTiles = drawTiles.Where(tile => tile.GetComponent<TileBase>().IsWalkable()).ToList();
            List<TileBase> noDrawTiles = new List<TileBase>();
            
            foreach (TileBase tile in drawTiles)
            {
                if (tile.GetComponent<TileBase>().IsWalkable())
                    tile.SetPathfindVisual();
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
                if (tile.IsWalkable())
                    tile.SetDefaultVisual();
            }
            
        }
        
        public void DrawShootableTiles(List<TileBase> tilesToShoot)
        {
            foreach (TileBase tile in tilesToShoot)
            {
                tile.SetShootableVisual();
            }
        }
        
        public void ClearBoardMovement()
        {
            var tileArray = boardCalculator.GetTileArray();
            foreach (TileBase tile in tileArray)
            {
                if (tile.state == TileState.Movable)
                    tile.SetDefaultVisual();
            }
        }
        
        public void ClearBoardPathfinding()
        {
            var tileArray = boardCalculator.GetTileArray();
            foreach (TileBase tile in tileArray)
            {
                if (tile.state == TileState.Pathfind)
                    tile.SetDefaultVisual();
            }
        }

        public void ClearBoardShootables()
        {
            var tileArray = boardCalculator.GetTileArray();
            foreach (TileBase tile in tileArray)
            {
                if (tile.state == TileState.Shootable)
                    tile.SetDefaultVisual();
            }
        }
    }
}
