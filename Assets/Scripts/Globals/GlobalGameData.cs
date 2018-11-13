using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;


namespace Global
{
    public class GlobalGameData : MonoBehaviour
    {
        #region Global Static Members
        public static Player.PlayerData playerData;
        public bool firstGame;
        #endregion
        public Text money;

        //#region Editor Variables
        //[SerializeField]
        //private Player.PlayerData PlayerData;
        //#endregion

        private void Awake()
        {
            //TODO: Create this file on first play
            firstGame = !File.Exists(Application.persistentDataPath + "GameData/PlayerData.txt");

            //playerData = PlayerData;

            if (firstGame)
            {
                SetupForFirstGame();
            }

        }

        // Update is called once per frame
        void Update()
        {
#if UNITY_EDITOR
            money.text = Player.PlayerData.Money.ToString();
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                Player.PlayerData.Money += 1000;
            }
#endif
        }


        //TODO: use this function for anything all global setup that the game needs to do (mostly setup data that is persistant through gameplay)
        void SetupForFirstGame()
        {
            Player.PlayerData.Money = 500;

        }

    }
}