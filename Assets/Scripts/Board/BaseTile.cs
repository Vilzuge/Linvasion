using UnityEngine;

/*
-------------------------------------------
Base tile that other tile types are extended from
-------------------------------------------
*/
namespace Board
{
    public class BaseTile : MonoBehaviour
    {
        public int row;
        public int col;

        public int gCost;
        public int hCost;

        public int fCost
        {
            get
            {
                return gCost + hCost;
            }
        }
    }
}
