using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MopubBannerScriptLevel : MonoBehaviour {

    public string bannerId = "f9dbbcd225954e168d66f01ab7662f0d";

    public bool isLoaded = false;

    private void Start()
    {
        //MoPub.ShowBanner(bannerId,true);
        //MoPub.CreateBanner("f9dbbcd225954e168d66f01ab7662f0d", MoPubBase.AdPosition.BottomRight, MoPubBase.BannerType.Size300x250);
        MoPub.CreateBanner(bannerId, MoPubBase.AdPosition.BottomRight);

    }

    public void OnDestroy()
    {
        MoPub.ShowBanner(bannerId, false);
    }

    public void OnGUI()
    {

    }

}
