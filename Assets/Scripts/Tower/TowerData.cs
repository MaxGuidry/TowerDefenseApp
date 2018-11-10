using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower
{
    [CreateAssetMenu(fileName = "ScriptableObject", menuName = "TowerData")]
    public class TowerData : ScriptableObject
    {
        public enum TowerType
        {
            LONG,
            SHORT,
            SPREAD
        };
        public TowerType type;
        public int damage;
        public float range;
        public float fireRate;
        public int level;
        public int buyCost;
        public int upgradeCost;
        public int bulletCount;
        public float spreadAngle;
        public float bulletFireVel = 1;
        public string TowerName;
        public Sprite TowerSprite;
    }
}