using UnityEngine;

namespace Board
{
    public class TileGrass : BaseTile
    {
        public override void Start()
        {
            defaultTile = Resources.Load<Material>("Materials/GroundGreen");
            moveableTile = Resources.Load<Material>("Materials/GroundHighlight");
        }
        
        
        public override void SetDefaultMaterial()
        {
            transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material = defaultTile;
        }
    }
}
