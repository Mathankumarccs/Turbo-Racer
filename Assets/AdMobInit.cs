using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using mixpanel;

public class AdMobInit : MonoBehaviour {

    public string appId = "ca-app-pub-4073866383873410~1716271158";

    // Use this for initialization
    void Start () {
        MobileAds.Initialize(appId);
        var props = new Value();
        props["admobappid"] = appId;
        props["event"] = "admob-initialize";
        Mixpanel.Track("admob-initialize", props);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
