using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
public class Interstitial : MonoBehaviour
{
    private InterstitialAd _interstitial;
    /// AdMob test pub key
    public string interstitialId = "ca-app-pub-3940256099942544/8691691433";


    private void Start()
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
        if(this._interstitial.IsLoaded())
        {
            this._interstitial.Show();
        }

        
    }

    public void ShowInterstitial()
    {
        ShowGameOverAd();
        RequestInterstitial();
    }
}
