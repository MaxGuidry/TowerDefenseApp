using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower
{
    [System.Serializable]
    public class TowerData
    {
        
        public enum TowerType
        {
            LONG,
            SHORT,
            SPREAD
        };
        [HideInInspector]
        public TowerType type;
        public int damage;
        public float range;
        public float fireRate;
        public int buyCost;
        public int upgradeCost;
        public int bulletCount;
        public float spreadAngle;
        public float bulletFireVel = 1;
        public string TowerName;
        public Sprite TowerSprite;
    }
}