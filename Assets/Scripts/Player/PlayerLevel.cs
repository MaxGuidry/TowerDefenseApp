using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerLevel : MonoBehaviour
    {
        public List<PlayerData> levels;
        public void OnEnable()
        {
            Global.GlobalGameData.playerData = levels[0];
        }

        public PlayerData UpgradePlayer()
        {
            int currentLevelIndex = levels.IndexOf(Global.GlobalGameData.playerData);
            int maxLevelIndex = levels.Count - 1;
            if (currentLevelIndex < maxLevelIndex)
            {
                return levels[currentLevelIndex + 1];
            }
            else
                return Global.GlobalGameData.playerData;
        }

        public void AddExperience()
        {
            Global.GlobalGameData.playerData.currentExp += 10;
            if (Global.GlobalGameData.playerData.currentExp >= Global.GlobalGameData.playerData.expToNextLevel)
                Global.GlobalGameData.playerData = UpgradePlayer();
        }
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                AddExperience();
        }
    }
}