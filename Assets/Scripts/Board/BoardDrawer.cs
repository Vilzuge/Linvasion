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
            if (hoverUnit && hoverUnit.GetComponent<BaseUnit>() && hoverUnit.GetComponent<BaseUnit>().state != TankState.Aiming && !selectedUnit)
                DrawMovableTiles(hoverUnit);
            else
            {
                ClearBoardPathfinding();
                ClearBoardMovement();
            }
            TryDrawPath(selectedUnit, mousePosition);
        }
        
        // Drawing movement tiles
        public void DrawMovableTiles(GameObject unit)
        {
            List<TileBase> moveTiles = boardCalculator.CalculateMovableTiles(unit);
            foreach (TileBase tile in moveTiles)
            {
                tile.SetMovableVisual();
            }
        }
        
        public void DrawShootableTiles(List<TileBase> tilesToShoot)
        {
            foreach (TileBase tile in tilesToShoot)
            {
                tile.SetShootableVisual();
            }
        }
        
        // Trying to draw a path for the unit
        private void TryDrawPath(GameObject selectedUnit, Vector2Int mousePosition)
        {
            if (!selectedUnit)
                return;
            
            var startPosition = selectedUnit.GetComponent<UnitMovement>().position;
            var boardSize = boardCalculator.GetBoardSize();
            
            
            if (selectedUnit.GetComponent<BaseUnit>().state != TankState.Selected)
                ClearBoardPathfinding();
            else
            {
                DrawMovableTiles(selectedUnit);
                if (mousePosition.x >= 0 && mousePosition.y >= 0 && mousePosition.x <= boardSize - 1 &&
                    mousePosition.y <= boardSize - 1)
                {
                    DrawPath(selectedUnit, startPosition, mousePosition);
                }
            }
        }
        
        private void DrawPath(GameObject selectedUnit, Vector2Int start, Vector2Int end)
        {
            var tileArray = boardCalculator.GetTileArray();
            var movableTiles = selectedUnit.GetComponent<UnitMovement>().GetAvailableMoves();
            
            if (!tileArray[end.x, end.y].IsWalkable())
                return;

            List<TileBase> drawTiles = GetComponent<Pathfinding>().FindPath(tileArray[start.x, start.y], tileArray[end.x, end.y]);
            if (drawTiles.Any())
                drawTiles = drawTiles.Where(tile => tile.GetComponent<TileBase>().IsWalkable()).ToList();
            
            foreach (TileBase tile in drawTiles)
            {
                if (movableTiles.Contains(tile))
                    tile.SetPathfindVisual();
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
