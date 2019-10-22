using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using mixpanel;
using System;

public class AdMobBannerRectangle : MonoBehaviour {

    public string adBannerId = "/21700095690/unit_320_250";

    private BannerView bannerView;

    // Use this for initialization
    void Start () {
        RequestBanner();
        var props = new Value();
        props["admobunitid"] = adBannerId;
        props["event"] = "admob-showbanner";
        Mixpanel.Track("admob-banner", props);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void RequestBanner()
    {
        #if UNITY_ANDROID
            string adUnitId = adBannerId;
        #elif UNITY_IPHONE
        string adUnitId = "unexpected_platform";
        #else
            string adUnitId = "unexpected_platform";
        #endif
        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.MediumRectangle, AdPosition.Bottom);

        // Called when an ad request has successfully loaded.
        bannerView.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        bannerView.OnAdOpening += HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        bannerView.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        bannerView.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);

    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        var props = new Value();
        props["admobunitid"] = adBannerId;
        props["event"] = "admob-showbanner-loaded";
        Mixpanel.Track("admob-banner-event", props);
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        var props = new Value();
        props["admobunitid"] = adBannerId;
        props["event"] = "admob-showbanner-failed";
        Mixpanel.Track("admob-banner-event", props);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        var props = new Value();
        props["admobunitid"] = adBannerId;
        props["event"] = "admob-showbanner-opened";
        Mixpanel.Track("admob-banner-event", props);
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        var props = new Value();
        props["admobunitid"] = adBannerId;
        props["event"] = "admob-showbanner-closed";
        Mixpanel.Track("admob-banner-event", props);
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        var props = new Value();
        props["admobunitid"] = adBannerId;
        props["event"] = "admob-showbanner-leaveapp";
        Mixpanel.Track("admob-banner-event", props);
    }

    //private void OnDestroy()
    //{
    //    if(bannerView != null) 
    //    bannerView.Destroy();
    //}

    void OnDestroy()
    {
        if (bannerView != null)
        bannerView.Destroy();
    }

}
