using UnityEngine;

/*
-------------------------------------------
Water tile, not walkable tile
-------------------------------------------
*/

namespace Board
{
    public class TileWater : TileBase
    {
        public override void Start()
        {
            defaultTile = Resources.Load<Material>("Materials/GroundShootable");
        }
        
        public override void SetDefaultMaterial()
        {
            transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material = defaultTile;
        }
    }
}
