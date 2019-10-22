using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MopubSecondScreen : MonoBehaviour {

    public string bannerId = "ac59559f7a2a4351ab50ded0fcf3f333";


    private void Start()
    {
         //Create and instantiate an SdkConfiguration
         //Publishers can override MoPubEventListener.OnSdkInitializedEvent() to get a callb
         //Alternatively, publishers can call "yield return WaitUntil(() => MoPub.IsSdkInitialized)" to suspend the coroutine execution until the SDK has initialized.
        MoPub.CreateBanner(bannerId, MoPubBase.AdPosition.TopRight);
    }

    private void OnDestroy()
    {
         MoPub.ShowBanner(bannerId, false);
    }
}
