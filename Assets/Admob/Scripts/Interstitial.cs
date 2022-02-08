using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
public class Interstitial : MonoBehaviour
{
    private InterstitialAd interstitial;
    public string interstitialId = "ca-app-pub-3940256099942544/8691691433";


    private void Start()
    {
        RequestInterstitial();
    }
    private void OnDestroy()
    {
        interstitial.Destroy();
    }

    void RequestInterstitial()
    {
        interstitial = new InterstitialAd(interstitialId);
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);

    }

     void ShowGameOverAd()
    {
        if(this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }

        
    }

    public void ShowInterstitial()
    {
        ShowGameOverAd();
        RequestInterstitial();
    }
}
