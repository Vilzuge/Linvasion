using System.Linq;
using System.Collections.Generic;
using Characters;
using Game;
using UnityEngine;

/*
-------------------------------------------
This script handles the game-board and it's visualization
-------------------------------------------
*/

namespace Board
{
    public class Board : MonoBehaviour
    {
        private Vector2Int mousePosition;
        
        [SerializeField] private GameController controller;
        [SerializeField] private GameObject selectedUnit;
        
        private BoardCalculator boardCalculator;
        private BoardDrawer boardDrawer;
        
        private void Awake()
        {
            boardCalculator = GetComponent<BoardCalculator>();
            boardDrawer = GetComponent<BoardDrawer>();
        }
        
        private void Start()
        {
            mousePosition = new Vector2Int();

            controller.SetGameState(GameState.PlayerTurn);
            
            var tileArray = boardCalculator.GetTileArray();
            
            foreach (TileBase tile in tileArray)
            {
                int row = (int) tile.worldPosition.x;
                int col = (int) tile.worldPosition.z;
                if (boardCalculator.GetUnitOnTile(new Vector2Int(row, col)) != null)
                {
                    tileArray[row, col].walkable = false;
                }
            }
        }

        private void Update()
        {
            GameObject hoverUnit = boardCalculator.GetUnitOnTile(mousePosition);
            if (hoverUnit && hoverUnit.GetComponent<TankBase>() && hoverUnit.GetComponent<TankBase>().state != TankState.Aiming && !selectedUnit)
                boardDrawer.DrawMovableTiles(hoverUnit);
            else
            {
                boardDrawer.ClearBoardPathfinding();
                boardDrawer.ClearBoardMovement();
            }
            boardDrawer.TryDrawPath(selectedUnit, mousePosition);
        }
        
        
        /* CALLED FROM BOARDINPUTHANDLER.CS*/
        public void TrackMousePosition(Vector3 inputPosition)
        {
            Vector2Int endPosition = boardCalculator.CalculateCoordinatesFromPosition(inputPosition);
            mousePosition = endPosition;
        }

        /* CALLED FROM BOARDINPUTHANDLER.CS*/
        public void OnTileSelected(Vector3 inputPosition)
        {
            var coordinates = boardCalculator.CalculateCoordinatesFromPosition(inputPosition);
            var unitObject = boardCalculator.GetUnitOnTile(coordinates);
            var tileArray = boardCalculator.GetTileArray();
            
            // IS IT PLAYERS TURN
            if (!controller.CanPerformMove())
                return;

            // IF SELECTED UNIT EXISTS
            if (selectedUnit)
            {
                // UNIT IS AIMING AND TRIES TO SHOOT AT COORDS -> TRY SHOOTING WITH THE UNIT
                if (selectedUnit.GetComponent<TankBase>().state == TankState.Aiming)
                {
                    selectedUnit.GetComponent<TankBase>().TryToShoot(tileArray[coordinates.x, coordinates.y]);
                    boardDrawer.ClearBoardShootables();
                    DeselectUnit();
                }

                // SELECTED UNIT IS BEING PRESSED -> DESELECT
                else if (unitObject != null && selectedUnit == unitObject)
                    DeselectUnit();

                // ANOTHER UNIT IS BEING PRESSED -> SELECT THE NEW ONE
                else if (unitObject != null && selectedUnit != unitObject && controller.IsTeamTurnActive())
                    SelectUnit(coordinates);
                
                // UNIT IS SELECTED AND CAN MOVE TO THE TILE PRESSED
                else if (selectedUnit.GetComponent<TankBase>().CanMoveTo(tileArray[coordinates.x, coordinates.y]) && selectedUnit.GetComponent<TankBase>().state == TankState.Selected)
                {
                    MoveSelectedUnit(coordinates);
                    DeselectUnit();
                    boardDrawer.ClearBoardMovement();
                    boardDrawer.ClearBoardPathfinding();
                }
            }
            // IF THERE IS NO SELECTED UNIT
            else
            {
                // NO UNITS SELECTED -> SELECT THE NEW ONE
                if (unitObject != null && controller.IsTeamTurnActive() && unitObject.GetComponent<TankBase>())
                {
                    SelectUnit(coordinates);
                }
            }
        }
        
        // Applying damage to a unit
        public void ApplyDamage(Vector2Int coordsShotAt, int damageValue)
        {
            var tileArray = boardCalculator.GetTileArray();
            
            TileBase damaged = tileArray[coordsShotAt.x, coordsShotAt.y];
            GameObject damagedUnit = boardCalculator.GetUnitOnTile(coordsShotAt);
            if (!damagedUnit)
                return;
            if (damagedUnit.GetComponent<TankBase>())
                damagedUnit.GetComponent<TankBase>().Damage(damageValue);

            else if (damagedUnit.GetComponent<EnemyBase>())
                damagedUnit.GetComponent<EnemyBase>().Damage(damageValue);
        }
        
        // Moving a selected unit
        private void MoveSelectedUnit(Vector2Int endCoordinates)
        {
            Vector2Int pos = selectedUnit.GetComponent<TankBase>().position;
            selectedUnit.GetComponent<TankBase>().MoveTo(endCoordinates);
            Debug.Log(selectedUnit.name + " was moved");
        }
    
        // Unit selection
        private void SelectUnit(Vector2Int coordinates)
        {
            DeselectUnit();
            selectedUnit = boardCalculator.GetUnitOnTile(coordinates);
            selectedUnit.GetComponent<TankBase>().SetSelected();
            Debug.Log(selectedUnit.name + " was selected");
            // TODO: Drawing movable tiles
        }
        
        // Unit deselection
        private void DeselectUnit()
        {
            if (!selectedUnit) return;
            Debug.Log(selectedUnit.name + " will be deselected");
            selectedUnit.GetComponent<TankBase>().SetDeselected();
            selectedUnit = null;
        }
        
    }
}
