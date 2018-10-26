using System.Collections;
using System.Collections.Generic;
using Global;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UserInterface
{
    public class MainMenuUIManager : MonoBehaviour
    {
        public Sprite SoundOn, SoundOff, MusicOn, MusicOff;

        // Use this for initialization
        void Start()
        {
            Utils.MusicOn = true;
            Utils.SoundOn = true;
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void SoundAction()
        {
            var button = EventSystem.current.currentSelectedGameObject;
            var sprite = button.GetComponent<Button>().image.sprite;
            sprite = sprite == SoundOn ? SoundOff : SoundOn;
            Utils.SoundOn = sprite != SoundOn;
        }

        public void MusicAction()
        {
            var button = EventSystem.current.currentSelectedGameObject;
            var sprite = button.GetComponent<Button>().image.sprite;
            sprite = sprite == MusicOn ? MusicOff : MusicOn;
            Utils.MusicOn = sprite != SoundOn;
        }

        public void StartGame()
        {

        }

        public void Credits()
        {

        }

        public void GooglePlay()
        {

        }

        public void Achievements()
        {

        }
    }
}
