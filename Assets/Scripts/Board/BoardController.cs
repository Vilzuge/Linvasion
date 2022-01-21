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
    public class BoardController : MonoBehaviour
    {
        [SerializeField] private GameController controller;
        [SerializeField] private BoardCalculator boardCalculator;
        [SerializeField] private BoardDrawer boardDrawer;
        
        private GameObject selectedUnit;
        private Vector2Int mousePosition;

        private void Start()
        {
            selectedUnit = null;
            controller.SetGameState(GameState.PlayerTurn);
        }

        private void Update()
        {
            boardDrawer.UpdateHoverDraws(mousePosition, selectedUnit);
        }

        /* CALLED FROM BOARDINPUTHANDLER.CS */
        public void TrackMousePosition(Vector3 inputPosition)
        {
            Vector2Int endPosition = boardCalculator.CalculateCoordinatesFromPosition(inputPosition);
            mousePosition = endPosition;
        }

        /* CALLED FROM BOARDINPUTHANDLER.CS */
        public void OnTileSelected(Vector3 inputPosition)
        {
            var coordinates = boardCalculator.CalculateCoordinatesFromPosition(inputPosition);
            var unitObject = boardCalculator.GetUnitOnTile(coordinates);
            var tileArray = boardCalculator.GetTileArray();

            var selectedUnitComponent = selectedUnit.GetComponent<BaseUnitPlayer>();
            var selectedShooting = selectedUnit.GetComponent<UnitShooting>();
            var selectedMovement = selectedUnit.GetComponent<UnitMovement>();
            
            // IS IT PLAYERS TURN
            if (!controller.IsPlayerTurnActive())
                return;

            // IF SELECTED UNIT EXISTS
            if (selectedUnit)
            {
                // UNIT IS AIMING AND TRIES TO SHOOT AT COORDS -> TRY SHOOTING WITH THE UNIT
                if (selectedUnitComponent.state == TankState.Aiming)
                {
                    selectedShooting.TryToShoot(tileArray[coordinates.x, coordinates.y]);
                    boardDrawer.ClearBoardShootables();
                    DeselectUnit();
                }

                // SELECTED UNIT IS BEING PRESSED -> DESELECT
                else if (unitObject != null && selectedUnit == unitObject)
                {
                    DeselectUnit();
                }

                // ANOTHER UNIT IS BEING PRESSED -> SELECT THE NEW ONE
                else if (unitObject != null && selectedUnit != unitObject)
                    SelectUnit(coordinates);
                
                // UNIT IS SELECTED AND CAN MOVE TO THE TILE PRESSED
                else if (selectedMovement.CanMoveTo(tileArray[coordinates.x, coordinates.y]) && selectedUnitComponent.state == TankState.Selected)
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
                if (unitObject != null && unitObject.GetComponent<BaseUnit>())
                {
                    SelectUnit(coordinates);
                }
            }
        }
        
        // Moving a selected unit
        private void MoveSelectedUnit(Vector2Int endCoordinates)
        {
            selectedUnit.GetComponent<UnitMovement>().MoveTo(endCoordinates);
            Debug.Log(selectedUnit.name + " was moved");
        }
    
        // Unit selection
        private void SelectUnit(Vector2Int coordinates)
        {
            DeselectUnit();
            selectedUnit = boardCalculator.GetUnitOnTile(coordinates);
            selectedUnit.GetComponent<BaseUnitPlayer>().SetSelected();
            Debug.Log(selectedUnit.name + " was selected");
        }
        
        // Unit deselection
        private void DeselectUnit()
        {
            if (!selectedUnit) return;
            selectedUnit.GetComponent<BaseUnitPlayer>().SetDeselected();
            Debug.Log(selectedUnit.name + " was deselected");
            selectedUnit = null;
        }
    }
}
