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

        public override void SetDefault()
        {
            base.SetDefault();
            transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material = defaultTile;
        }
        
        public override void SetMovable()
        {
            base.SetMovable();
            transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material = moveableTile;
        }
        
        public override void SetPathfind()
        {
            base.SetPathfind();
            transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material = pathTile;
        }
        
        public override void SetShootable()
        {
            base.SetShootable();
            transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material = aimTile;
        }
    }
}
