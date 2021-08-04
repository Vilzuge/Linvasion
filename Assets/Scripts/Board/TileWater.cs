using UnityEngine;

namespace Board
{
    public class TileWater : BaseTile
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
