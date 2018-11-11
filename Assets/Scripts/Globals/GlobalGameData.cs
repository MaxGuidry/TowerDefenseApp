using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


namespace Global
{
    public class GlobalGameData : MonoBehaviour
    {
        #region Global Static Members
        public static Player.PlayerData playerData;
        public static bool firstGame;
        #endregion


        #region Editor Variables
        [SerializeField]
        private Player.PlayerData PlayerData;
        #endregion

        private void Awake()
        {
            //TODO: Create this file on first play
            firstGame = !File.Exists(Application.persistentDataPath + "/PlayerData");


            
            playerData = PlayerData;
            Utils.SaveFile<Player.PlayerData>(PlayerData, "PlayerData");

            var obj = Utils.LoadFileToObject<Player.PlayerData>("PlayerData");
            if (firstGame)
            {
                SetupForFirstGame();
            }

        }
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                playerData.money += 1000;
            }

#endif
        }


        //TODO: use this function for anything all global setup that the game needs to do (mostly setup data that is persistant through gameplay)
        void SetupForFirstGame()
        {
            playerData.money = 500;

        }

    }
}