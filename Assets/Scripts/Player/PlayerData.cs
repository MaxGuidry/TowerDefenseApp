using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "ScriptableObject", menuName = "PlayerData")]
    [System.Serializable]
    public class PlayerData : ScriptableObject
    {
        public float Speed;
        public float Health;
        public float Defense;
        public int money = 0;

    }
}
