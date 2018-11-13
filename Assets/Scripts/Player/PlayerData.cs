using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [System.Serializable]
    public class PlayerData
    {
        public float Speed = 1;
        public float Health = 100;
        public float Defense = .90f;
        //[HideInInspector]
        public static int Money;
        public float currentExp = 0;
        public float expToNextLevel = 100;
        public bool Alive = true;
    }
}
