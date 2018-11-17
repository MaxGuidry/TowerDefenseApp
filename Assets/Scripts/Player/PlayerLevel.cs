using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Global;

namespace Player
{
    public class PlayerLevel : MonoBehaviour
    {

        //players base stats
        int baseLevel = 1;
        float baseSpeed = 1;
        float baseHealth = 100;
        float baseDefense = .90f;

        //values players stats will be upgraded by
        public int upgradeLevel = 1;
        public int upgradePrestige = 1;
        public float upgradeSpeed = 1;
        public float upgradeHealth = 10;
        public float upgradeDefense = .02f;
        public float currentExp = 0;
        public float expToNextLevel = 100;

        /// <summary>
        /// gives experience to the player
        /// </summary>
        public void AddExperience()
        {
            GlobalGameData.playerData.currentExp += 10;
            if (GlobalGameData.playerData.currentExp >= GlobalGameData.playerData.expToNextLevel)
                UpgradePlayer();
            GlobalGameData.expEvent.Invoke();
        }

        /// <summary>
        /// Increases the players stats on level up
        /// </summary>
        public void UpgradePlayer()
        {
            GlobalGameData.playerData.level += upgradeLevel;         
            GlobalGameData.playerData.Speed += upgradeSpeed;
            GlobalGameData.playerData.Health += upgradeHealth;
            GlobalGameData.playerData.Defense -= upgradeDefense;
            GlobalGameData.playerData.currentExp = 0;
            GlobalGameData.playerData.expToNextLevel += expToNextLevel;
            if (GlobalGameData.playerData.level == 5)
                PrestigePlayer();
            GlobalGameData.upgradeEvent.Invoke();
        }

        /// <summary>
        /// resets the players to slightly increased base stats
        /// </summary>
        public void PrestigePlayer()
        {
            GlobalGameData.playerData.prestige += upgradePrestige;
            GlobalGameData.playerData.level = baseLevel;
            GlobalGameData.playerData.Speed = baseSpeed += 1;
            GlobalGameData.playerData.Health = baseHealth += 10;
            GlobalGameData.playerData.Defense = baseDefense -= .02f;
            GlobalGameData.playerData.expToNextLevel = expToNextLevel += 100;
            IncreaseUpgradeValue();
            GlobalGameData.prestigeEvent.Invoke();
        }

        /// <summary>
        /// increases the value of the stats the player will use to upgrade
        /// </summary>
        public void IncreaseUpgradeValue()
        {
            upgradeHealth += 10;
            upgradeDefense += .02f;
            upgradeSpeed += 1;
            expToNextLevel += 100;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                UpgradePlayer();
        }
    }
}