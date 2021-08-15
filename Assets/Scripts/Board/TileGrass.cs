using UnityEngine;

/*
-------------------------------------------
Grass tile, walkable normal tile
-------------------------------------------
*/

namespace Board
{
    public class TileGrass : TileBase
    {
        public override void Start()
        {
            defaultTile = Resources.Load<Material>("Materials/M_GroundGrass");
            moveableTile = Resources.Load<Material>("Materials/M_GroundMovable");
            pathTile = Resources.Load<Material>("Materials/M_GroundPathfind");
            aimTile = Resources.Load<Material>("Materials/M_GroundShootable");
        }

        public override void SetDefaultMaterial()
        {
            transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material = defaultTile;
        }
        
        public override void SetMovableMaterial()
        {
            transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material = moveableTile;
        }
        
        public override void SetPathMaterial()
        {
            transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material = pathTile;
        }
        
        public override void SetShootableMaterial()
        {
            transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material = aimTile;
        }
    }
}
