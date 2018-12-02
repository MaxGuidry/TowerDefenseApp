using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;


namespace Advertisements
{
    public enum AdType
    {

        video,
        rewardedVideo,
        display,
        banner,
        playable,
        rewardedPlayable,
    }


    public class PlayAdvert
    {
        //when we add in option to pay to stop seeing ads they will still need to watch ads that give them a bonus.
        public static bool ShowThisUserRegularAds;
        private static bool initialized = false;
        public static void Init()
        {
            if (initialized)
                return;
            Monetization.Initialize("2928078", true);
            initialized = true;
        }
        public static void ShowAdvert(AdType type)
        {
            Global.Utils.StartCoroutineOnUtils(ShowAd(type));
        }

        private static IEnumerator ShowAd(AdType type)
        {
            
            while (!Monetization.isInitialized)
            {
                yield return new WaitForSeconds(.2f);
            }
            while (!Monetization.IsReady(type.ToString()))
            {
                yield return new WaitForSeconds(.2f);
            }

            var ad = Monetization.GetPlacementContent(type.ToString());
            if (ad == null)
            {
                Debug.LogError("Ad retrieved was null.");
                yield break;
            }
            while (!ad.ready) yield return new WaitForSeconds(.2f);
            yield return (ad as ShowAdPlacementContent).Show();


            Debug.LogError("Passed show ad");
            yield return null;
        }

    }
}