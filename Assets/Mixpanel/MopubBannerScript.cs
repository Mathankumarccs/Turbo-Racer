using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoPubInternal;
using mixpanel;
using System;

public class MopubBannerScript : MonoBehaviour {

    public string bannerId = "b5521a2fdda14f4294b8068c384bfc5c";

    public bool isLoaded = false;

    private void Start()
    {
        //MoPubManager.OnAdLoadedEvent += myOnAdLoaded;
        //MoPubManager.OnAdFailedEvent += myOnAdFailed;
        //MoPub.CreateBanner(bannerId, MoPubBase.AdPosition.BottomCenter, MoPubBase.BannerType.Size320x50);
        MoPub.CreateBanner(bannerId, MoPubBase.AdPosition.BottomRight);
    }

    public void myOnAdLoaded(string unitId,float id) {
        Debug.Log("GGMOPUBTEST onAdLoaded delegate" + unitId);
        var props = new Value();
        props["mopubunitid"] = bannerId;
        props["event"] = "showinterstitial-success";
        Mixpanel.Track("mopub-interstitial", props);

    }

    public void myOnAdFailed(string unitId,string error)
    {
        Debug.Log("GGMOPUBTEST onAdFailed delegate" + unitId + "with error" + error);
        var props = new Value();
        props["mopubunitid"] = unitId;
        props["event"] = "showinterstitial-failed";
        props["reason"] = error;
        Mixpanel.Track("mopub-interstitial", props);
    }

    public void OnDestroy()
    {
        MoPub.DestroyBanner(bannerId);
    }

    public void OnGUI()
    {

    }

}
