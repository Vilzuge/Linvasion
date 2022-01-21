using System.Collections.Generic;
using System.Linq;
using Characters;
using UnityEngine;

namespace Board
{
    public class BoardDrawer : MonoBehaviour
    {
        
        [SerializeField] private BoardCalculator boardCalculator;
        
        // Start is called before the first frame update
        void Start()
        {

        }
        
        public void UpdateHoverDraws(Vector2Int mousePosition, GameObject selectedUnit)
        {
            var hoverUnit = boardCalculator.GetUnitOnTile(mousePosition);
            var hoverUnitComponent = hoverUnit.GetComponent<BaseUnitPlayer>();
            
            if (hoverUnit && !selectedUnit && hoverUnitComponent && hoverUnitComponent.state != TankState.Aiming)
                DrawMovableTiles(hoverUnit);
            else
            {
                ClearBoardPathfinding();
                ClearBoardMovement();
            }
            TryDrawPath(selectedUnit, mousePosition);
        }
        
        // Drawing movement tiles
        private void DrawMovableTiles(GameObject unit)
        {
            List<BaseTile> moveTiles = boardCalculator.CalculateMovableTiles(unit);
            foreach (var tile in moveTiles)
            {
                tile.SetMovableVisual();
            }
        }
        
        public void DrawShootableTiles(List<BaseTile> tilesToShoot)
        {
            foreach (var tile in tilesToShoot)
            {
                tile.SetShootableVisual();
            }
        }
        
        // Trying to draw a path for the unit
        private void TryDrawPath(GameObject selectedUnit, Vector2Int mousePosition)
        {
            var selectedMovement = selectedUnit.GetComponent<UnitMovement>();
            var selectedUnitComponent = selectedUnit.GetComponent<BaseUnitPlayer>();
            var boardSize = boardCalculator.GetBoardSize();
            
            if (!selectedUnit)
            {
                return;
            }

            if (selectedUnitComponent.state != TankState.Selected)
            {
                ClearBoardPathfinding();
            }
            else
            {
                DrawMovableTiles(selectedUnit);
                if (mousePosition.x >= 0 && mousePosition.y >= 0 && mousePosition.x <= boardSize - 1 &&
                    mousePosition.y <= boardSize - 1)
                {
                    DrawPath(selectedUnit, selectedMovement.position, mousePosition);
                }
            }
        }
        
        private void DrawPath(GameObject selectedUnit, Vector2Int start, Vector2Int end)
        {
            var tileArray = boardCalculator.GetTileArray();
            var movableTiles = selectedUnit.GetComponent<UnitMovement>().GetAvailableMoves();
            var selectedPathfinding = GetComponent<Pathfinding>();

            if (!tileArray[end.x, end.y].IsWalkable())
            {
                return;
            }

            var drawTiles = selectedPathfinding.FindPath(tileArray[start.x, start.y], tileArray[end.x, end.y]);
            if (drawTiles.Any())
            {
                drawTiles = drawTiles.Where(tile => tile.GetComponent<BaseTile>().IsWalkable()).ToList();
            }
            
            foreach (var tile in drawTiles)
            {
                if (movableTiles.Contains(tile))
                {
                    tile.SetPathfindVisual();
                }
            }
        }

        public void ClearBoardMovement()
        {
            var tileArray = boardCalculator.GetTileArray();
            foreach (var tile in tileArray)
            {
                if (tile.state == TileState.Movable)
                {
                    tile.SetDefaultVisual();
                }
            }
        }
        
        public void ClearBoardPathfinding()
        {
            var tileArray = boardCalculator.GetTileArray();
            foreach (var tile in tileArray)
            {
                if (tile.state == TileState.Pathfind)
                {
                    tile.SetDefaultVisual();
                }
            }
        }

        public void ClearBoardShootables()
        {
            var tileArray = boardCalculator.GetTileArray();
            foreach (var tile in tileArray)
            {
                if (tile.state == TileState.Shootable)
                {
                    tile.SetDefaultVisual();
                }
            }
        }
    }
}
