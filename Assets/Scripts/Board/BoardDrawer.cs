using UnityEngine;

namespace Board
{
    public class BoardDrawer : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        
        /*
        private void DrawMovableTiles(GameObject hoverUnit)
        {
            int moves = hoverUnit.transform.GetComponent<TankBase>().movementValue;
            Vector2Int myPos = hoverUnit.transform.GetComponent<TankBase>().position;
            List<TileBase> moveTiles = CalculateMovableTiles(myPos, moves);
            
            foreach (TileBase tile in moveTiles)
            {
                tile.SetMovable();
            }
        }
        
        private void TryDrawPath()
        {
            if (!selectedUnit || !CheckIfCoordinatesAreOnBoard(mousePosition) ||
                selectedUnit.GetComponent<TankBase>().state != TankState.Selected)
            {
                ClearBoardPathfinding();
                return;
            }
            Vector2Int startPosition = selectedUnit.GetComponent<TankBase>().position;
            if (mousePosition.x >= 0 && mousePosition.y >= 0 && mousePosition.x <= BoardSize-1 && mousePosition.y <= BoardSize-1)
                DrawPath(startPosition, mousePosition);
        }
        
        public void DrawShootableTiles(List<TileBase> tilesToShoot)
        {
            foreach (TileBase tile in tilesToShoot)
            {
                tile.SetShootable();
            }
        }
        
        private void ClearBoardMovement()
        {
            foreach (TileBase tile in tileArray)
            {
                if (tile.state == TileState.Movable)
                    tile.SetDefault();
            }
        }
        
        private void ClearBoardPathfinding()
        {
            foreach (TileBase tile in tileArray)
            {
                if (tile.state == TileState.Pathfind)
                    tile.SetDefault();
            }
        }

        private void ClearBoardShootables()
        {
            foreach (TileBase tile in tileArray)
            {
                if (tile.state == TileState.Shootable)
                    tile.SetDefault();
            }
        }
        
        */
        
        
        
    }
}
