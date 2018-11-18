using System;
using System.Collections.Generic;
using System.Globalization;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class InGameUIManager : MonoBehaviour
    {
        public static InGameUIManager Instance;
        public Transform StatHolderTransform, TimeTransform;
        public GameObject PausePanel, ShopPanel;
        public PlayerData PlayerData;
        public UiStat Health, Currency, WaveCount;

        private List<GameObject> _statHolders;
        private List<UiStat> _uiStats;
        private int _gameSpeed;

        private void Awake()
        {
            Instance = this;
            PausePanel.SetActive(false);
            ShopPanel.SetActive(false);

            _gameSpeed = 1;

            _statHolders = new List<GameObject>
            {
                StatHolderTransform.GetChild(0).gameObject,
                StatHolderTransform.GetChild(1).gameObject,
                StatHolderTransform.GetChild(2).gameObject
            };
        }

        private void Update()
        {
            Health.Stat = PlayerData.Health;
            Currency.Stat = 0f;
            WaveCount.Stat = 0f;

            _uiStats = new List<UiStat>
            {
                Health,
                Currency,
                WaveCount
            };

            for (var i = 0; i < _statHolders.Count; i++)
            {
                _statHolders[i].transform.GetChild(0).GetComponent<Text>().text =
                    _uiStats[i].Stat.ToString(CultureInfo.InvariantCulture);
                _statHolders[i].transform.GetChild(1).GetComponent<Image>().sprite = _uiStats[i].StatSprite;

                if (i == 0)
                {
                    _statHolders[i].transform.GetChild(1).GetComponent<Image>().color = new Color(255, 0, 0, 255);
                }
            }
        }

        /// <summary>
        ///     Either pauses the game or continues the game
        /// </summary>
        public void PauseGame()
        {
            if (Time.timeScale != 0)
            {
                Time.timeScale = 0;
                PausePanel.SetActive(true);
            }
            else
            {
                Time.timeScale = _gameSpeed;
                PausePanel.SetActive(false);
            }
        }

        /// <summary>
        ///     Speed up the game 
        /// </summary>
        public void GameSpeed()
        {
            _gameSpeed++;
            if (_gameSpeed == 4)
            {
                _gameSpeed = 1;
            }

            TimeTransform.GetChild(0).GetComponentInChildren<Text>().text = "x" + _gameSpeed;
            Time.timeScale = _gameSpeed;
        }
        
        /// <summary>
        ///     Opens up shop ui
        /// </summary>
        public void ShopActive()
        {
            ShopPanel.SetActive(!ShopPanel.activeInHierarchy);
        }

        /// <summary>
        ///     A UiStat that holds the value and Sprite
        /// </summary>
        [Serializable]
        public struct UiStat
        {
            public float Stat;
            public Sprite StatSprite;
        }
    }
}