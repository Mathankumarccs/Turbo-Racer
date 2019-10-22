using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoPubInternal;
using mixpanel;

public class MopubInterstitialScript : MonoBehaviour {

    public string interstitialId = "ac59559f7a2a4351ab50ded0fcf3f333";

    public bool isLoaded = false;

    private void Start()
    {
        MoPub.RequestInterstitialAd(interstitialId);
    }

    public void myOnAdLoaded(string unitId,float id) {
        Debug.Log("GGMOPUBTEST onAdLoaded delegate" + unitId);
        var props = new Value();
        props["mopubunitid"] = interstitialId;
        props["event"] = "showinterstitial-success";
        Mixpanel.Track("mopub-interstitial", props);

    }

    public void myOnAdFailed(string unitId,string error)
    {
        Debug.Log("GGMOPUBTEST onAdFailed delegate" + unitId + "with error" + error);
        var props = new Value();
        props["mopubunitid"] = interstitialId;
        props["event"] = "showinterstitial-failed";
        props["reason"] = error;
        Mixpanel.Track("mopub-interstitial", props);
    }

    public void OnDestroy()
    {
        //MoPub.ShowBanner(bannerId, false);
    }

    public void OnGUI()
    {
        if(GUI.Button(new Rect(10,10,100,40),"Show Interstitial")) {
            MoPub.ShowInterstitialAd(interstitialId);
            var props = new Value();
            props["mopubunitid"] = interstitialId;
            props["event"] = "showinterstitial";
            Mixpanel.Track("mopub-interstitial", props);
        }
    }

}
