using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mixpanel;
using System;
using MoPubInternal;

public class MoPubInitialize : MonoBehaviour {

    public string[] bannerIds = new string[] { "b5521a2fdda14f4294b8068c384bfc5c", "f9dbbcd225954e168d66f01ab7662f0d" };


	// Use this for initialization
	void Start () {
        // Create and instantiate an SdkConfiguration
        // Publishers can override MoPubEventListener.OnSdkInitializedEvent() to get a callback.
        // Alternatively, publishers can call "yield return WaitUntil(() => MoPub.IsSdkInitialized)" to suspend the coroutine execution until the SDK has initialized.
        Debug.Log("GGMOPUBTEST Initializing Mopub");
        MoPub.SdkConfiguration sdkConfiguration = new MoPub.SdkConfiguration();
        sdkConfiguration.AdUnitId = bannerIds[0];
        Debug.Log("GGMOPUBTEST Initializing Mopub1");
        MoPub.EnableLocationSupport(true);
        MoPubManager.OnSdkInitializedEvent += mySDKInitialized;
        //MoPub.RequestInterstitialAd(interstitialId3);
        Debug.Log("GGMOPUBTEST Initializing Mopub2");
        MoPub.LoadBannerPluginsForAdUnits(bannerIds);
        MoPub.InitializeSdk(sdkConfiguration);
        Debug.Log("GGMOPUBTEST Initializing Mopub3");

        var props = new Value();
        props["mopubunitid"] = bannerIds[0];
        props["event"] = "initialize";
        Mixpanel.Track("mopub-initialize", props);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void mySDKInitialized(string unitId)
    {

        Debug.Log("GGMOPUBTEST my SDK initialized" + unitId);
        var props = new Value();
        props["mopubunitid"] = bannerIds[0];
        props["event"] = "initialize-success";
        Mixpanel.Track("mopub-initialize", props);
    }
}
