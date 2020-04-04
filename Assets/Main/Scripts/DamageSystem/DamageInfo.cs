using UnityEngine;

namespace Gamaga.DamageSystem
{
    public struct DamageInfo
    {
        public int dmg;
        public float force;
        public Vector2 dir;
        public ForceMode2D forceMode;
        public GameObject sender;
    }

}
