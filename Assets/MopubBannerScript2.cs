using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mixpanel;
using System;

public class MopubBannerScript2 : MonoBehaviour {

    public string bannerId = "b5521a2fdda14f4294b8068c384bfc5c";

    public bool isLoaded = false;

    private void Start()
    {
        //MoPubManager.OnAdLoadedEvent += myOnAdLoaded;
        //MoPubManager.OnAdFailedEvent += myOnAdFailed;
        //MoPub.CreateBanner(bannerId, MoPubBase.AdPosition.BottomCenter, MoPubBase.BannerType.Size320x50);
        //MoPub.ShowBanner(bannerId, true);
        MoPub.CreateBanner(bannerId, MoPubBase.AdPosition.TopLeft);

        //MoPub.CreateBanner("b5521a2fdda14f4294b8068c384bfc5c", MoPubBase.AdPosition.BottomRight, MoPubBase.BannerType.Size320x50);
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
        MoPub.ShowBanner(bannerId, false);
    }

    public void OnGUI()
    {

    }

}
