using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

namespace Assets.CodeBase.AdMob.Scripts
{
    public class Interstitial : MonoBehaviour
    {
        private InterstitialAd _interstitial;
        private const string TestKey = "ca-app-pub-3940256099942544/1033173712";
        private const string PubKey = "HideForGit";
        public string interstitialId = TestKey;


        private void OnEnable()
        {
            RequestInterstitial();
        }
        private void OnDestroy()
        {
            _interstitial.Destroy();
        }

        void RequestInterstitial()
        {
            _interstitial = new InterstitialAd(interstitialId);
            AdRequest request = new AdRequest.Builder().Build();
            _interstitial.LoadAd(request);

        }

        void ShowGameOverAd()
        {
            if (_interstitial.IsLoaded())
            {
                _interstitial.Show();
            }


        }

        public void ShowInterstitial()
        {
            ShowGameOverAd();
            RequestInterstitial();
        }
    }
}