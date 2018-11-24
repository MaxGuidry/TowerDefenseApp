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
        public static void Init()
        {
            Monetization.Initialize("2928078", true);

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
                throw new System.Exception("Ad was null");
            }
            while (!ad.ready) yield return new WaitForSeconds(.2f);
            yield return (ad as ShowAdPlacementContent).Show();


            Debug.LogError("Passed show ad");
            yield return null;
        }

    }
}